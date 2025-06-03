import groupApiRequest from "@/apis/group.api";
import priorityApiRequest from "@/apis/priority.api";
import statusApiRequest from "@/apis/status.api";
import teamApiRequest from "@/apis/team.api";
import todoApiRequest from "@/apis/todo.api";
import typeApiRequest from "@/apis/type.api";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  Sheet,
  SheetClose,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import { Textarea } from "@/components/ui/textarea";
import {
  Todo,
  todoDefault,
  todoSchema,
} from "@/data/schema/Project/todo.schema";
import { handleSuccessApi } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link as LucideLink, Pencil, Trash } from "lucide-react";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import SelectMulti from "react-select";

import tagApiRequest from "@/apis/tag.api";
import { Button } from "@/components/custom/button";
import { Role } from "@/data/enums/role.enum";
import { File } from "@/data/schema/Project/file.schema";
import { View } from "@/data/schema/Project/view.schema";
import { useRole } from "@/hooks/use-role";
import DeleteFormTodo from "./delete-form-todo";
import FileReferenceDialog from "./file-view-choose";
import TabView from "./tab-view";
import { UserProjectStatus } from "@/data/enums/userprojectstatus.enum";

interface Props {
  mode: "ADD" | "VIEW" | "UPDATE";
  setMode: (value: "ADD" | "UPDATE" | "VIEW") => void;
  setIsOpen: (value: boolean | Todo) => void;
  isOpen: boolean | Todo | undefined;
  projectId: number;
}

interface AssignTo {
  name: string;
  id: string;
}

