"use client";
import activityApiRequest from "@/apis/activity.api";
import teamApiRequest from "@/apis/team.api";
import AppBreadcrumb, { PathItem } from "@/components/custom/_breadcrumb";
import { Button } from "@/components/custom/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Card, CardContent } from "@/components/ui/card";
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { activityOptions, TypeActivity } from "@/data/enums/typeactivity.enum";
import { Separator } from "@radix-ui/react-dropdown-menu";
import { useQuery } from "@tanstack/react-query";
import { Download } from "lucide-react";
import { useState } from "react";
import { utils, writeFile } from "xlsx";
const pathList: Array<PathItem> = [
  {
    name: "Hoạt động",
    url: "#",
  },
];
export default function ActivityLog({ params }: { params: { id: string } }) {
  const [selectedTypeActivity, setSelectedTypeActivity] = useState<
    TypeActivity[]
  >([]);
  const [selectedUsers, setSelectedUsers] = useState<number[]>([]);
  const toggleTypeActivity = (value: TypeActivity) => {
    setSelectedTypeActivity((prev) =>
      prev.includes(value)
        ? prev.filter((item) => item !== value)
        : [...prev, value]
    );
  };
  const toggleUser = (value: number) => {
    setSelectedUsers((prev) =>
      prev.includes(value)
        ? prev.filter((item) => item !== value)
        : [...prev, value]
    );
  };
  const { data } = useQuery({
    queryKey: ["users-project"],
    queryFn: () => teamApiRequest.getUsersByProjectId(Number(params.id)),
  });
  const handleExportExcel = () => {
    if (!dataActivities?.data) return;

    // Format data for Excel
    const excelData = dataActivities.data.map((item) => ({
      "Họ và tên": item.fullName,
      Email: item.email,
      "Nội dung": item.content,
      "Thời gian": item.createdAt,
    }));

    // Create worksheet
    const ws = utils.json_to_sheet(excelData);
    // Autofit columns
    const colWidths = [
      { wch: 25 }, // Họ và tên
      { wch: 30 }, // Email
      { wch: 50 }, // Nội dung
      { wch: 20 }, // Thời gian
    ];
    ws["!cols"] = colWidths;
    const wb = utils.book_new();
    utils.book_append_sheet(wb, ws, "Activities");

    const date = Date.now().toString();
    // Generate Excel file
    writeFile(wb, `Hoạt động của dự án-${date}.xlsx`);
  };
  const { data: dataActivities } = useQuery({
    queryKey: ["activities", selectedTypeActivity, selectedUsers],
    queryFn: () => {
      const paramProjectId = "projectId=" + params.id;
      const paramTypeActivities = selectedTypeActivity
        .map((item) => "TypeActivities=" + item)
        .join("&");
      const paramCreatedBys = selectedUsers
        .map((item) => "CreatedBys=" + item)
        .join("&");
      return activityApiRequest.getList(
        paramProjectId + "&" + paramTypeActivities + "&" + paramCreatedBys
      );
    },
  });
  return (
    <>
      <div className="mb-2 flex items-center justify-between space-y-2">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">
            Hoạt động trong dự án
          </h2>
          <AppBreadcrumb pathList={pathList} className="mt-2" />
        </div>
        <Button
          variant="outline"
          onClick={handleExportExcel}
          disabled={!dataActivities?.data || dataActivities.data.length === 0}
        >
          <Download className="mr-2 h-4 w-4" />
          Xuất Excel
        </Button>
      </div>
      <div className="flex items-center gap-4 mt-4 mb-4">
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="outline">Loại hoạt động</Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent className="w-56">
            <DropdownMenuLabel>Loại hoạt động</DropdownMenuLabel>
            <DropdownMenuSeparator />
            {activityOptions.map((item, index) => {
              return (
                <DropdownMenuCheckboxItem
                  key={index}
                  checked={selectedTypeActivity.includes(item.value)}
                  onCheckedChange={() => toggleTypeActivity(item.value)}
                  onSelect={(e) => e.preventDefault()}
                >
                  {item.icon}
                  <span>{item.label}</span>
                </DropdownMenuCheckboxItem>
              );
            })}
          </DropdownMenuContent>
        </DropdownMenu>
        {data && (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">Người dùng</Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent className="w-56">
              <DropdownMenuLabel>Người dùng</DropdownMenuLabel>
              <DropdownMenuSeparator />
              {data.data.map((item, index) => {
                return (
                  <DropdownMenuCheckboxItem
                    key={index}
                    checked={selectedUsers.includes(item.id!)}
                    onCheckedChange={() => toggleUser(item.id!)}
                    onSelect={(e) => e.preventDefault()}
                  >
                    {item.email}
                  </DropdownMenuCheckboxItem>
                );
              })}
            </DropdownMenuContent>
          </DropdownMenu>
        )}
      </div>
      <Separator className="my-4" />
      <div className="space-y-4">
        {dataActivities?.data.map((item, index) => {
          return (
            <Card key={index}>
              <CardContent className="p-4 flex items-start justify-between">
                <div className="flex items-start gap-4">
                  {
                    activityOptions.find(
                      (option) => option.value === item.typeActivity
                    )?.icon
                  }
                  {/* <CheckCircle2 className="text-blue-500 mt-1" /> */}
                  <div className="flex flex-col gap-1">
                    <div className="flex items-center gap-2">
                      <Avatar className="w-8 h-8">
                        <AvatarImage src={item.imageUrl} alt={item.fullName} />
                        <AvatarFallback>{item.fullName}</AvatarFallback>
                      </Avatar>
                      <div>
                        <p className="font-medium">{item.fullName}</p>
                        <p className="text-xs text-muted-foreground">
                          {item.email}
                        </p>
                      </div>
                    </div>
                    <p className="text-sm">{item.content}</p>
                  </div>
                </div>
                <p className="text-sm text-muted-foreground whitespace-nowrap">
                  {item.createdAt}
                </p>
              </CardContent>
            </Card>
          );
        })}
      </div>
    </>
  );
}
