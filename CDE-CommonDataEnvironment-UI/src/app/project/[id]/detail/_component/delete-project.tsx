import React from 'react'
import { useRouter } from 'next/navigation';
import projectApiRequest from '@/apis/project.api';
import { useMutation } from '@tanstack/react-query';
import { handleSuccessApi } from '@/lib/utils';
import { Button } from '@/components/custom/button';
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
export default function DeleteProject({ id }: { id: number }) {
    const router = useRouter()
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete-project'],
        mutationFn: () => projectApiRequest.deleteProject(id),
        onSuccess: () => {
            handleSuccessApi({
                title: "Xóa dự án thành công",
                message: "Bạn đã xóa dự án"
            })
            router.push('/project')
        }
    })
    return (
        <AlertDialog>
            <AlertDialogTrigger asChild>
                <Button  variant={'outline'} className="border border-red-500 text-red-500 hover:bg-red-500 hover:text-white">
                    Xóa dự án
                </Button>
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Bạn có chắc chắn không?</AlertDialogTitle>
                    <AlertDialogDescription>
                        Việc bạn xóa dự án sẽ không thể khôi phục.
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Hủy</AlertDialogCancel>
                    <Button onClick={() => mutateDelete()} loading={isPendingDelete}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>

    )
}
