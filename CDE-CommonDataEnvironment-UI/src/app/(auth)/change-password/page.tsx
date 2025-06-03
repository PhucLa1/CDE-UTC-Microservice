"use client"
import React, { useEffect } from 'react'
import Image from 'next/image'
import { Card } from '@/components/ui/card'
import { SendEmailVerifyForm } from './_components/sendEmailVerify'
import MultiStep from 'react-multistep'
import VerifyCodeForm from './_components/verifyCode'
import { ChangePasswordForm } from './_components/changePassword'
export default function Page() {
    useEffect(() => {
        const nextButton = Array.from(document.querySelectorAll('button')).find(
            (btn) => btn.textContent?.trim() === "Next"
        );
        const prevButton = Array.from(document.querySelectorAll('button')).find(
            (btn) => btn.textContent?.trim() === "Prev"
        );

        if (nextButton) {
            nextButton.style.visibility = 'hidden'; // Ẩn nút (vẫn chiếm không gian)
        }

        if (prevButton) {
            prevButton.style.visibility = 'hidden';
        }
    }, []); // Chạy một lần khi component được mount
    return (
        <>
            <div className='container grid h-svh flex-col items-center justify-center bg-primary-foreground lg:max-w-none lg:px-0'>
                <div className='mx-auto flex w-full flex-col justify-center space-y-2 sm:w-[480px] lg:p-8'>
                    <div className='mb-4 flex items-center justify-center'>
                        <Image src={'/images/logo.png'} alt='logo' width={200} height={60} />
                    </div>
                    <Card className='p-6'>
                        <div className='flex flex-col space-y-2 text-left mb-5'>
                            <h1 className='text-2xl font-semibold tracking-tight'>Hãy làm theo các bước dưới đây để có thể đổi mật khẩu</h1>
                        </div>

                        <MultiStep activeStep={0}>
                            <SendEmailVerifyForm />
                            <VerifyCodeForm />
                            <ChangePasswordForm />
                        </MultiStep>
                    </Card>
                </div>
            </div>
        </>
    )
}
