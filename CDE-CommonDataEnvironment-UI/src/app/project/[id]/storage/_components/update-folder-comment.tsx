import folderCommentApiRequest from '@/apis/foldercomment.api';
import { Button } from '@/components/custom/button';
import { Form, FormControl, FormField, FormItem, FormMessage } from '@/components/ui/form';
import { Textarea } from '@/components/ui/textarea';
import { FolderComment, folderCommentSchema } from '@/data/schema/Project/foldercomment.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import React from 'react'
import { useForm } from 'react-hook-form';
type FormProps = {
    folderComment: FolderComment,
    setUpdateComment: (updateComment: number) => void
}
export default function UpdateFolderComment({ folderComment, setUpdateComment }: FormProps) {
    const queryClient = useQueryClient();
    const form = useForm<FolderComment>({
        resolver: zodResolver(folderCommentSchema),
        defaultValues: {
            content: folderComment.content,
            id: folderComment.id,
            projectId: folderComment.projectId,
            folderId: folderComment.folderId
        }
    });
    const onSubmit = (values: FolderComment) => {
        mutate(values)
    };
    const { mutate, isPending } = useMutation({
        mutationKey: ['update-foler-comment'],
        mutationFn: (value: FolderComment) => folderCommentApiRequest.update(value),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['get-detail-folder', folderComment.folderId] })
            setUpdateComment(0)
        }
    })
    return (
        <div className="flex items-center justify-between w-full mt-2">
            <Form {...form}>
                <form className='w-full' onSubmit={form.handleSubmit(onSubmit)}>
                    <div className="w-full">
                        <FormField
                            control={form.control}
                            name="content"
                            render={({ field }) => (
                                <FormItem>
                                    <FormControl>
                                        <Textarea className='w-full'  {...field} autoFocus />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                    </div>
                    <div className='flex justify-start items-center mt-2 mb-2'>
                        <Button onClick={() => setUpdateComment(0)} variant={"destructive"}>Hủy</Button>
                        <Button className='ml-2' type='submit' loading={isPending}>Cập nhật</Button>
                    </div>
                </form>
            </Form>
        </div>
    )
}
