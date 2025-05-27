import React, { useState } from 'react'
import {
    AlertDialog,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import Select from "react-select";
import { UserProject } from '@/data/schema/Project/userproject.schema';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import userGroupApiRequest from '@/apis/usergroup.api';
import { Button } from '@/components/custom/button';
import { AddUserGroupRequest } from '@/data/schema/Project/usergroup.schema';
import { handleSuccessApi } from '@/lib/utils';

type FormProps = {
    dataDropdown: UserProject[],
    groupId: number,
    projectId: number
}
export default function AddUserGroup({ dataDropdown, groupId, projectId }: FormProps) {
    const queryClient = useQueryClient()
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const [selectedUsers, setSelectedUsers] = useState<{ label: string, value: number }[]>([]); // State lưu giá trị đã chọn
    console.log(selectedUsers)

    const { mutate, isPending } = useMutation({
        mutationKey: ['add-user-group'],
        mutationFn: (value: AddUserGroupRequest) => userGroupApiRequest.addUser(value),
        onSuccess: () => {
            setIsOpen(false);
            setSelectedUsers([]); // Reset danh sách sau khi thêm thành công
            handleSuccessApi({
                title: 'Thêm thành công người dùng',
                message: 'Thêm thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['users-group', groupId] })
            queryClient.invalidateQueries({ queryKey: ['groups'] })
        }
    });

    const handleSubmit = () => {
        if (selectedUsers.length === 0) return; // Kiểm tra nếu chưa chọn người dùng

        const userIds = selectedUsers.map(user => user.value); // Chuyển dữ liệu thành mảng ID
        const requestData: AddUserGroupRequest = {
            userIds: userIds,
            groupId: groupId,
            projectId: projectId
        };
        console.log(requestData);
        mutate(requestData);
    };

    return (
        <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
            <AlertDialogTrigger asChild>
                <Button variant="outline">Thêm mới người dùng</Button>
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Bạn đang thêm mới người dùng vào nhóm?</AlertDialogTitle>
                </AlertDialogHeader>
                <Select
                    isMulti
                    options={dataDropdown
                        .filter(role => role.id !== undefined)
                        .map(role => ({ label: role.email, value: role.id! }))}
                    className="w-full"
                    placeholder="Thêm người dùng"
                    onChange={(newValue) => setSelectedUsers([...newValue])} // Chuyển từ readonly sang mảng mutable
                />
                <AlertDialogFooter>
                    <AlertDialogCancel>Hủy</AlertDialogCancel>
                    <Button onClick={handleSubmit} loading={isPending}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}
