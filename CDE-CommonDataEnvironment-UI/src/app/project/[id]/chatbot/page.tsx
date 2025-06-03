"use client";
import geminiApiRequest, { GeminiAskRequest } from "@/apis/gemini.api";
import { Badge } from "@/components/ui/badge";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Conversation } from "@/data/schema/Project/conversation.schema";
import { Message } from "@/data/schema/Project/message.schema";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Bot } from "lucide-react";
import { useEffect, useRef, useState } from "react";
import ChatInput from "./_components/chat-input";
import ChatMessage from "./_components/chat-message";
import ConversationList from "./_components/conversation-list";

const ChatContainer = ({ params }: { params: { id: string } }) => {
  const queryClient = useQueryClient();
  const [conversation, setConversation] = useState<Conversation>();
  const [listConversation, setListConversation] = useState<Conversation[]>();
  const [messages, setMessages] = useState<Message[]>();
  const [isTyping, setIsTyping] = useState(false);
  const bottomRef = useRef<HTMLDivElement>(null);
  const scrollAreaRef = useRef<HTMLDivElement>(null);

  const { data, isLoading } = useQuery({
    queryKey: ["get-list-conversation"],
    queryFn: () => geminiApiRequest.getList(Number(params.id)),
  });
  useEffect(() => {
    if (bottomRef.current) {
      setTimeout(() => {
        bottomRef.current?.scrollIntoView({ behavior: "smooth" });
      }, 100);
    }
  }, [messages, isTyping]);
  useEffect(() => {
    setMessages(conversation?.messages);
  }, [conversation]);
  useEffect(() => {
    if (data) {
      console.log(data?.data)
      setListConversation(data?.data);
    }
  }, [data]);
  const { mutate } = useMutation({
    mutationKey: ["ask-gemini"],
    mutationFn: (request: GeminiAskRequest) => geminiApiRequest.ask(request),
    onSuccess: (data) => {
      const aiResponse: Message = {
        content: data.data,
        isAI: true,
      };

      setMessages((prev) => [...(prev ?? []), aiResponse]);
      queryClient.invalidateQueries({ queryKey: ["get-list-conversation"] });
      setIsTyping(false);
    },
  });
  const handleSendMessage = async (content: string) => {
    if (!content.trim()) return;

    const userMessage: Message = {
      content: content,
      isAI: false,
    };

    setMessages((prev) => [...(prev ?? []), userMessage]);
    setIsTyping(true);
    console.log({
      conversationId: conversation?.id ?? 0,
      question: content,
      projectId: Number(params.id),
    });
    mutate({
      conversationId: conversation?.id ?? 0,
      question: content,
      projectId: Number(params.id),
    });
  };
  console.log(conversation, isLoading);
  return (
    <div className="flex gap-2 mt-8">
      <div className="flex-1 my-auto h-[90vh] sm:h-[80vh] bg-card rounded-lg sm:rounded-xl md:rounded-2xl shadow-lg border border-border overflow-hidden flex flex-col">
        <ConversationList
          conversations={listConversation}
          setConversation={setConversation}
          setListConversation={setListConversation}
        />
      </div>
      <div className="flex-[3]">
        <div className="my-auto flex-1 h-[90vh] sm:h-[80vh] bg-card rounded-lg sm:rounded-xl md:rounded-2xl shadow-lg border border-border overflow-hidden flex flex-col">
          <div className="py-4 px-6 border-b border-border flex items-center justify-between bg-gradient-to-r from-card to-background">
            <div className="flex items-center gap-3">
              <div className="w-10 h-10 rounded-full bg-primary/10 flex items-center justify-center">
                <Bot className="h-5 w-5 text-primary" />
              </div>
              <div>
                <h1 className="text-xl font-semibold tracking-tight">
                  CDE AI Chatbot
                </h1>
                <div className="flex items-center gap-2">
                  <Badge
                    variant="outline"
                    className="text-xs bg-primary/5 text-primary"
                  >
                    Online
                  </Badge>
                  <span className="text-xs text-muted-foreground">
                    Mô hình 2.0-Flash Gemini AI Model
                  </span>
                </div>
              </div>
            </div>
          </div>

          {/* Messages */}
          <ScrollArea
            ref={scrollAreaRef}
            className="flex-1 p-4 overflow-y-auto"
          >
            <div className="flex flex-col gap-6">
              {messages?.map((message, index) => (
                <ChatMessage key={index} message={message} />
              ))}

              {isTyping && (
                <div className="flex items-start gap-3">
                  <div className="w-8 h-8 rounded-full flex items-center justify-center bg-primary/10 text-primary">
                    <Bot className="h-4 w-4" />
                  </div>
                  <div className="py-3 px-4 bg-muted rounded-2xl rounded-tl-none max-w-[80%]">
                    <div className="typing-indicator">
                      <span>.</span>
                      <span>.</span>
                      <span>.</span>
                    </div>
                  </div>
                </div>
              )}
              <div ref={bottomRef} />
            </div>
          </ScrollArea>

          {/* Input */}
          <div className="p-4 border-t border-border bg-card">
            <ChatInput onSendMessage={handleSendMessage} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ChatContainer;
