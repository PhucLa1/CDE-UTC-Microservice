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
import { Input } from '@/components/ui/input'
import { Button } from '@/components/custom/button'
import { handleSuccessApi } from '@/lib/utils'
import { useMutation } from '@tanstack/react-query';
import authApiRequest from '@/apis/auth.api';
import { sendEmailVerifyDefault, sendEmailVerifySchema, SendEmailVerify } from '@/data/schema/Auth/sendEmailVerify.schema';


export function SendEmailVerifyForm() {
  const form = useForm<SendEmailVerify>({
    resolver: zodResolver(sendEmailVerifySchema),
    defaultValues: sendEmailVerifyDefault,
  })

  const onSubmit = (data: SendEmailVerify) => {
    mutate(data)
  }

  const { mutate, isPending } = useMutation({
    mutationKey: ['send-mail-verify'],
    mutationFn: (data: SendEmailVerify) => authApiRequest.sendEmailVerify(data),
    onSuccess: () => {
      handleSuccessApi({
        title: 'Gửi mã code thành công',
        message: 'Vui lòng kiểm tra mail của bạn'
      })
      const nextButton = Array.from(document.querySelectorAll('button')).find(
        (btn) => btn.textContent?.trim() === "Next"
      );
      nextButton?.click();
      sessionStorage.setItem('email', form.getValues('email'));
    }
  })


  return (
    <div >
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <div className='grid gap-2'>
            <FormField
              control={form.control}
              name='email'
              render={({ field }) => (
                <FormItem className='space-y-1'>
                  <FormLabel>Email</FormLabel>
                  <FormControl>
                    <Input placeholder='name@example.com' {...field} />
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
