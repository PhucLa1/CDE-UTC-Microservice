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
import fileApiRequest from '@/apis/file.api';
import { File } from '@/data/schema/Project/file.schema';

type FormProps = {
    file: File,
    folder: Folder,
    isFile: boolean,
    node: ReactNode,
    setSheetOpen: (value: boolean) => void
}

export default function DeleteFolder({ folder, node, setSheetOpen, isFile, file }: FormProps) {
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
    const { mutate: mutateDeleteFile, isPending: isPendingDeleteFile } = useMutation({
        mutationKey: ['delete-file'],
        mutationFn: () => fileApiRequest.delete(file),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Xóa tệp ',
                message: 'Xóa tệp thành công'
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
                    <AlertDialogTitle>Bạn có chác chắn muốn xóa tài liệu này không</AlertDialogTitle>
                    <AlertDialogDescription>
                        Hành động này sẽ không thể khôi phục nên làm ơn chú ý
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Hủy</AlertDialogCancel>
                    <Button loading={isPendingDelete || isPendingDeleteFile} onClick={() => {
                        if (isFile) {
                            mutateDeleteFile()
                        } else {
                            mutateDelete()
                        }
                    }}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>
    )
}