export function UpsertTodo({
  mode,
  setIsOpen,
  isOpen,
  projectId,
  setMode,
}: Props) {
  const queryClient = useQueryClient();
  const { roleDetail } = useRole();
  const [assignTo, setAssignTo] = useState<AssignTo[]>([]);
  const [selectedTagIds, setSelectedTagIds] = useState<
    { label: string; value: number }[]
  >([]);
  const [isOpenDialog, setIsOpenDialog] = useState<boolean>(false);
  const [selectedFiles, setSelectedFiles] = useState<File[]>([]);
  const [selectedViews, setSelectedViews] = useState<View[]>([]);
  const [disable, setDisable] = useState<boolean>(true);
  const onSubmit = (values: Todo) => {
    values.projectId = projectId;
    values.tagIds = selectedTagIds.map((item) => item.value);
    values.assignTo = Number(values.assignToString?.slice(1));
    values.isAssignToGroup = Number(values.assignToString?.[0]);
    values.fileIds = selectedFiles.map((item) => item.id!);
    values.viewIds = selectedViews.map((item) => item.id!);
    console.log(values);
    if (mode == "ADD") mutateCreate(values);
    else if (mode === "UPDATE") mutateUpdate(values);
  };
  const form = useForm<Todo>({
    resolver: zodResolver(todoSchema),
    defaultValues: todoDefault,
  });

  const { data: dataType, isLoading: isLoadingType } = useQuery({
    queryKey: ["get-list-type"],
    queryFn: () => typeApiRequest.getList(projectId),
  });

  const { data: dataStatus, isLoading: isLoadingStatus } = useQuery({
    queryKey: ["get-list-status"],
    queryFn: () => statusApiRequest.getList(projectId),
  });

  const { data: dataPriority, isLoading: isLoadingPriority } = useQuery({
    queryKey: ["get-list-priority"],
    queryFn: () => priorityApiRequest.getList(projectId),
  });

  const { data: dataGroup, isLoading: isLoadingGroup } = useQuery({
    queryKey: ["get-list-group"],
    queryFn: () => groupApiRequest.getList(projectId),
  });

  const { data: dataUser, isLoading: isLoadingUser } = useQuery({
    queryKey: ["get-list-user"],
    queryFn: () => teamApiRequest.getUsersByProjectId(projectId),
  });

  const { data: dataTag } = useQuery({
    queryKey: ["get-list-tag"],
    queryFn: () => tagApiRequest.getList(projectId),
  });

  useEffect(() => {
    if (dataGroup && dataUser) {
      const userIds = dataUser.data.filter((item) => item.userProjectStatus == UserProjectStatus.Active).map((item) => {
        return {
          name: item.fullName,
          id: "0" + item.id,
        } as AssignTo;
      });
      const groupIds = dataGroup.data.map((item) => {
        return {
          name: item.name,
          id: "1" + item.id,
        } as AssignTo;
      });
      setAssignTo([...userIds, ...groupIds]);
    }
  }, [dataGroup, dataUser, isLoadingGroup, isLoadingUser]);

  useEffect(() => {
    if (mode == "UPDATE" || mode == "ADD") setDisable(false);
    if (mode == "UPDATE" || mode == "VIEW") {
      if (typeof isOpen == "object") {
        form.setValue("id", isOpen?.id);
        form.setValue("name", isOpen?.name);
        form.setValue("description", isOpen?.description);
        form.setValue("typeId", isOpen?.type?.id);
        form.setValue("statusId", isOpen?.status?.id);
        form.setValue("priorityId", isOpen?.priority?.id);
        form.setValue(
          "startDate",
          new Date(isOpen?.startDate ?? "").toISOString().split("T")[0]
        );
        form.setValue(
          "dueDate",
          new Date(isOpen?.dueDate ?? "").toISOString().split("T")[0]
        );
        form.setValue(
          "assignToString",
          (isOpen?.isAssignToGroup ?? "").toString() +
            (isOpen?.assignTo ?? "").toString()
        );
        if (isOpen && isOpen.tags && isOpen.tags.length > 0) {
          const tagIds = isOpen.tags.map((item) => {
            return {
              label: item.name,
              value: item.id!,
            };
          });
          setSelectedTagIds(tagIds);
        }
        if (isOpen?.files !== undefined && isOpen?.files !== null){
            setSelectedFiles(isOpen?.files);
        }
        if (isOpen?.views !== undefined && isOpen?.views !== null){
            setSelectedViews(isOpen?.views);
        }
      }
    }
  }, [isOpen, mode]);

  const { mutate: mutateCreate, isPending: isPendingCreate } = useMutation({
    mutationKey: ["create-todo"],
    mutationFn: (todo: Todo) => todoApiRequest.create(todo),
    onSuccess: () => {
      handleSuccessApi({
        title: "Tạo mới việc cần làm",
        message: "Chế độ xem được tạo thành công",
      });
      setIsOpen(false);
      queryClient.invalidateQueries({ queryKey: ["get-list-todo"] });
    },
  });
  const { mutate: mutateUpdate, isPending: isPendingUpdate } = useMutation({
    mutationKey: ["update-todo"],
    mutationFn: (todo: Todo) => todoApiRequest.update(todo.id!, todo),
    onSuccess: () => {
      handleSuccessApi({
        title: "Chỉnh sửa việc cần làm",
        message: "Chế độ xem được chỉnh sửa thành công",
      });
      setIsOpen(false);
      queryClient.invalidateQueries({ queryKey: ["get-list-todo"] });
    },
  });
  return (
    <Sheet open={!!isOpen} onOpenChange={setIsOpen}>
      <SheetTrigger asChild>{}</SheetTrigger>
      <SheetContent className="w-[600px] overflow-y-auto">
        <SheetHeader>
          <SheetTitle>
            <div className="flex items-center justify-between mt-6">
              <p>Việc cần làm</p>
              {roleDetail?.role == Role.Admin ||
              (typeof isOpen == "object" &&
                isOpen.createdBy == roleDetail?.id) ? (
                <div className="flex items-center">
                  <DeleteFormTodo
                    node={<Trash className="w-3.5 h-3.5 cursor-pointer" />}
                    id={typeof isOpen == "object" ? isOpen.id! : 0}
                  />
                  <Pencil
                    className="w-3.5 h-3.5 cursor-pointer ml-2"
                    onClick={() => {
                      setDisable(!disable);
                      setMode(mode == "UPDATE" ? "VIEW" : "UPDATE");
                    }}
                  />
                </div>
              ) : (
                <></>
              )}
            </div>
          </SheetTitle>
          <SheetDescription>Tạo việc cần làm của bạn ở đây</SheetDescription>
        </SheetHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <div className="flex flex-col space-y-1.5 flex-1 mt-3">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Tên</FormLabel>
                    <FormControl>
                      <Input
                        disabled={disable}
                        placeholder="Sửa lại tên file"
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1 mt-3">
              <FormField
                control={form.control}
                name="description"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Mô tả</FormLabel>
                    <FormControl>
                      <Textarea
                        disabled={disable}
                        placeholder="Nhập mô tả cho việc cần làm"
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 mt-4">
              <FormField
                control={form.control}
                name="typeId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Phân loại</FormLabel>
                    <FormControl>
                      <Select
                        disabled={disable}
                        key={field.value}
                        value={field.value?.toString()} // Lấy giá trị từ field
                        onValueChange={(value) => {
                          console.log(value);
                          field.onChange(Number(value));
                        }}
                      >
                        <SelectTrigger id="type">
                          <SelectValue placeholder="Phân loại" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          {!isLoadingType && dataType
                            ? dataType.data.map((item) => {
                                return (
                                  <SelectItem
                                    key={item.id}
                                    value={item.id!.toString() ?? ""}
                                  >
                                    {item.name}
                                  </SelectItem>
                                );
                              })
                            : []}
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 mt-4">
              <FormField
                control={form.control}
                name="statusId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Trạng thái</FormLabel>
                    <FormControl>
                      <Select
                        disabled={disable}
                        key={field.value}
                        value={field.value?.toString()} // Lấy giá trị từ field
                        onValueChange={(value) => {
                          field.onChange(Number(value));
                        }}
                      >
                        <SelectTrigger id="status">
                          <SelectValue placeholder="Trạng thái" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          {!isLoadingStatus && dataStatus
                            ? dataStatus.data.map((item) => {
                                return (
                                  <SelectItem
                                    key={item.id}
                                    value={item.id!.toString() ?? ""}
                                  >
                                    {item.name}
                                  </SelectItem>
                                );
                              })
                            : []}
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 mt-4">
              <FormField
                control={form.control}
                name="priorityId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Mức độ cần thiết</FormLabel>
                    <FormControl>
                      <Select
                        disabled={disable}
                        key={field.value}
                        value={field.value?.toString()} // Lấy giá trị từ field
                        onValueChange={(value) => {
                          field.onChange(Number(value));
                        }}
                      >
                        <SelectTrigger id="priority">
                          <SelectValue placeholder="Mức độ cần thiết" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          {!isLoadingPriority && dataPriority
                            ? dataPriority.data.map((item) => {
                                return (
                                  <SelectItem
                                    key={item.id}
                                    value={item.id!.toString() ?? ""}
                                  >
                                    {item.name}
                                  </SelectItem>
                                );
                              })
                            : []}
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="grid grid-cols-2 gap-4 mt-4">
              <FormField
                control={form.control}
                name="startDate"
                render={({ field }) => (
                  <FormItem className="space-y-1">
                    <FormLabel>Ngày bắt đầu</FormLabel>
                    <FormControl>
                      <Input
                        disabled={disable}
                        type="date"
                        {...field}
                        value={field.value ? field.value : ""} // Hiển thị giá trị mặc định nếu có
                        onChange={(e) => {
                          const value = e.target.value; // Giá trị là chuỗi (string)
                          field.onChange(value); // Cập nhật giá trị vào form
                        }}
                      />
                    </FormControl>
                    <FormMessage /> {/* Hiển thị thông báo lỗi từ Zod */}
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="dueDate"
                render={({ field }) => (
                  <FormItem className="space-y-1">
                    <FormLabel>Ngày kết thúc</FormLabel>
                    <FormControl>
                      <Input
                        disabled={disable}
                        type="date"
                        {...field}
                        value={field.value ? field.value : ""} // Hiển thị giá trị mặc định nếu có
                        onChange={(e) => {
                          const value = e.target.value; // Giá trị là chuỗi (string)
                          field.onChange(value); // Cập nhật giá trị vào form
                        }}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 mt-4">
              <FormField
                control={form.control}
                name="assignToString"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Giao việc</FormLabel>
                    <FormControl>
                      <Select
                        disabled={disable}
                        key={field.value}
                        value={field.value?.toString()} // Lấy giá trị từ field
                        onValueChange={(value) => {
                          console.log(value);
                          field.onChange(value);
                        }}
                      >
                        <SelectTrigger id="assignTo">
                          <SelectValue placeholder="Giao việc cho" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          {assignTo.map((item, index) => {
                            return (
                              <SelectItem
                                key={index}
                                value={item.id.toString() ?? ""}
                              >
                                {item.name} (
                                {item.id.toString()[0] == "0"
                                  ? "Cá nhân"
                                  : "Nhóm"}
                                )
                              </SelectItem>
                            );
                          })}
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 mt-4">
              <Label htmlFor="tags">Nhãn dán</Label>
              <SelectMulti
                isDisabled={disable}
                isMulti
                options={dataTag?.data.map((item) => ({
                  label: item.name,
                  value: item.id!,
                }))}
                value={selectedTagIds}
                className="w-full"
                placeholder="Thêm nhãn dán"
                onChange={(newValue: any) => setSelectedTagIds([...newValue])} // Chuyển từ readonly sang mảng mutable
              />
            </div>
            <div
              onClick={() => setIsOpenDialog(!isOpenDialog)}
              className={`flex items-center space-x-2 cursor-pointer mt-4 ${
                mode !== "UPDATE" ? "hidden" : ""
              }`}
            >
              <LucideLink className="w-4 h-4 text-gray-500" />
              <p className="text-sm font-medium text-gray-700">
                Thêm liên kết tới các tệp & chế độ xem
              </p>
            </div>
            {isOpenDialog ? (
              <FileReferenceDialog
                isOpen={isOpenDialog}
                setIsOpen={setIsOpenDialog}
                projectId={projectId}
                selectedFiles={selectedFiles}
                setSelectedFiles={setSelectedFiles}
                selectedViews={selectedViews}
                setSelectedViews={setSelectedViews}
              />
            ) : (
              <></>
            )}

            {mode != "VIEW" ? (
              <SheetFooter className="mt-4">
                <SheetClose asChild>
                  <Button variant="outline">Hủy</Button>
                </SheetClose>
                <Button
                  loading={mode == "ADD" ? isPendingCreate : isPendingUpdate}
                  type="submit"
                >
                  {mode == "ADD" ? "Thêm" : "Cập nhật"}
                </Button>
              </SheetFooter>
            ) : (
              <></>
            )}
          </form>
        </Form>
        {mode == "VIEW" && typeof isOpen == "object" ? (
          <TabView
            todoId={isOpen?.id ?? 0}
            views={selectedViews ?? []}
            files={selectedFiles ?? []}
          />
        ) : (
          <></>
        )}
      </SheetContent>
    </Sheet>
  );
}
