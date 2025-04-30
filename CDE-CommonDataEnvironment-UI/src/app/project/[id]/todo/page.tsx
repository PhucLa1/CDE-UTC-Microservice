"use client";
import AppBreadcrumb, { PathItem } from "@/components/custom/_breadcrumb";
import { Button } from "@/components/custom/button";
import { Input } from "@/components/ui/input";
import { LayoutGrid, List, Search } from "lucide-react";
import React, { useState } from "react";
import { useQuery } from "@tanstack/react-query";
import viewApiRequest from "@/apis/view.api";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Checkbox } from "@/components/ui/checkbox";
import { ChevronUpIcon } from "@radix-ui/react-icons";
import { Todo } from "@/data/schema/Project/todo.schema";
import { UpsertTodo } from "./components/form-crud-todo";
const pathList: Array<PathItem> = [
  {
    name: "Danh sách việc cần làm",
    url: "#",
  },
];
export default function page({ params }: { params: { id: string } }) {
  const [searchQuery, setSearchQuery] = useState("");
  const [viewMode, setViewMode] = useState(
    localStorage.getItem("viewModeData") || "table"
  );
  const [isOpen, setIsOpen] = useState<boolean | Todo>(false);

  const handleViewModeChange = (mode: "table" | "grid") => {
    localStorage.setItem("viewModeData", mode);
    setViewMode(mode); // Cập nhật state để component re-render
  };

  const { data, isLoading } = useQuery({
    queryKey: ["views"],
    queryFn: () => viewApiRequest.getList(Number(params.id)),
  });
  console.log(!!isOpen)
  if (isLoading) return <></>;
  return (
    <>
      {!!isOpen ? <UpsertTodo projectId={Number(params.id)} mode={'ADD'} isOpen={isOpen} setIsOpen={setIsOpen}/> : <></>}
      <div className="mb-2 flex items-center justify-between space-y-2">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Việc cần làm</h2>
          <AppBreadcrumb pathList={pathList} className="mt-2" />
        </div>
        <div>
          <Button onClick={() => setIsOpen(true)}>Thêm mới</Button>
        </div>
      </div>

      <div className="flex justify-between items-center gap-4">
        <div className="relative flex-1 max-w-xs">
          <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
          <Input
            type="text"
            placeholder="Tìm kiếm..."
            className="pl-9"
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
          />
        </div>

        <div className="flex gap-2">
          <Button
            variant="outline"
            size="icon"
            onClick={() => handleViewModeChange("table")}
            className={viewMode === "table" ? "bg-accent" : ""}
          >
            <List className="h-4 w-4" />
          </Button>
          <Button
            variant="outline"
            size="icon"
            onClick={() => handleViewModeChange("grid")}
            className={viewMode === "grid" ? "bg-accent" : ""}
          >
            <LayoutGrid className="h-4 w-4" />
          </Button>
        </div>
      </div>
      <div>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="w-[200px]">
                Tên <ChevronUpIcon className="inline-block ml-2 h-4 w-4" />
              </TableHead>
              <TableHead>Được giao cho</TableHead>
              <TableHead>Tạo vào</TableHead>
              <TableHead>Tạo bởi</TableHead>
              <TableHead>Trạng thái</TableHead>
              <TableHead>Ưu tiên</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody></TableBody>
        </Table>
      </div>
    </>
  );
}
