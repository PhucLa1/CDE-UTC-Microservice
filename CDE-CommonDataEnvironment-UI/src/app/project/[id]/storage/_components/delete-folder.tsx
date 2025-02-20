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
import { useMutation, useQueryClient } from '@tanstack/react-query';
import folderApiRequest from '@/apis/folder.api';
import { Folder } from '@/data/schema/Project/folder.schema';
import { handleSuccessApi } from '@/lib/utils';
import { Button } from '@/components/custom/button';

type FormProps = {
    folder: Folder,
    node: ReactNode,
    setSheetOpen: (value: boolean) => void
}

export default function DeleteFolder({ folder, node, setSheetOpen }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient()
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete-foler'],
        mutationFn: () => folderApiRequest.delete(folder),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa thư mục ',
                message: 'Xóa thư mục thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['storage'] })
            setIsOpen(false)
            setSheetOpen(false)
        }
    })
    console.log(isOpen)
    return (
        <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
            <AlertDialogTrigger asChild>
                {node}
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Bạn có chác chắn muốn xóa thư mục này không</AlertDialogTitle>
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
