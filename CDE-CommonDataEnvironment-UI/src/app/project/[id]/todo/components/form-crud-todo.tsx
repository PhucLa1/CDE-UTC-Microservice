import todoApiRequest from "@/apis/todo.api";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Link as LucideLink } from 'lucide-react';
import { Input } from "@/components/ui/input";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
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
import {
  Todo,
  todoDefault,
  todoSchema,
} from "@/data/schema/Project/todo.schema";
import { handleSuccessApi } from "@/lib/utils";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useForm } from "react-hook-form";
import { Textarea } from "@/components/ui/textarea";
import typeApiRequest from "@/apis/type.api";
import statusApiRequest from "@/apis/status.api";
import priorityApiRequest from "@/apis/priority.api";
import { useEffect, useState } from "react";
import groupApiRequest from "@/apis/group.api";
import teamApiRequest from "@/apis/team.api";

interface Props {
  mode: "ADD" | "DELETE" | "UPDATE";
  setIsOpen: (value: boolean | Todo) => void;
  isOpen: boolean | Todo | undefined;
  projectId: number;
}

interface AssignTo {
  name: string;
  id: string;
}

export function UpsertTodo({ mode, setIsOpen, isOpen, projectId }: Props) {
  const queryClient = useQueryClient();
  const [assignTo, setAssignTo] = useState<AssignTo[]>([])
  const onSubmit = (values: Todo) => {
    console.log(values);
    // if(state === State.CREATE) mutateCreate(values);
    // else if(state === State.UPDATE) mutateUpdate(values)
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

  useEffect(() => {
    if(dataGroup && dataUser){
      const userIds = dataUser.data.map((item, _) => {
        return {
          name: item.fullName,
          id: '0' + item.id,
        } as AssignTo
      })
      const groupIds = dataGroup.data.map((item, _) => {
        return {
          name: item.name,
          id: '1' + item.id,
        } as AssignTo
      })
      setAssignTo([...userIds, ...groupIds])
    }
  }, [dataGroup, dataUser, isLoadingGroup, isLoadingUser])

  const { mutate, isPending } = useMutation({
    mutationKey: ["create-todo"],
    mutationFn: (todo: Todo) => todoApiRequest.create(todo),
    onSuccess: () => {
      handleSuccessApi({
        title: "Tạo mới việc cần làm",
        message: "Chế độ xem được tạo thành công",
      });
    },
  });
  return (
    <Sheet  open={!!isOpen} onOpenChange={setIsOpen}>
      <SheetTrigger asChild>{}</SheetTrigger>
      <SheetContent className="w-[500px]">
        <SheetHeader>
          <SheetTitle>Việc cần làm</SheetTitle>
          <SheetDescription>Tạo việc cần làm của bạn ở đây</SheetDescription>
        </SheetHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <div className="flex flex-col space-y-1.5 flex-1 mt-2">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Tên</FormLabel>
                    <FormControl>
                      <Input placeholder="Sửa lại tên file" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1 mt-2">
              <FormField
                control={form.control}
                name="description"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Mô tả</FormLabel>
                    <FormControl>
                      <Textarea
                        placeholder="Nhập mô tả cho việc cần làm"
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 mt-2">
              <FormField
                control={form.control}
                name="typeId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Phân loại</FormLabel>
                    <FormControl>
                      <Select
                        key={field.value}
                        value={field.value?.toString()} // Lấy giá trị từ field
                        onValueChange={(value) => {
                          console.log(value)
                          field.onChange(Number(value));
                        }}
                      >
                        <SelectTrigger id="type">
                          <SelectValue placeholder="Phân loại" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          {!isLoadingType && dataType
                            ? dataType.data.map((item, _) => {
                                return (
                                  <SelectItem key={item.id} value={item.id!.toString() ?? ''}>
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
            <div className="flex flex-col space-y-1.5 mt-2">
              <FormField
                control={form.control}
                name="statusId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Trạng thái</FormLabel>
                    <FormControl>
                      <Select
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
                            ? dataStatus.data.map((item, _) => {
                                return (
                                  <SelectItem key={item.id} value={item.id!.toString() ?? ''}>
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
            <div className="flex flex-col space-y-1.5 mt-2">
              <FormField
                control={form.control}
                name="priorityId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Mức độ cần thiết</FormLabel>
                    <FormControl>
                      <Select
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
                            ? dataPriority.data.map((item, _) => {
                                return (
                                  <SelectItem key={item.id} value={item.id!.toString() ?? ''}>
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
            <div className="grid grid-cols-2 gap-4 mt-2">
                                <FormField
                                    control={form.control}
                                    name="startDate"
                                    render={({ field }) => (
                                        <FormItem className="space-y-1">
                                            <FormLabel>Ngày bắt đầu</FormLabel>
                                            <FormControl>
                                                <Input
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
            <div className="flex flex-col space-y-1.5 mt-2">
              <FormField
                control={form.control}
                name="priorityId"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Giao việc</FormLabel>
                    <FormControl>
                      <Select
                        key={field.value}
                        value={field.value?.toString()} // Lấy giá trị từ field
                        onValueChange={(value) => {
                          console.log(value)
                          field.onChange(value);
                        }}
                      >
                        <SelectTrigger id="assignTo">
                          <SelectValue placeholder="Giao việc cho" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          {assignTo.map((item, _) => {
                                return (
                                  <SelectItem key={item.id} value={item.id.toString() ?? ''}>
                                    {item.name} ({item.id.toString()[0] == '0' ? 'Cá nhân' : 'Nhóm'})
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
            <div className="flex flex-col space-y-1.5 mt-2">
            <Select
                            isMulti
                            options={data!.data
                                .map(item => ({ label: item.name, value: item.id! }))}
                            value={selectedTagIds}
                            className="w-full"
                            placeholder="Thêm nhãn dán"
                            onChange={(newValue) => setSelectedTagIds([...newValue])} // Chuyển từ readonly sang mảng mutable
                        />
                        </div>
            <div className="flex items-center space-x-2 mt-2 cursor-pointer">
              <LucideLink className="w-4 h-4 text-gray-500" />
              <p className="text-sm font-medium text-gray-700">Thêm liên kết tới các tệp & chế độ xem</p>
            </div>

            <SheetFooter>
              <SheetClose>Hủy</SheetClose>
              {/* <Button loading={isPending} type="submit">
                Tiếp tục
              </Button> */}
            </SheetFooter>
          </form>
        </Form>
      </SheetContent>
    </Sheet>
  );
}
