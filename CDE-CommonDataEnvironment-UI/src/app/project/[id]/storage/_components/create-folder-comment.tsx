import folderCommentApiRequest from '@/apis/foldercomment.api';
import { Button } from '@/components/custom/button';
import { Form, FormControl, FormField, FormItem, FormMessage } from '@/components/ui/form';
import { Textarea } from '@/components/ui/textarea';
import { FolderComment, folderCommentDefault, folderCommentSchema } from '@/data/schema/Project/foldercomment.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import React from 'react'
import { useForm } from 'react-hook-form';
type FormProps = {
    id: number,
    projectId: number
}
export default function CreateFolderComment({ id, projectId }: FormProps) {
    const queryClient = useQueryClient()
    const form = useForm<FolderComment>({
        resolver: zodResolver(folderCommentSchema),
        defaultValues: folderCommentDefault
    });
    const onSubmit = (values: FolderComment) => {
        values.folderId = id
        values.projectId = projectId
        mutate(values)
    };

    const { mutate, isPending } = useMutation({
        mutationKey: ['create-folder-comment'],
        mutationFn: (value: FolderComment) => folderCommentApiRequest.create(value),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['get-detail-folder', id] })
            form.reset()

        }
    })
    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)}>
                <div className="mt-4">
                    <div className="flex flex-col space-y-1.5 flex-1">
                        <FormField
                            control={form.control}
                            name="content"
                            render={({ field }) => (
                                <FormItem>
                                    <FormControl>
                                        <Textarea {...field} placeholder="Viêt thêm vình luận vào đây..." />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                    <Button loading={isPending} type="submit" className="mt-2">Thêm bình luận</Button>
                </div>
            </form>
        </Form>
    )
}
