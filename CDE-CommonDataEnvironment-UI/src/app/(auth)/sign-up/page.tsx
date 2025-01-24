import { Card } from '@/components/ui/card'
import Image from 'next/image'
import { UserAuthForm } from './user-auth-form'


export default function SignIn() {
    return (
        <>
            <div className='container grid h-svh flex-col items-center justify-center bg-primary-foreground lg:max-w-none lg:px-0'>
                <div className='mx-auto flex w-full flex-col justify-center space-y-2 sm:w-[480px] lg:p-8'>
                    <div className='mb-4 flex items-center justify-center'>
                        <Image src={'/images/logo.png'} alt='logo' width={200} height={60} />
                    </div>
                    <Card className='p-6'>
                        <div className='flex flex-col space-y-2 text-left mb-5'>
                            <h1 className='text-2xl font-semibold tracking-tight'>Đăng ký hệ thống</h1>
                        </div>
                        <UserAuthForm />
                    </Card>
                </div>
            </div>
        </>
    )
}
