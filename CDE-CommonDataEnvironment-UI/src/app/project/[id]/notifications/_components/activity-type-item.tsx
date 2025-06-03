import { useState } from "react";
import { Clock } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Switch } from "@/components/ui/switch";
import { Separator } from "@/components/ui/separator";
import { cn } from "@/lib/utils";
import { TimePickerDialog } from "./time-picker-dialog";
import { ActivityTypeSchemaWithIcon } from "../page";
import { Role } from "@/data/enums/role.enum";
import { useRole } from "@/hooks/use-role";

interface NotificationItemProps {
  notification: ActivityTypeSchemaWithIcon;
  onTimeChange: (id: number, time: string) => void;
  onToggleEnabled: (id: number) => void;
}

export function NotificationItem({
  notification,
  onTimeChange,
  onToggleEnabled,
}: NotificationItemProps) {
  const [timeDialogOpen, setTimeDialogOpen] = useState(false);
  const { roleDetail } = useRole();
  const handleTimeSelect = (time: string) => {
    onTimeChange(notification.id!, time);
  };

  return (
    <div
      className={cn(
        "group relative rounded-lg border p-4 transition-all",
        notification.enabled ? "bg-background" : "bg-muted/30 opacity-80",
        notification.isUpdated &&
          "animate-in fade-in-0 border-primary/20 dark:border-primary/30"
      )}
    >
      <div className="flex flex-col sm:flex-row justify-between gap-2 sm:items-center">
        <div className="flex items-start gap-3">
          <div className={cn("mt-1 rounded-md p-1.5 ")}>
            {notification.icon}
          </div>
          <div className="space-y-1">
            <div className="flex items-center">
              <h3 className="font-medium">{notification.label}</h3>
            </div>
            <p className="text-sm text-muted-foreground">
              {notification.description}
            </p>
          </div>
        </div>
        <div className="flex items-center gap-4">
          <div className="flex flex-col items-end gap-1">
            <Button
              variant="ghost"
              size="sm"
              onClick={() => setTimeDialogOpen(true)}
              className={cn(
                "text-muted-foreground hover:text-foreground h-8 px-2",
                notification.isUpdated && "text-primary hover:text-primary/80"
              )}
            >
              <Clock
                className={cn(
                  "h-3.5 w-3.5 mr-1.5 transition-colors",
                  notification.isUpdated && "text-primary"
                )}
              />
              <span
                className={cn(
                  "text-sm transition-colors",
                  notification.isUpdated && "font-medium text-primary"
                )}
              >
                {notification.timeSend}
              </span>
            </Button>
            {notification.isUpdated && (
              <span className="text-[10px] text-primary">Cập nhật</span>
            )}
          </div>
          <Separator orientation="vertical" className="h-8 hidden sm:block" />
          <div className="flex items-center">
            <Switch
              disabled={roleDetail?.role !== Role.Admin}
              checked={notification.enabled}
              onCheckedChange={() => onToggleEnabled(notification.id!)}
            />
          </div>
        </div>
      </div>

      {roleDetail?.role == Role.Admin ? <TimePickerDialog
        open={timeDialogOpen}
        onOpenChange={setTimeDialogOpen}
        onTimeSelect={handleTimeSelect}
        defaultTime={notification.timeSend}
        title={`Chọn giờ cho ${notification.label}`}
        description={`Chọn phần để gửi thông báo ${notification.label!}`}
      /> : <></>}
    </div>
  );
}
