import { Button } from "@/components/ui/button";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Conversation } from "@/data/schema/Project/conversation.schema";
import { cn } from "@/lib/utils";
import { format } from "date-fns";
import { MessageSquare, Plus } from "lucide-react";

interface ConversationListProps {
  className?: string;
  conversations?: Conversation[];
  setConversation: React.Dispatch<
    React.SetStateAction<Conversation | undefined>
  >;
  setListConversation: React.Dispatch<
    React.SetStateAction<Conversation[] | undefined>
  >;
}

const ConversationList = ({
  className,
  conversations,
  setConversation,
  setListConversation,
}: ConversationListProps) => {
  return (
    <div
      className={cn(
        "flex flex-col bg-card rounded-lg overflow-hidden",
        className
      )}
    >
      <div className="p-4  bg-gradient-to-r from-card to-background">
        <Button
          onClick={() =>
            setListConversation([
              ...(conversations ?? []),
              {
                title: "Đoạn chat mới",
                messages: [],
              },
            ])
          }
          className="w-full justify-start gap-2"
          variant="outline"
        >
          <Plus className="h-4 w-4" />
          Đoạn chat mới
        </Button>
      </div>

      <ScrollArea className="flex-1">
        <div className="p-2">
          {conversations
            ?.slice()
            .reverse()
            .map((conversation, index) => {
              const lastMessage =
                conversation.messages?.[conversation.messages.length - 1];
              return (
                <button
                  onClick={() => setConversation(conversation)}
                  key={index}
                  className="w-full p-3 rounded-lg hover:bg-muted text-left transition-colors group space-y-1"
                >
                  <div className="flex items-center gap-2">
                    <MessageSquare className="h-4 w-4 text-muted-foreground" />
                    <span className="font-medium truncate">
                      {conversation.title}
                    </span>
                  </div>
                  <div className="text-xs text-muted-foreground line-clamp-1">
                    {lastMessage?.content}
                  </div>
                  {lastMessage ? (
                    <div className="text-xs text-muted-foreground opacity-0 group-hover:opacity-100 transition-opacity">
                      {format(
                        new Date(lastMessage?.createdAt ?? ""),
                        "MMM d, h:mm a"
                      )}
                    </div>
                  ) : (
                    <></>
                  )}
                </button>
              );
            })}
        </div>
      </ScrollArea>
    </div>
  );
};

export default ConversationList;
