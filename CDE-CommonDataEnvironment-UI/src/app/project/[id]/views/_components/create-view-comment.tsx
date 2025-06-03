import viewCommentApiRequest from '@/apis/viewcomment.api';
import { Button } from '@/components/custom/button';
import { Form, FormControl, FormField, FormItem, FormMessage } from '@/components/ui/form';
import { Textarea } from '@/components/ui/textarea';
import { ViewComment, viewCommentDefault, viewCommentSchema } from '@/data/schema/Project/viewcomment.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useForm } from 'react-hook-form';
type FormProps = {
    id: number,
    projectId: number
}
export default function CreateViewComment({ id, projectId }: FormProps) {
    const queryClient = useQueryClient()
    const form = useForm<ViewComment>({
        resolver: zodResolver(viewCommentSchema),
        defaultValues: viewCommentDefault
    });
    const onSubmit = (values: ViewComment) => {
        values.viewId = id
        values.projectId = projectId
        mutate(values)
    };

    const { mutate, isPending } = useMutation({
        mutationKey: ['create-view-comment'],
        mutationFn: (value: ViewComment) => viewCommentApiRequest.create(value),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['get-detail-view'] })
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
