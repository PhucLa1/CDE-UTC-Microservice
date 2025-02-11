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
export default function FormDeleteUser({ node, userProject, projectId }: { node: ReactNode, userProject: UserProject, projectId: number }) {
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
                    <Button loading={isPendingDelete} onClick={() => mutateDelete()}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}
