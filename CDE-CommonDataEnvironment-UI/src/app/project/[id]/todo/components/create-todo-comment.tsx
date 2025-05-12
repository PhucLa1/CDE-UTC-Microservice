
import todoCommentApiRequest from '@/apis/todocomment.api';
import { Button } from '@/components/custom/button';
import { Form, FormControl, FormField, FormItem, FormMessage } from '@/components/ui/form';
import { Textarea } from '@/components/ui/textarea';
import { TodoComment, todoCommentDefault, todoCommentSchema } from '@/data/schema/Project/todocomment.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import React from 'react'
import { useForm } from 'react-hook-form';
type FormProps = {
    id: number
}
export default function CreateTodoComment({ id }: FormProps) {
    const queryClient = useQueryClient()
    const form = useForm<TodoComment>({
        resolver: zodResolver(todoCommentSchema),
        defaultValues: todoCommentDefault
    });
    const onSubmit = (values: TodoComment) => {
        values.todoId = id
        console.log(values)
        mutate(values)
    };

    const { mutate, isPending } = useMutation({
        mutationKey: ['create-todo-comment'],
        mutationFn: (value: TodoComment) => todoCommentApiRequest.create(value),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['get-todo-comments'] })
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
                    <Button loading={isPending} className="mt-2">Thêm bình luận</Button>
                </div>
            </form>
        </Form>
    )
}
