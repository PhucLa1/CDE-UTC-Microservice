import React, { ReactNode, useState } from 'react'
import {
    AlertDialog,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { UserProject } from '@/data/schema/Project/userproject.schema'
import teamApiRequest from '@/apis/team.api'
import { Role } from '@/data/enums/role.enum'
import { handleSuccessApi } from '@/lib/utils'
import { Button } from '@/components/custom/button'
import userGroupApiRequest from '@/apis/usergroup.api'
type FormProps = {
    node: ReactNode, 
    userProject: UserProject, 
    projectId: number, 
    groupId: number
}
export default function FormDeleteUser({ node, userProject, projectId, groupId }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient()
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['kick-user'],
        mutationFn: () => teamApiRequest.kickUser({
            projectId: projectId,
            role: Role.Admin,
            email: userProject.email,
            userId: userProject.id
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa người dùng khỏi dự án thành công',
                message: 'Xóa người dùng khỏi dự án thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['users-project'] })
            setIsOpen(false)
        }
    })
    const { mutate: mutateDeleteUserGroup, isPending: isPendingDeleteUserGroup } = useMutation({
        mutationKey: ['kick-user'],
        mutationFn: () => userGroupApiRequest.deleteUser({
            projectId: projectId,
            role: Role.Admin,
            email: userProject.email,
            userId: userProject.id,
            groupId: groupId,
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa người dùng khỏi nhóm thành công',
                message: 'Xóa người dùng khỏi nhóm thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['users-group', groupId] })
            queryClient.invalidateQueries({ queryKey: ['groups'] })
            setIsOpen(false)
        }
    })

    const handleDelete = () => {
        if (groupId == 0) mutateDelete()
        else mutateDeleteUserGroup()
    }
    return (
        <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
            <AlertDialogTrigger asChild>
                {node}
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Bạn có chác chắn muốn xóa người dùng này ra khỏi dự án</AlertDialogTitle>
                    <AlertDialogDescription>
                        Hành động này sẽ không thể khôi phục nên làm ơn chú ý
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Hủy</AlertDialogCancel>
                    <Button loading={isPendingDelete || isPendingDeleteUserGroup} onClick={() => handleDelete()}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}
