import { useState } from "react";
import { format } from "date-fns";
import { Check, Copy, Bot, User } from "lucide-react";
import { cn } from "@/lib/utils";
import { useToast } from "@/hooks/use-toast";
import { Button } from "@/components/ui/button";
import { Message } from "@/data/schema/Project/message.schema";


interface ChatMessageProps {
  message: Message;
}

const ChatMessage = ({ message }: ChatMessageProps) => {
  const [copied, setCopied] = useState(false);
  const { toast } = useToast();
  const isAI = message.isAI;

  const copyToClipboard = () => {
    navigator.clipboard
      .writeText(message.content!)
      .then(() => {
        setCopied(true);
        toast({
          description: "Message copied to clipboard",
          duration: 2000,
        });
        setTimeout(() => setCopied(false), 2000);
      })
      .catch((err) => {
        console.log("Failed to copy message: ", err);
        toast({
          variant: "destructive",
          description: "Failed to copy message",
          duration: 2000,
        });
      });
  };

  const formatTimestamp = (date: Date) => {
    return format(date, "h:mm a");
  };
  console.log("formatTimestamp: ", formatTimestamp(new Date()));

  // Function to render message content with code blocks
  const renderContent = (content: string) => {
    const codeBlockRegex = /```(\w+)?\n([\s\S]*?)```/g;
    const parts = [];
    let lastIndex = 0;
    let match;

    // Find code blocks and process them
    while ((match = codeBlockRegex.exec(content)) !== null) {
      // Add text before code block
      if (match.index > lastIndex) {
        parts.push(
          <p key={`text-${lastIndex}`} className="whitespace-pre-wrap">
            {content.substring(lastIndex, match.index)}
          </p>
        );
      }

      // Add code block
      const language = match[1] || "plaintext";
      const code = match[2];

      parts.push(
        <div key={`code-${match.index}`} className="my-2 relative group">
          <div className="absolute right-2 top-2 opacity-0 group-hover:opacity-100 transition-opacity">
            <Button
              variant="ghost"
              size="sm"
              className="h-8 w-8 p-0 rounded-full"
              onClick={() => {
                navigator.clipboard.writeText(code);
                toast({
                  description: "Sao chép thành công",
                  duration: 2000,
                });
              }}
            >
              <Copy className="h-4 w-4" />
              <span className="sr-only">Sao chép</span>
            </Button>
          </div>
          <div className="bg-muted text-muted-foreground rounded-md p-3 overflow-x-auto">
            <pre className="text-sm">
              <code>{code}</code>
            </pre>
          </div>
          {language !== "plaintext" && (
            <div className="absolute top-0 right-0 bg-primary/10 text-primary text-xs px-2 py-0.5 rounded-bl-md rounded-tr-md">
              {language}
            </div>
          )}
        </div>
      );

      lastIndex = match.index + match[0].length;
    }

    // Add remaining text after last code block
    if (lastIndex < content.length) {
      parts.push(
        <p key={`text-${lastIndex}`} className="whitespace-pre-wrap">
          {content.substring(lastIndex)}
        </p>
      );
    }

    return parts.length > 0 ? (
      parts
    ) : (
      <p className="whitespace-pre-wrap">{content}</p>
    );
  };

  return (
    <div className={cn("flex gap-3", !isAI && "flex-row-reverse")}>
      <div
        className={cn(
          "w-8 h-8 rounded-full flex items-center justify-center shrink-0",
          isAI
            ? "bg-primary/10 text-primary"
            : "bg-secondary text-secondary-foreground"
        )}
      >
        {isAI ? <Bot className="h-4 w-4" /> : <User className="h-4 w-4" />}
      </div>

      <div className={cn("group", "max-w-[80%]", isAI && "mr-12")}>
        <div
          className={cn(
            "relative py-3 px-4 rounded-2xl",
            isAI
              ? "bg-muted rounded-tl-none"
              : "bg-primary text-primary-foreground rounded-tr-none"
          )}
        >
          <div className="text-sm">{renderContent(message.content!)}</div>

          {/* Copy button (only for AI messages) */}
          {isAI && (
            <Button
              variant="ghost"
              size="sm"
              className="absolute -right-10 top-2 opacity-0 group-hover:opacity-100 transition-opacity h-8 w-8 p-0"
              onClick={copyToClipboard}
            >
              {copied ? (
                <Check className="h-4 w-4" />
              ) : (
                <Copy className="h-4 w-4" />
              )}
              <span className="sr-only">Copy message</span>
            </Button>
          )}
        </div>

        <div
          className={cn(
            "text-xs text-muted-foreground mt-1",
            !isAI && "text-right"
          )}
        >
          {/* {formatTimestamp(new Date(message.createdAt ?? ''))} */}
        </div>
      </div>
    </div>
  );
};

export default ChatMessage;
