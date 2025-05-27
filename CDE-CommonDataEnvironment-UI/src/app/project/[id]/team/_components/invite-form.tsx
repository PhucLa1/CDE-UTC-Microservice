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
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useForm } from 'react-hook-form';
import { UserProject, userProjectDefault, userProjectSchema } from '@/data/schema/Project/userproject.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/custom/button';
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group';
import { Role } from '@/data/enums/role.enum';
import teamApiRequest from '@/apis/team.api';
import { handleSuccessApi } from '@/lib/utils';
export default function InviteForm({ button, projectId }: { button: ReactNode, projectId: number }) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();

    const form = useForm<UserProject>({
        resolver: zodResolver(userProjectSchema),
        defaultValues: userProjectDefault
    });
    const onSubmit = (values: UserProject) => {
        values.projectId = projectId
        console.log(values)
        mutate(values);
    };

    const {mutate, isPending} = useMutation({
        mutationKey: ['invite-user'],
        mutationFn: (value: UserProject) => teamApiRequest.inviteUser(value),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Lời mời đã được gửi đi',
                message: 'Làm ơn chờ đợi phản hồi từ thành viên đó',
            })
            queryClient.invalidateQueries({queryKey: ['users-project']})
            setIsOpen(false)
            form.reset()
        },
        onError: () => {
            setIsOpen(false)
            form.reset()
        }
    })

    return (
        <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
            <AlertDialogTrigger asChild>
                {button}
            </AlertDialogTrigger>
            <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>Thêm người vào dự án</AlertDialogTitle>
                </AlertDialogHeader>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)}>
                        <div className="flex flex-col space-y-1.5 flex-1">
                            <FormField
                                control={form.control}
                                name="email"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Email</FormLabel>
                                        <FormControl>
                                            <Input placeholder="nguyenvana@gmail.com" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>
                        <div className="flex flex-col space-y-1.5 flex-1 mt-2">
                            <FormField
                                control={form.control}
                                name="role"
                                render={({ field }) => (
                                    <FormItem className="space-y-3">
                                        <FormLabel>Chọn quyền trong dự án</FormLabel>
                                        <FormControl>
                                            <RadioGroup
                                                onValueChange={(value) => {
                                                    const enumValue = parseInt(value, 10);
                                                    field.onChange(enumValue)
                                                }}
                                                defaultValue={field.value.toString()}
                                                className="flex flex-col space-y-1"
                                            >
                                                <FormItem className="flex items-center space-x-3 space-y-0">
                                                    <FormControl>
                                                        <RadioGroupItem value={Role.Admin.toString()} />
                                                    </FormControl>
                                                    <FormLabel className="font-normal">
                                                        Quản trị viên
                                                    </FormLabel>
                                                </FormItem>
                                                <FormItem className="flex items-center space-x-3 space-y-0">
                                                    <FormControl>
                                                        <RadioGroupItem value={Role.Member.toString()} />
                                                    </FormControl>
                                                    <FormLabel className="font-normal">
                                                        Người dùng
                                                    </FormLabel>
                                                </FormItem>
                                            </RadioGroup>
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>
                        <AlertDialogFooter>
                            <AlertDialogCancel>Hủy</AlertDialogCancel>
                            <Button type="submit" loading={isPending}>Tiếp tục</Button>
                        </AlertDialogFooter>
                    </form>
                </Form >
            </AlertDialogContent>
        </AlertDialog>

    )
}
