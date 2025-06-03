"use client";
import AppBreadcrumb, { PathItem } from "@/components/custom/_breadcrumb";
import { Search, Zap } from "lucide-react";
import { useToast } from "@/hooks/use-toast";
import { ActivityType } from "@/data/schema/Project/activitytype.schema";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { NotificationItem } from "./_components/activity-type-item";
import { TimePickerDialog } from "./_components/time-picker-dialog";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/custom/button";
import { ReactNode, useEffect, useState } from "react";
import { useMutation, useQuery } from "@tanstack/react-query";
import activityTypeApiRequest from "@/apis/activitytype.api";
import { activityOptions } from "@/data/enums/typeactivity.enum";
import { Checkbox } from "@/components/ui/checkbox";
import { handleSuccessApi } from "@/lib/utils";
import { Role } from "@/data/enums/role.enum";
import { useRole } from "@/hooks/use-role";
const pathList: Array<PathItem> = [
  {
    name: "Thông báo",
    url: "#",
  },
];
export type ActivityTypeSchemaWithIcon = ActivityType & {
  icon?: ReactNode;
};
export default function Page({ params }: { params: { id: string } }) {
  const { roleDetail } = useRole();
  const { toast } = useToast();
  const [checked, setChecked] = useState<boolean>(false);
  const [types, setTypes] = useState<ActivityTypeSchemaWithIcon[]>([]);
  const [searchQuery, setSearchQuery] = useState("");
  const [syncDialogOpen, setSyncDialogOpen] = useState(false);

  const { data, isLoading } = useQuery({
    queryKey: ["get-list-activity-type"],
    queryFn: () => activityTypeApiRequest.getList(Number(params.id)),
  });

  const { mutate, isPending } = useMutation({
    mutationFn: () => {
      const update = {
        isAcceptAll: checked,
        projectId: Number(params.id),
        updateActivityTypesDtos: types.map((type) => ({
          id: type.id!,
          enabled: type.enabled!,
          timeSend: type.timeSend!,
        })),
      };
      console.log(update);
      return activityTypeApiRequest.update(update);
    },
    onSuccess: () => {
      handleSuccessApi({
        title: "Cập nhật thời gian gửi thông báo thành công",
        message: "Bạn đã cập nhật gửi thông báo thành công",
      });
    },
  });

  useEffect(() => {
    if (data) {
      const types = data.data.map((item) => {
        item.isUpdated = false;
        item.label = activityOptions.filter(
          (ao) => ao.value == item.typeActivity
        )[0].label;
        item.timeSend = item.timeSend?.slice(0, 5);
        item.icon = activityOptions.filter(
          (ao) => ao.value == item.typeActivity
        )[0].icon;
        item.description = activityOptions.filter(
          (ao) => ao.value == item.typeActivity
        )[0].description;
        return item;
      });
      setTypes(types);
      setChecked(data.data[0].isAcceptAll!);
    }
  }, [isLoading, data]);
  const handleTimeChange = (id: number, time: string) => {
    setTypes(
      types.map((type) =>
        type.id === id ? { ...type, timeSend: time, isUpdated: true } : type
      )
    );
    setChecked(false);
  };

  const handleToggleEnabled = (id: number) => {
    setTypes(
      types.map((type) =>
        type.id === id ? { ...type, enabled: !type.enabled } : type
      )
    );
  };

  const handleSync = (time: string) => {
    setTypes(
      types.map((type) => ({
        ...type,
        timeSend: time,
        isUpdated: true,
      }))
    );
    setChecked(true);

    toast({
      title: "Tất cả cấu hình được đồng bộ",
      description: `Tất cả thông báo sẽ được gửi vào ${time}`,
    });
  };

  const filteredTypes = types.filter((type) =>
    type.label!.toLowerCase().includes(searchQuery.toLowerCase())
  );

  const animationDelay = (index: number) => {
    return {
      animationDelay: `${index * 50}ms`,
    };
  };
  return (
    <>
      <div className="mb-2 flex items-center justify-between space-y-2">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">
            Cấu hình thông báo
          </h2>
          <AppBreadcrumb pathList={pathList} className="mt-2" />
        </div>
        {roleDetail?.role == Role.Admin ?<div>
          <Button loading={isPending} onClick={() => mutate()}>
            Lưu
          </Button>
        </div> : <></>}
      </div>
      <Card className="w-full mx-auto mb-4">
        <CardHeader className="pb-3">
          <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
            <CardTitle className="text-2xl">Thông báo về hoạt động</CardTitle>
            <div className="flex items-center gap-4">
              <div className="flex items-center space-x-2">
                <Checkbox id="terms" checked={checked} />
                <label
                  htmlFor="terms"
                  className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                >
                  Gửi trong cùng 1 tệp
                </label>
              </div>
              {roleDetail?.role == Role.Admin ? <Button
                onClick={() => setSyncDialogOpen(true)}
                className="gap-2 ml-2"
                size="sm"
              >
                <Zap className="h-4 w-4" />
                <span>Đồng bộ giờ</span>
              </Button> : <></>}
            </div>
          </div>
          <div className="relative">
            <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
            <Input
              type="search"
              placeholder="Tìm kiếm thông báo"
              className="pl-8"
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
          </div>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-2 gap-3">
            {filteredTypes.length === 0 ? (
              <div className="py-12 text-center text-muted-foreground">
                Không thấy cấu hình nào
              </div>
            ) : (
              filteredTypes.map((type, index) => (
                <div
                  key={type.id}
                  className="animate-in fade-in-50 slide-in-from-bottom-3"
                  style={animationDelay(index)}
                >
                  <NotificationItem
                    notification={type}
                    onTimeChange={handleTimeChange}
                    onToggleEnabled={handleToggleEnabled}
                  />
                </div>
              ))
            )}
          </div>
        </CardContent>

        <TimePickerDialog
          open={syncDialogOpen}
          onOpenChange={setSyncDialogOpen}
          onTimeSelect={handleSync}
          title="Đồng bộ toàn thông báo"
          description="Chọn giờ để đồng bộ toàn thông báo"
        />
      </Card>
    </>
  );
}
