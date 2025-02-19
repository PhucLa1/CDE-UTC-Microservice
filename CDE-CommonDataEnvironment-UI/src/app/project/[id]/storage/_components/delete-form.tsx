
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
import { FolderComment } from '@/data/schema/Project/foldercomment.schema'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import folderCommentApiRequest from '@/apis/foldercomment.api'
import { handleSuccessApi } from '@/lib/utils'
import { Button } from '@/components/custom/button'
type FormProps = {
    node: ReactNode,
    folderComment: FolderComment
}
export default function DeleteForm({ node, folderComment }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient()
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete-foler-comment'],
        mutationFn: () => folderCommentApiRequest.delete(folderComment),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa bình luận',
                message: 'Xóa bình luận thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['get-detail-folder', folderComment.folderId] })
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
                    <AlertDialogTitle>Bạn có chác chắn muốn xóa bình luận này không</AlertDialogTitle>
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
