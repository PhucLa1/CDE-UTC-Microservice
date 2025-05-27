
import fileCommentApiRequest from '@/apis/filecomment.api'
import folderCommentApiRequest from '@/apis/foldercomment.api'
import { Button } from '@/components/custom/button'
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
import { handleSuccessApi } from '@/lib/utils'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { ReactNode, useState } from 'react'
type FormProps = {
    node: ReactNode,
    id: number,
    projectId: number,
    storageId: number,
    isFile: boolean,
}
export default function DeleteForm({ node, id, projectId, storageId, isFile }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient()
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete-foler-comment'],
        mutationFn: () => folderCommentApiRequest.delete({
            id: id,
            projectId: projectId,
            content: ''
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa bình luận',
                message: 'Xóa bình luận thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['get-detail-folder', storageId] })
            setIsOpen(false)
        }
    })
    const { mutate: mutateDeleteFileComment, isPending: isPendingDeleteFileComment } = useMutation({
        mutationKey: ['delete-file-comment'],
        mutationFn: () => fileCommentApiRequest.delete({
            id: id,
            projectId: projectId,
            content: ''
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa bình luận',
                message: 'Xóa bình luận thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['get-detail-file', storageId] })
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
                        loading={isPendingDelete || isPendingDeleteFileComment}
                        onClick={() => { isFile ? mutateDeleteFileComment() : mutateDelete() }}>Tiếp tục
                    </Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}
