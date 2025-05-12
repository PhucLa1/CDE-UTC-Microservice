
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
import { handleSuccessApi } from '@/lib/utils'
import { Button } from '@/components/custom/button'
import todoCommentApiRequest from '@/apis/todocomment.api'
type FormProps = {
    node: ReactNode,
    id: number,
}
export default function DeleteFormComment({ node, id }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient()
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete-todo-comment'],
        mutationFn: () => todoCommentApiRequest.delete(id),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa bình luận',
                message: 'Xóa bình luận thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['get-todo-comments'] })
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
                    <Button
                        loading={isPendingDelete}
                        onClick={() => mutateDelete()}>Tiếp tục
                    </Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}
