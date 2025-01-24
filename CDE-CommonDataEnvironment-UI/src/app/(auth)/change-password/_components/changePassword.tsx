"use client";
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from '@/components/ui/form'
import { Button } from '@/components/custom/button'
import { handleSuccessApi } from '@/lib/utils'
import { useMutation } from '@tanstack/react-query';
import authApiRequest from '@/apis/auth.api';
import { ChangePassword, changePasswordDefault, changePasswordSchema } from '@/data/schema/Auth/changePassword.schema';
import { PasswordInput } from '@/components/custom/password-input';
import { useRouter } from 'next/navigation';
import { useEffect } from 'react';


export function ChangePasswordForm() {
    const router = useRouter();
    const form = useForm<ChangePassword>({
        resolver: zodResolver(changePasswordSchema),
        defaultValues: changePasswordDefault,
    })

    const onSubmit = (data: ChangePassword) => {
        mutate(data)
    }
    useEffect(() => {
        form.setValue('email', sessionStorage.getItem('email')?.toString())
    }, [])

    const { mutate, isPending } = useMutation({
        mutationKey: ['change-password'],
        mutationFn: (data: ChangePassword) => authApiRequest.changePassword(data),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Thay đổi mật khẩu thành công',
                message: 'Bạn đã thay đổi mật khẩu thành công'
            })
            router.push('/detail-user')
        }
    })


    return (
        <div >
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}>
                    <div className='grid gap-2'>
                        <FormField
                            control={form.control}
                            name='password'
                            render={({ field }) => (
                                <FormItem className='space-y-1'>
                                    <div className='flex items-center justify-between'>
                                        <FormLabel>Mật khẩu</FormLabel>
                                    </div>
                                    <FormControl>
                                        <PasswordInput placeholder='********' {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField
                            control={form.control}
                            name='rePassword'
                            render={({ field }) => (
                                <FormItem className='space-y-1'>
                                    <div className='flex items-center justify-between'>
                                        <FormLabel>Xác thực mật khẩu</FormLabel>
                                    </div>
                                    <FormControl>
                                        <PasswordInput placeholder='********' {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <Button className='mt-2' loading={isPending}>
                            Gửi mã code
                        </Button>
                    </div>
                </form>
            </Form>
        </div>
    )
}
