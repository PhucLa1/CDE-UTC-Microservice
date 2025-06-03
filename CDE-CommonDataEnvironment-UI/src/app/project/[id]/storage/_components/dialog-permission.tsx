"use client";
import storageApiRequest from "@/apis/storage.api";
import { TargetResult } from "@/apis/team.api";
import {
  AlertDialog,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Permission } from "@/data/enums/permisson.enum";
import { StoragePermissionResult } from "@/data/schema/Project/storagepermisson.schema";
import { handleSuccessApi } from "@/lib/utils";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { X } from "lucide-react";
import { ReactNode, useState } from "react";

interface Props {
  node: ReactNode;
  targets: TargetResult[]; // TargetResult should contain: id, name, isGroup,
  access: Permission;
  isFile: boolean; // Optional, if you need to differentiate between file and folder permissions
  id: number;
  isOpenPermissions: boolean;
  setIsOpenPermissions: (isOpen: boolean) => void;
  storagePermissionResults: StoragePermissionResult[]; // Optional, if you want to pre-populate selected users
}

export default function DialogPermissions({
  node,
  targets,
  access,
  isFile,
  id,
  setIsOpenPermissions,
  isOpenPermissions,
  storagePermissionResults,
}: Props) {
  const queryClient = useQueryClient();
  const [selectedUsers, setSelectedUsers] = useState<StoragePermissionResult[]>(
    storagePermissionResults
  );
  const [selectedPermission, setSelectedPermission] =
    useState<Permission>(access);

  const availableTargets = targets.filter(
    (t) => !selectedUsers.some((u) => u.targetId === t.id)
  );

  const handleAddUser = (value: string) => {
    const user = targets.find((t) => t.id.toString() === value);
    if (!user) return;

    setSelectedUsers((prev) => [
      ...prev,
      {
        name: user.name,
        access: Permission.Write, // Default access can be set here
        url: user.url ?? "",
        email: user.email ?? "",
        targetId: user.id,
      },
    ]);
  };

  const handleChangePermission = (id: number, permission: Permission) => {
    setSelectedUsers((prev) =>
      prev.map((u) => (u.targetId === id ? { ...u, access: permission } : u))
    );
  };

  const handleRemoveUser = (id: number) => {
    setSelectedUsers((prev) => prev.filter((u) => u.targetId !== id));
  };

  const { mutate, isPending } = useMutation({
    mutationKey: ["update-permission"],
    mutationFn: () => {
      console.log({
        id: id,
        isFile: isFile,
        access: selectedPermission,
        targetPermission: selectedUsers.reduce((acc, u) => {
          acc[u.targetId] = u.access;
          return acc;
        }, {} as Record<number, Permission>),
      });
      return storageApiRequest.updateStoragePermission({
        id: id,
        isFile: isFile,
        access: selectedPermission,
        targetPermission: selectedUsers.reduce((acc, u) => {
          acc[u.targetId] = u.access;
          return acc;
        }, {} as Record<number, Permission>),
      });
    },
    onSuccess: () => {
      handleSuccessApi({
        title: "Cập nhật quyền thành công",
        message: "Quyền truy cập đã được cập nhật thành công.",
      });
      isFile
        ? queryClient.invalidateQueries({
            queryKey: ["get-detail-file"],
          })
        : queryClient.invalidateQueries({
            queryKey: ["get-detail-folder"],
          });

      setIsOpenPermissions(false); // ✅ Đóng dialog khi API thành công
    },
  });

  return (
    <AlertDialog open={isOpenPermissions} onOpenChange={setIsOpenPermissions}>
      <AlertDialogTrigger asChild>{node}</AlertDialogTrigger>
      <AlertDialogContent className="max-h-[90vh] overflow-y-auto focus:outline-none">
        <AlertDialogHeader>
          <AlertDialogTitle>Chỉnh quyền với lưu trữ này?</AlertDialogTitle>
          <AlertDialogDescription />
        </AlertDialogHeader>

        <div className="space-y-3">
          <h2 className="text-lg font-semibold">Quyền cho lưu trữ này</h2>

          <RadioGroup
            className="flex items-center justify-between"
            value={selectedPermission.toString()}
            onValueChange={(val) => setSelectedPermission(Number(val))}
          >
            <div className="flex items-center space-x-2">
              <RadioGroupItem value={Permission.Write.toString()} id="full" />
              <Label htmlFor="full">Quyền ghi</Label>
            </div>
            <div className="flex items-center space-x-2">
              <RadioGroupItem value={Permission.Read.toString()} id="read" />
              <Label htmlFor="read">Chỉ đọc</Label>
            </div>
            <div className="flex items-center space-x-2">
              <RadioGroupItem
                value={Permission.NoAccess.toString()}
                id="none"
              />
              <Label htmlFor="none">Không có quyền</Label>
            </div>
          </RadioGroup>

          <div>
            <Label className="text-sm">
              Cấp quyền truy cập cho người dùng hoặc nhóm
            </Label>
            <Select onValueChange={handleAddUser}>
              <SelectTrigger className="w-full mt-2">
                <SelectValue placeholder="Thêm người dùng, nhóm" />
              </SelectTrigger>
              <SelectContent>
                {availableTargets.map((item) => (
                  <SelectItem key={item.id} value={item.id.toString()}>
                    {item.email} {item.isGroup ? "(Nhóm)" : "(Người dùng)"}
                  </SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>

          {selectedUsers.length > 0 && (
            <div className="space-y-2">
              <Label className="text-sm">Quyền truy cập</Label>

              {selectedUsers.map((user) => (
                <div
                  key={user.id}
                  className="flex items-center justify-between bg-muted p-2 rounded-md"
                >
                  <div className="flex items-center space-x-2">
                    <Avatar className="h-6 w-6">
                      <AvatarImage src={user.url} alt="@shadcn" />
                      <AvatarFallback>
                        {user.name.slice(0, 2).toUpperCase()}
                      </AvatarFallback>
                    </Avatar>
                    <div className="text-sm">
                      <div>{user.name}</div>
                      <div className="text-xs text-muted-foreground">
                        {user.email}
                      </div>
                    </div>
                  </div>
                  <div className="flex items-center space-x-2">
                    <select
                      className="text-sm border rounded px-2 py-1"
                      value={user.access}
                      onChange={(e) =>
                        handleChangePermission(
                          user.targetId,
                          Number(e.target.value)
                        )
                      }
                    >
                      <option value={Permission.Write}>Quyền ghi</option>
                      <option value={Permission.Read}>Chỉ đọc</option>
                      <option value={Permission.NoAccess}>
                        Không có quyền
                      </option>
                    </select>
                    <Button
                      variant="ghost"
                      size="icon"
                      onClick={() => handleRemoveUser(user.targetId)}
                    >
                      <X className="h-4 w-4" />
                    </Button>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>

        <AlertDialogFooter>
          <AlertDialogCancel disabled={isPending}>Hủy</AlertDialogCancel>
          <Button disabled={isPending} onClick={() => mutate()}>
            {isPending ? "Đang lưu..." : "Lưu"}
          </Button>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}
