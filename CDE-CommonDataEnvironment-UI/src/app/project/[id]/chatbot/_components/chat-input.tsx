import { useState, useRef, useEffect } from "react";
import { Send, Paperclip, Sparkles } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";
import { cn } from "@/lib/utils";

interface ChatInputProps {
  onSendMessage: (message: string) => void;
}

const ChatInput = ({ onSendMessage }: ChatInputProps) => {
  const [message, setMessage] = useState("");
  const [rows, setRows] = useState(1);
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  useEffect(() => {
    if (textareaRef.current) {
      const scrollHeight = textareaRef.current.scrollHeight;
      const lineHeight = parseInt(
        getComputedStyle(textareaRef.current).lineHeight
      );
      const newRows = Math.min(
        5,
        Math.max(1, Math.floor(scrollHeight / lineHeight))
      );
      setRows(newRows);
    }
  }, [message]);

  const handleSend = () => {
    if (message.trim()) {
      onSendMessage(message);
      setMessage("");
      setRows(1);
    }
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLTextAreaElement>) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      handleSend();
    }
  };

  return (
    <div className="flex flex-col">
      <div className="relative flex items-end bg-background rounded-lg border border-input px-3 py-2 focus-within:ring-1 focus-within:ring-ring">
        <Textarea
          ref={textareaRef}
          rows={rows}
          className={cn(
            "flex-1 resize-none border-0 bg-transparent p-0 focus-visible:ring-0 focus-visible:ring-offset-0",
            "placeholder:text-muted-foreground",
            rows === 1 ? "min-h-[40px] max-h-[40px] py-2.5" : "min-h-[40px]"
          )}
          placeholder="Nhập câu hỏi..."
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          onKeyDown={handleKeyDown}
        />

        <div className="flex items-center gap-2 ml-3">
          <Button
            type="button"
            size="icon"
            variant="ghost"
            className="h-8 w-8 rounded-full"
          >
            <Paperclip className="h-4 w-4 text-muted-foreground" />
            <span className="sr-only">Attach file</span>
          </Button>

          <Button
            type="button"
            size="icon"
            variant="ghost"
            className="h-8 w-8 rounded-full"
          >
            <Sparkles className="h-4 w-4 text-muted-foreground" />
            <span className="sr-only">AI suggestions</span>
          </Button>

          <Button
            type="button"
            size="icon"
            variant="secondary"
            className={cn(
              "h-8 w-8 rounded-full transition-all",
              !message.trim() && "opacity-50 cursor-not-allowed"
            )}
            onClick={handleSend}
            disabled={!message.trim()}
          >
            <Send className="h-4 w-4" />
            <span className="sr-only">Gửi tin nhắn</span>
          </Button>
        </div>
      </div>
      <div className="mt-2 text-xs text-center text-muted-foreground">
        Nhấn Enter để gửi, Shift+Enter xuống dòng
      </div>
    </div>
  );
};

export default ChatInput;
