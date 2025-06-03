"use client";
import projectApiRequest from "@/apis/project.api";
import AppBreadcrumb, { PathItem } from "@/components/custom/_breadcrumb";
import { Button } from "@/components/custom/button";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle
} from "@/components/ui/card";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import {
  ProjectDetail,
  projectDetailDefault,
  projectDetailSchema,
} from "@/data/schema/Project/projectdetail.schema";
import { handleSuccessApi } from "@/lib/utils";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import DeleteProject from "./_component/delete-project";
import LeaveProject from "./_component/leave-project";
const pathList: Array<PathItem> = [
  {
    name: "Chi tiết dự án",
    url: "#",
  },
];
export default function Page({ params }: { params: { id: string } }) {
  const queryClient = useQueryClient();

  const form = useForm<ProjectDetail>({
    resolver: zodResolver(projectDetailSchema),
    defaultValues: projectDetailDefault,
  });
  const { data, isLoading } = useQuery({
    queryKey: ["get-detail-project"],
    queryFn: () => projectApiRequest.getDetail(Number(params.id)),
  });

  const { mutate, isPending } = useMutation({
    mutationKey: ["update-project"],
    mutationFn: (body: FormData) => projectApiRequest.updateProject(body),
    onSuccess: () => {
      handleSuccessApi({
        title: "Cập nhật dự án thành công",
        message: "Bạn đã cập nhật dự án thành công",
      });
      queryClient.invalidateQueries({ queryKey: ["get-detail-project"] });
    },
  });

  useEffect(() => {
    if (data) {
      form.setValue("id", Number(params.id));
      form.setValue("name", data.data.name);
      form.setValue("description", data.data.description);
      form.setValue("createdAt", data.data.createdAt);
      form.setValue("updatedAt", data.data.updatedAt);
      form.setValue("imageUrl", data.data.imageUrl);
      form.setValue("ownership", data.data.ownership);
      form.setValue("userCount", data.data.userCount);
      form.setValue("folderCount", data.data.folderCount);
      form.setValue("fileCount", data.data.fileCount);
      form.setValue("size", data.data.size);
      form.setValue("startDate", data.data.startDate.toString().split("T")[0]);
      form.setValue("endDate", data.data.endDate.toString().split("T")[0]);
      // form.setValue("image", new File([], "default.png"))
    }
  }, [data]);
  const onSubmit = (values: ProjectDetail) => {
    const formData = new FormData();
    formData.append("id", params.id.toString());
    formData.append("name", form.getValues("name"));
    formData.append("description", form.getValues("description"));
    formData.append("image", form.getValues("image"));
    formData.append("startDate", form.getValues("startDate"));
    formData.append("endDate", form.getValues("endDate"));
    console.log(values);
    mutate(formData);
  };
  const onInvalid = (errors: any) => {
    console.log("Form validation errors:", errors);
  };
  if (isLoading) return <></>;
  return (
    <>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit, onInvalid)}
          className="space-y-8"
        >
          <div className="mb-2 flex items-center justify-between space-y-2">
            <div>
              <h2 className="text-2xl font-bold tracking-tight">
                Chi tiết dự án
              </h2>
              <AppBreadcrumb pathList={pathList} className="mt-2" />
            </div>
            <div>
              <Button type="submit" loading={isPending}>
                Lưu
              </Button>
            </div>
          </div>
          <div className="-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row">
            <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-8">
              <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                <CardHeader>
                  <CardTitle>Thông tin cơ bản</CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="grid w-full items-center gap-4">
                    <div className="flex flex-col space-y-1.5 flex-1">
                      <FormField
                        control={form.control}
                        name="name"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Tên dự án</FormLabel>
                            <FormControl>
                              <Input placeholder="" {...field} />
                            </FormControl>
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="flex flex-col space-y-1.5 flex-1">
                      <FormField
                        control={form.control}
                        name="description"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Mô tả</FormLabel>
                            <FormControl>
                              <Textarea placeholder="" {...field} />
                            </FormControl>
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="flex flex-col space-y-1.5 flex-1">
                      <FormField
                        control={form.control}
                        name="ownership"
                        render={({ field }) => (
                          <FormItem>
                            <FormLabel>Chủ sở hữu</FormLabel>
                            <FormControl>
                              <span className="text-xs block text-gray-600">
                                {field.value || "Không có chủ sỡ hữu"}
                              </span>
                            </FormControl>
                            <FormMessage />
                          </FormItem>
                        )}
                      />
                    </div>
                    <div className="flex space-x-4">
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="createdAt"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Ngày tạo dự án</FormLabel>
                              <FormControl>
                                <span className="text-xs block text-gray-600">
                                  {field.value || "Không có chủ sỡ hữu"}
                                </span>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="updatedAt"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Lần sửa cuối cùng</FormLabel>
                              <FormControl>
                                <span className="text-xs block text-gray-600">
                                  {field.value || "Không có chủ sỡ hữu"}
                                </span>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                    </div>
                    <div className="flex space-x-4">
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="userCount"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Thành viên</FormLabel>
                              <FormControl>
                                <span className="text-xs block text-gray-600">
                                  {field.value}
                                </span>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="fileCount"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Tệp</FormLabel>
                              <FormControl>
                                <span className="text-xs block text-gray-600">
                                  {field.value}
                                </span>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="folderCount"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Thư mục</FormLabel>
                              <FormControl>
                                <span className="text-xs block text-gray-600">
                                  {field.value}
                                </span>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="size"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Kích thước</FormLabel>
                              <FormControl>
                                <span className="text-xs block text-gray-600">
                                  {field.value} MB
                                </span>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                    </div>
                  </div>
                </CardContent>
              </Card>
              <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                <CardHeader>
                  <CardTitle>Cấu hình dự án</CardTitle>
                </CardHeader>
                <CardContent>
                  <div className="grid w-full items-center gap-4">
                    <div className="flex flex-col space-y-1.5 flex-1">
                      <FormField
                        control={form.control}
                        name="image"
                        render={({ field }) => {
                          const imageUrl = data?.data.imageUrl; // Lấy URL từ form nếu có
                          return (
                            <FormItem className="space-y-1">
                              <FormLabel>Ảnh dự án</FormLabel>
                              <FormControl>
                                <div className="relative w-128 h-64 border-2 border-dashed border-gray-300 rounded-lg flex justify-center items-center">
                                  {field.value && field.value.size > 0 ? (
                                    <img
                                      src={URL.createObjectURL(field.value)}
                                      alt="Selected"
                                      className="w-full h-full object-cover rounded-lg"
                                    />
                                  ) : imageUrl ? (
                                    <img
                                      src={imageUrl}
                                      alt="Project"
                                      className="w-full h-full object-cover rounded-lg"
                                    />
                                  ) : (
                                    <span className="text-gray-500">
                                      Nhấn vào đây để chọn ảnh
                                    </span>
                                  )}
                                  <Input
                                    type="file"
                                    accept="image/*"
                                    onChange={(e) => {
                                      const file = e.target.files?.[0];
                                      field.onChange(file ?? field.value); // Nếu không chọn ảnh mới, giữ nguyên giá trị cũ
                                    }}
                                    className="absolute inset-0 w-full h-full opacity-0 cursor-pointer"
                                  />
                                </div>
                              </FormControl>
                              <FormMessage />{" "}
                              {/* Hiển thị thông báo lỗi từ Zod */}
                            </FormItem>
                          );
                        }}
                      />
                    </div>
                    <div className="flex space-x-4">
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="startDate"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Ngày bắt đầu dự án</FormLabel>
                              <FormControl>
                                <Input type="date" {...field}></Input>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                      <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                          control={form.control}
                          name="endDate"
                          render={({ field }) => (
                            <FormItem>
                              <FormLabel>Ngày kết thúc dự án</FormLabel>
                              <FormControl>
                                <Input type="date" {...field}></Input>
                              </FormControl>
                              <FormMessage />
                            </FormItem>
                          )}
                        />
                      </div>
                    </div>
                  </div>
                </CardContent>
              </Card>
            </div>
          </div>
        </form>
      </Form>
      <div className="flex items-center gap-2 mb-4">
        <DeleteProject id={Number(params.id)} />
        <LeaveProject id={Number(params.id)} />
      </div>
    </>
  );
}
