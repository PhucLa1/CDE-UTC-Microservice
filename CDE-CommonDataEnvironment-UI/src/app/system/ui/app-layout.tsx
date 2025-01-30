"use client";
import { AuthLayout, MainLayout } from "@/components/app-layout";
import { usePathname } from 'next/navigation'
import { ToastContainer } from "react-toastify";


// Create a client
const authPaths = ['/login', '/sign-up', '/change-password'];
export default function AppLayout({ children }: Readonly<{ children: React.ReactNode }>) {
    const pathname = usePathname()
    console.log(pathname)
    let layout = 0;
    if (authPaths.includes(pathname)) layout = 0;
    else if (pathname == "/project") layout = 0;
    else layout = 1;

    return (
        <>
            {layout == 0 ? <AuthLayout>{children}</AuthLayout>
                : <MainLayout>{children}</MainLayout>}
            <ToastContainer autoClose={2000} />
        </>
    );
}