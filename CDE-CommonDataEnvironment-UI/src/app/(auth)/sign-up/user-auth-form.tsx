"use client";
import { HTMLAttributes } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/custom/button";
import { PasswordInput } from "@/components/custom/password-input";
import { cn, handleSuccessApi } from "@/lib/utils";
import Link from "next/link";
import { SignUp, signUpDefault, signUpSchema } from "@/data/schema/Auth/signup.schema";
import { useMutation } from "@tanstack/react-query";
import authApiRequest from "@/apis/auth.api";
interface UserAuthFormProps extends HTMLAttributes<HTMLDivElement> { }

export function UserAuthForm({ className, ...props }: UserAuthFormProps) {

    const form = useForm<SignUp>({
        resolver: zodResolver(signUpSchema),
        defaultValues: signUpDefault,
    });

    function onSubmit(data: SignUp) {
        console.log(data);
        mutate(data);

    }

    const {mutate, isPending} = useMutation({
        mutationKey: ["sign-up"],
        mutationFn: (data: SignUp) => authApiRequest.signUp(data),
        onSuccess:() => {
            handleSuccessApi({
                title: "Đăng kí thành công",
                message: "Vui lòng kiểm tra email để xác nhận đã đăng kí hệ thống thành công"
            })
        }
    })

    return (
        <div className={cn("grid gap-6", className)} {...props}>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)}>
                    <div className="grid gap-4">
                        {/* Firstname and Lastname */}
                        <div className="grid grid-cols-2 gap-4">
                            <FormField
                                control={form.control}
                                name="firstName"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Họ</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Họ" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="lastName"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Tên</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Tên" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>

                        {/* Email */}
                        <FormField
                            control={form.control}
                            name="email"
                            render={({ field }) => (
                                <FormItem className="space-y-1">
                                    <FormLabel>Email</FormLabel>
                                    <FormControl>
                                        <Input placeholder="name@example.com" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        {/* Phone */}
                        <FormField
                            control={form.control}
                            name="mobilePhoneNumber"
                            render={({ field }) => (
                                <FormItem className="space-y-1">
                                    <FormLabel>Số điện thoại</FormLabel>
                                    <FormControl>
                                        <Input placeholder="0123456789" {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        {/* Password and Repassword */}
                        <div className="grid grid-cols-2 gap-4">
                            <FormField
                                control={form.control}
                                name="password"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Mật khẩu</FormLabel>
                                        <FormControl>
                                            <PasswordInput placeholder="********" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="rePassword"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Xác nhận mật khẩu</FormLabel>
                                        <FormControl>
                                            <PasswordInput placeholder="********" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>

                        <Button className="mt-2" loading={isPending}>
                            Đăng kí
                        </Button>

                        {/* Redirect to Login */}
                        <p className="mt-4 text-center text-sm text-muted-foreground">
                            Đã có tài khoản?{" "}
                            <Link
                                href="/login"
                                className="font-medium text-primary hover:opacity-75"
                            >
                                Đăng nhập tại đây
                            </Link>
                        </p>
                    </div>
                </form>
            </Form>
        </div>
    );
}
