import teamApiRequest from "@/apis/team.api";
import { Button } from "@/components/custom/button";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Card } from "@/components/ui/card";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Sheet, SheetContent, SheetTrigger } from "@/components/ui/sheet";
import { Role } from "@/data/enums/role.enum";
import { UserProjectStatus } from "@/data/enums/userprojectstatus.enum";
import { UserProject } from "@/data/schema/Project/userproject.schema";
import { handleSuccessApi } from "@/lib/utils";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { Mail } from "lucide-react";
import { ReactNode, useState } from "react";
import FormDeleteUser from "./form-delete-user";

type FormProps = {
  node: ReactNode;
  userProject: UserProject;
  currentRole: Role;
  projectId: number;
  isInGroup: boolean;
  currentUserId: number;
  groupId: number;
};

export default function UserInfoSheet({
  node,
  userProject,
  currentRole,
  projectId,
  isInGroup,
  currentUserId,
  groupId,
}: FormProps) {
  console.log(currentUserId)
  const [role, setRole] = useState<Role>(userProject.role);
  const queryClient = useQueryClient();
  const { mutate: mutateUpdate, isPending: isPendingUpdate } = useMutation({
    mutationKey: ["change-role"],
    mutationFn: () =>
      teamApiRequest.changeRole({
        projectId: projectId,
        role: role,
        email: userProject.email,
        userId: userProject.id,
      }),
    onSuccess: () => {
      handleSuccessApi({
        title: "Cập nhât thành công vị trí",
        message: "Bạn đã cập nhật thành công vị trí",
      });
      queryClient.invalidateQueries({ queryKey: ["users-project"] });
      queryClient.invalidateQueries({ queryKey: ["role"] });
    },
  });

  return (
    <Sheet>
      <SheetTrigger asChild>{node}</SheetTrigger>
      <SheetContent className="w-[400px]">
        <div className="flex flex-col items-center space-y-3 py-4">
          <Avatar className="h-20 w-20">
            <AvatarImage src={userProject.imageUrl} alt="@shadcn" />
            <AvatarFallback>{userProject.fullName!.charAt(0)}</AvatarFallback>
          </Avatar>
          <div className="text-center">
            <h2 className="text-lg font-semibold">{userProject.fullName!}</h2>
            <a
              href="mailto:phucminhbeos@gmail.com"
              className="text-blue-500 flex items-center gap-2"
            >
              <Mail className="h-4 w-4" /> {userProject.email}
            </a>
          </div>
        </div>
        <Card className="p-4 space-y-2">
          <p className="text-sm font-semibold">Chi tiết</p>
          <p className="text-sm text-gray-500">
            Ngày tham gia:{" "}
            <span className="font-medium">{userProject.dateJoined}</span>
          </p>
          <p className="text-sm text-gray-500">
            Trạng thái:{" "}
            <span className="font-medium text-green-600">
              {userProject.userProjectStatus == UserProjectStatus.Active
                ? "Đã vào"
                : "Đang chờ"}
            </span>
          </p>
          <p className="text-sm text-gray-500">
            Vị trí:
            <Select
              disabled={currentRole == Role.Admin ? false : true}
              value={role.toString()}
              onValueChange={(value) => {
                const enumValue = parseInt(value, 10); // Chuyển giá trị chuỗi về kiểu số
                setRole(enumValue); // Truyền lại giá trị enum số vào field
              }}
            >
              <SelectTrigger className="w-full mt-1">
                <SelectValue placeholder="Chọn vị trí" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value={Role.Member.toString()}>
                  Người dùng
                </SelectItem>
                <SelectItem value={Role.Admin.toString()}>
                  Quản trị viên
                </SelectItem>
              </SelectContent>
            </Select>
          </p>
        </Card>

        <div className="flex justify-start mt-4">
          {currentRole == Role.Admin && (
            <Button loading={isPendingUpdate} onClick={() => mutateUpdate()}>
              Lưu thay đổi
            </Button>
          )}
          <FormDeleteUser
            groupId={groupId}
            projectId={projectId}
            userProject={userProject}
            node={
              <Button className="ml-4" variant="destructive">
                {isInGroup == true ? "Rời nhóm" : "Rời dự án"}
              </Button>
            }
          />
        </div>
      </SheetContent>
    </Sheet>
  );
}
