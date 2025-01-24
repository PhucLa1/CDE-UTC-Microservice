"use client";
import { HTMLAttributes } from 'react'
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
import { PasswordInput } from '@/components/custom/password-input'
import { cn, handleSuccessApi } from '@/lib/utils'
import Link from 'next/link'
import { Login, loginDefault, loginSchema } from '@/data/schema/Auth/login.schema';
import { useMutation } from '@tanstack/react-query';
import authApiRequest from '@/apis/auth.api';
import { useRouter } from 'next/navigation';

interface UserAuthFormProps extends HTMLAttributes<HTMLDivElement> { }


export function UserAuthForm({ className, ...props }: UserAuthFormProps) {
  const router = useRouter();
  const form = useForm<Login>({
    resolver: zodResolver(loginSchema),
    defaultValues: loginDefault,
  })

  const onSubmit = (data: Login) => {
    mutate(data)
  }

  const { mutate, isPending } = useMutation({
    mutationKey: ['login'],
    mutationFn: (data: Login) => authApiRequest.login(data),
    onSuccess: () => {
      handleSuccessApi({
        title: 'Đăng nhập thành công',
        message: 'Đang chuyển hướng trang sang trang chủ'
      })
      router.push('/detail-user')
    }
  })


  return (
    <div className={cn('grid gap-6', className)} {...props}>
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
            <FormField
              control={form.control}
              name='password'
              render={({ field }) => (
                <FormItem className='space-y-1'>
                  <div className='flex items-center justify-between'>
                    <FormLabel>Mật khẩu</FormLabel>
                    <Link href='/forgot-password'
                      className='text-sm font-medium text-muted-foreground hover:opacity-75'>
                      Quên mật khẩu?
                    </Link>
                  </div>
                  <FormControl>
                    <PasswordInput placeholder='********' {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <Button className='mt-2' loading={isPending}>
              Đăng nhập
            </Button>
            <p className='mt-4 text-center text-sm text-muted-foreground'>
              Chưa có tài khoản?{' '}
              <Link
                href='/sign-up'
                className='font-medium text-primary hover:opacity-75'>
                Đăng ký ngay
              </Link>
            </p>
          </div>
        </form>
      </Form>
    </div>
  )
}
