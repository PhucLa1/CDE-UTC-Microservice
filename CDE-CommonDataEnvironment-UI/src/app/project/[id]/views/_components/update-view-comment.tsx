import viewCommentApiRequest from '@/apis/viewcomment.api';
import { Button } from '@/components/custom/button';
import { Form, FormControl, FormField, FormItem, FormMessage } from '@/components/ui/form';
import { Textarea } from '@/components/ui/textarea';
import { ViewComment, viewCommentSchema } from '@/data/schema/Project/viewcomment.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import React from 'react'
import { useForm } from 'react-hook-form';
type FormProps = {
    viewComment: ViewComment,
    setUpdateComment: (updateComment: number) => void
}
export default function UpdateViewComment({ viewComment, setUpdateComment }: FormProps) {
    const queryClient = useQueryClient();
    const form = useForm<ViewComment>({
        resolver: zodResolver(viewCommentSchema),
        defaultValues: {
            content: viewComment.content,
            id: viewComment.id,
            projectId: viewComment.projectId,
            viewId: viewComment.viewId
        }
    });
    const onSubmit = (values: ViewComment) => {
        mutate(values)
    };
    const { mutate, isPending } = useMutation({
        mutationKey: ['update-view-comment'],
        mutationFn: (value: ViewComment) => viewCommentApiRequest.update(value),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['get-detail-view'] })
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
