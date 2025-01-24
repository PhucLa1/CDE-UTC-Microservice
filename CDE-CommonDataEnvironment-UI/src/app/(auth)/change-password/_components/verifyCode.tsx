"use client";
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from '@/components/ui/form'
import {
    InputOTP,
    InputOTPGroup,
    InputOTPSlot,
} from "@/components/ui/input-otp"
import { Button } from '@/components/custom/button'
import { handleSuccessApi } from '@/lib/utils'
import { useMutation } from '@tanstack/react-query';
import authApiRequest from '@/apis/auth.api';
import { VerifyCode, verifyCodeDefault, verifyCodeSchema } from '@/data/schema/Auth/verifyCode.schema';
import { REGEXP_ONLY_DIGITS } from 'input-otp';
import { useEffect } from 'react';

export default function VerifyCodeForm() {
    const form = useForm<VerifyCode>({
        resolver: zodResolver(verifyCodeSchema),
        defaultValues: verifyCodeDefault,
    })
    const onSubmit = (data: VerifyCode) => {

        mutate(data)
    }
    useEffect(() => {
        form.setValue('email', sessionStorage.getItem('email')?.toString())
    }, [])

    const { mutate, isPending } = useMutation({
        mutationKey: ['verify-code'],
        mutationFn: (data: VerifyCode) => authApiRequest.verifyCode(data),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Mã code thành công',
                message: 'Xác nhận mã code thành công'
            })
            const nextButton = Array.from(document.querySelectorAll('button')).find(
                (btn) => btn.textContent?.trim() === "Next"
            );
            nextButton?.click();
        }
    })
    return (
        <div >
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}>
                    <div className='grid gap-2'>
                        <FormField
                            control={form.control}
                            name="code"
                            render={({ field }) => (
                                <FormItem>
                                    <FormLabel>Nhập mã code</FormLabel>
                                    <FormControl>
                                        <InputOTP maxLength={6}
                                            pattern={REGEXP_ONLY_DIGITS} {...field}>
                                            <InputOTPGroup>
                                                <InputOTPSlot index={0} />
                                                <InputOTPSlot index={1} />
                                                <InputOTPSlot index={2} />
                                                <InputOTPSlot index={3} />
                                                <InputOTPSlot index={4} />
                                                <InputOTPSlot index={5} />
                                            </InputOTPGroup>
                                        </InputOTP>
                                    </FormControl>
                                    <FormDescription>
                                        Vui lòng nhập mã code mà bạn đã được nhận từ email.
                                    </FormDescription>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <Button type='submit' className='mt-2' loading={isPending}>
                            Xác nhận gửi mã code
                        </Button>
                    </div>
                </form>
            </Form>
        </div>
    )
}
