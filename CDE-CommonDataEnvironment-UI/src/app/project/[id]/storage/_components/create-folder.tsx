"use client"
import React, { ReactNode, useState } from 'react'
import {
    AlertDialog,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { Button } from '@/components/custom/button'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import { useForm } from 'react-hook-form'
import { Folder, folderDefault, folderSchema } from '@/data/schema/Project/folder.schema'
import { zodResolver } from '@hookform/resolvers/zod'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import folderApiRequest from '@/apis/folder.api'
import { handleSuccessApi } from '@/lib/utils'
type FormProps = {
    node: ReactNode,
    parentId: number,
    projectId: number,
}
export default function CreateFolder({ node, parentId, projectId }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();

    const form = useForm<Folder>({
        resolver: zodResolver(folderSchema),
        defaultValues: folderDefault
    });

    const onSubmit = (values: Folder) => {
        values.parentId = parentId
        values.projectId = projectId
        mutate(values)
    };

    const { mutate, isPending } = useMutation({
        mutationKey: ['create-folder'],
        mutationFn: (value: Folder) => folderApiRequest.create(value),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Tạo mới thư mục',
                message: 'Tạo mới thư mục thành công'
            })
            setIsOpen(false)
            form.reset()
            queryClient.invalidateQueries({ queryKey: ['storage'] })
            //Gọi lại API
        },
        onError: () => {
            setIsOpen(false)
            form.reset()
        }
    })

    return (
        <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
            <AlertDialogTrigger asChild>
                {node}
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Tạo mới một thư mục</AlertDialogTitle>
                </AlertDialogHeader>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)}>
                        <div className="flex flex-col space-y-1.5 flex-1">
                            <FormField
                                control={form.control}
                                name="name"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Tên</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Thư mục A1" {...field} autoFocus />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>
                        <AlertDialogFooter className='mt-2'>
                            <AlertDialogCancel>Hủy</AlertDialogCancel>
                            <Button loading={isPending} type='submit'>Tiêp tục</Button>
                        </AlertDialogFooter>
                    </form>
                </Form>
            </AlertDialogContent>
        </AlertDialog>
    )
}
