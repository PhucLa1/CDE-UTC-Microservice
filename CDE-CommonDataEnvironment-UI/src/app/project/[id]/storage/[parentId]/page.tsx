"use client";
import storageApiRequest from "@/apis/storage.api";
import AppBreadcrumb, { PathItem } from "@/components/custom/_breadcrumb";
import { Button } from "@/components/custom/button";
import { Input } from "@/components/ui/input";
import { useQuery } from "@tanstack/react-query";
import { LayoutGrid, List, Search } from "lucide-react";
import { useState } from "react";
import { FaFolder, FaUpload } from "react-icons/fa";
import CreateFolder from "../_components/create-folder";
import GridStorage from "../_components/grid-storage";
import TableStorage from "../_components/table-storage";
import UploadFile from "../_components/upload-file";
const pathList: Array<PathItem> = [
  {
    name: "Tệp & Thư mục",
    url: "#",
  },
];
export default function Page({
  params,
}: {
  params: { id: string; parentId: string };
}) {
  const [searchQuery, setSearchQuery] = useState("");
  const [isOpen, setIsOpen] = useState(false);
  const [viewMode, setViewMode] = useState(
    localStorage.getItem("viewModeData") || "table"
  );

  const handleViewModeChange = (mode: "table" | "grid") => {
    localStorage.setItem("viewModeData", mode);
    setViewMode(mode); // Cập nhật state để component re-render
  };

  const { data, isLoading } = useQuery({
    queryKey: ["storage", Number(params.parentId)],
    queryFn: () =>
      storageApiRequest.getList(Number(params.id), Number(params.parentId)),
  });

  const { data: dataPath, isLoading: isLoadingPath } = useQuery({
    queryKey: ["full-path", Number(params.parentId)],
    queryFn: () => storageApiRequest.getFullPath(Number(params.parentId)),
  });
  if (isLoading || isLoadingPath) return <></>;
  return (
    <>
      <div className="mb-2 flex items-center justify-between space-y-2">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Lưu trữ</h2>
          <AppBreadcrumb pathList={pathList} className="mt-2" />
        </div>
        <div className="mr-1">
          <div className="bg-gray-100 flex items-start justify-center">
            <div className="relative">
              <Button onClick={() => setIsOpen(!isOpen)}>Thêm mới</Button>
              <div
                className={`${
                  isOpen ? "" : "hidden"
                } absolute right-0 mt-2 w-64 bg-white rounded-lg shadow-lg py-2 z-10`}
              >
                <CreateFolder
                  node={
                    <button
                      className="w-full px-4 py-2 hover:bg-gray-100 flex items-center gap-3"
                      onClick={() => setIsOpen(false)}
                    >
                      <span className="text-gray-600 text-[14px]">
                        <FaFolder className="w-5 h-5" />
                      </span>
                      <span className="text-gray-700 text-[14px]">
                        Thêm thư mục
                      </span>
                    </button>
                  }
                  parentId={Number(params.parentId)}
                  projectId={Number(params.id)}
                />
                <UploadFile
                  folderId={Number(params.parentId)}
                  projectId={Number(params.id)}
                  node={
                    <button
                      className="w-full px-4 py-2 hover:bg-gray-100 flex items-center gap-3"
                      onClick={() => setIsOpen(false)}
                    >
                      <span className="text-gray-600 text-[14px]">
                        <FaUpload className="w-5 h-5" />
                      </span>
                      <span className="text-gray-700 text-[14px]">
                        Thêm tệp
                      </span>
                    </button>
                  }
                />
              </div>
            </div>
          </div>
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
        <AppBreadcrumb
          pathList={dataPath!.data.map((item) => {
            return {
              name: item.name,
              url: `${item.folderId}`,
            };
          })}
          className="mt-2"
        />
      </div>
      <div className="-mx-4 flex-1 overflow-auto px-4 py-4 lg:flex-row">
        {localStorage.getItem("viewModeData") === "table" && ( //+
          <TableStorage
            projectId={Number(params.id)}
            data={data!.data.sort((a, b) => {
              const nameComparison = b.createdAt!.localeCompare(a.createdAt!);
              return nameComparison !== 0 ? nameComparison : b.id! - a.id!;
            })}
          />
        )}
        {localStorage.getItem("viewModeData") === "grid" && (
          <GridStorage
            projectId={Number(params.id)}
            data={data!.data.sort((a, b) => {
              const nameComparison = b.createdAt!.localeCompare(a.createdAt!);
              return nameComparison !== 0 ? nameComparison : b.id! - a.id!;
            })}
          />
        )}
      </div>
    </>
  );
}
