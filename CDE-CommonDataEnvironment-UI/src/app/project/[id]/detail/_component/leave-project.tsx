import React from 'react'
import { useRouter } from 'next/navigation';
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
import teamApiRequest from '@/apis/team.api';
export default function LeaveProject({ id }: { id: number }) {
    const router = useRouter()
    const { mutate, isPending } = useMutation({
        mutationKey: ['leave-project'],
        mutationFn: () => teamApiRequest.leaveProject(id),
        onSuccess: () => {
            handleSuccessApi({
                title: "Rời dự án thành công",
                message: "Bạn đã rời dự án"
            })
            router.push('/project')
        }
    })
    return (
        <AlertDialog>
            <AlertDialogTrigger asChild>
                <Button className="bg-red-500 text-white hover:bg-white hover:text-red-600 hover:border-red-500">
                    Rời dự án
                </Button>
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Bạn có chắc chắn không?</AlertDialogTitle>
                    <AlertDialogDescription>
                        Việc bạn rời khỏi sẽ không thể khôi phục.
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Hủy</AlertDialogCancel>
                    <Button onClick={() => mutate()} loading={isPending}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>
        </AlertDialog>

    )
}
