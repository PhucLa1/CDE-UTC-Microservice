import { useState, useEffect } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Clock } from "lucide-react";
import { cn } from "@/lib/utils";

interface TimePickerDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onTimeSelect: (time: string) => void;
  defaultTime?: string;
  title: string;
  description: string;
}

export function TimePickerDialog({
  open,
  onOpenChange,
  onTimeSelect,
  defaultTime = "09:00",
  title,
  description,
}: TimePickerDialogProps) {
  const [hours, setHours] = useState("00");
  const [minutes, setMinutes] = useState("00");
  const [period, setPeriod] = useState<"AM" | "PM">("AM");

  useEffect(() => {
    if (defaultTime) {
      const [hoursStr, minutesStr] = defaultTime.split(":");
      let hoursNum = parseInt(hoursStr, 10);

      if (hoursNum >= 12) {
        setPeriod("PM");
        if (hoursNum > 12) hoursNum -= 12;
      } else {
        setPeriod("AM");
        if (hoursNum === 0) hoursNum = 12;
      }

      setHours(hoursNum.toString().padStart(2, "0"));
      setMinutes(minutesStr.padStart(2, "0"));
    }
  }, [defaultTime, open]);

  const handleSelectTime = () => {
    let hoursInt = parseInt(hours, 10);

    // Convert from 12-hour to 24-hour
    if (period === "PM" && hoursInt < 12) {
      hoursInt += 12;
    } else if (period === "AM" && hoursInt === 12) {
      hoursInt = 0;
    }

    const formattedTime = `${hoursInt.toString().padStart(2, "0")}:${minutes}`;
    onTimeSelect(formattedTime);
    onOpenChange(false);
  };

  const hourOptions = Array.from({ length: 12 }, (_, i) =>
    (i + 1).toString().padStart(2, "0")
  );

  const minuteOptions = Array.from({ length: 60 }, (_, i) =>
    i.toString().padStart(2, "0")
  );

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-[400px]">
        <DialogHeader>
          <DialogTitle className="flex items-center gap-2">
            <Clock className="h-5 w-5" />
            {title}
          </DialogTitle>
          <DialogDescription>{description}</DialogDescription>
        </DialogHeader>

        <div className="flex items-center justify-center py-6">
          <div className="grid grid-cols-3 gap-1 text-center">
            <div className="flex flex-col items-center">
              <span className="text-sm text-muted-foreground mb-2">Giờ</span>
              <div className="h-[180px] overflow-y-auto pr-1 scrollbar-thin scrollbar-thumb-muted-foreground/20 scrollbar-track-transparent">
                <div className="space-y-1">
                  {hourOptions.map((hour) => (
                    <Button
                      key={hour}
                      variant={hours === hour ? "default" : "outline"}
                      className={cn(
                        "w-16 transition-all",
                        hours === hour
                          ? "bg-primary text-primary-foreground"
                          : "hover:bg-muted"
                      )}
                      onClick={() => setHours(hour)}
                    >
                      {hour}
                    </Button>
                  ))}
                </div>
              </div>
            </div>

            <div className="flex flex-col items-center">
              <span className="text-sm text-muted-foreground mb-2">Phút</span>
              <div className="h-[180px] overflow-y-auto px-1 scrollbar-thin scrollbar-thumb-muted-foreground/20 scrollbar-track-transparent">
                <div className="space-y-1">
                  {minuteOptions.map((minute) => (
                    <Button
                      key={minute}
                      variant={minutes === minute ? "default" : "outline"}
                      className={cn(
                        "w-16 transition-all",
                        minutes === minute
                          ? "bg-primary text-primary-foreground"
                          : "hover:bg-muted"
                      )}
                      onClick={() => setMinutes(minute)}
                    >
                      {minute}
                    </Button>
                  ))}
                </div>
              </div>
            </div>

            <div className="flex flex-col items-center">
              <span className="text-sm text-muted-foreground mb-2">Thời gian</span>
              <div className="space-y-2 mt-4">
                <Button
                  variant={period === "AM" ? "default" : "outline"}
                  className={cn(
                    "w-16 transition-all",
                    period === "AM"
                      ? "bg-primary text-primary-foreground"
                      : "hover:bg-muted"
                  )}
                  onClick={() => setPeriod("AM")}
                >
                  AM
                </Button>
                <Button
                  variant={period === "PM" ? "default" : "outline"}
                  className={cn(
                    "w-16 transition-all",
                    period === "PM"
                      ? "bg-primary text-primary-foreground"
                      : "hover:bg-muted"
                  )}
                  onClick={() => setPeriod("PM")}
                >
                  PM
                </Button>
              </div>
            </div>
          </div>
        </div>

        <DialogFooter>
          <Button variant="outline" onClick={() => onOpenChange(false)}>
            Hủy
          </Button>
          <Button onClick={handleSelectTime}>Chọn giờ</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
