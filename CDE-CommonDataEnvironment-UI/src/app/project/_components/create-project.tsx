"use client";
import React, { useState } from 'react'
import {
    AlertDialog,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { Project, projectDefault, projectSchema } from '@/data/schema/Project/project.schema'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import { Input } from '@/components/ui/input'
import { Textarea } from "@/components/ui/textarea"
import { Button } from '@/components/custom/button'
import { useMutation, useQueryClient } from '@tanstack/react-query'
import projectApiRequest from '@/apis/project.api'
import { handleSuccessApi } from '@/lib/utils'
import { Plus } from 'lucide-react'


export default function CreateProject() {
    const [open, setOpen] = useState<boolean>(false)
    const queryClient = useQueryClient()
    const form = useForm<Project>({
        resolver: zodResolver(projectSchema),
        defaultValues: projectDefault,
    })

    const onSubmit = (data: Project) => {
        const formData = new FormData();
        formData.append('name', data.name);
        formData.append('description', data.description);
        formData.append('image', data.image);
        formData.append('startDate', data.startDate);
        formData.append('endDate', data.endDate);
        mutate(formData)
        console.log(data)
    }
    const { mutate, isPending } = useMutation({
        mutationKey: ['createProject'],
        mutationFn: (data: FormData) => projectApiRequest.create(data),
        onSuccess: () => {
            setOpen(false)
            handleSuccessApi({
                title: "Tạo mới dự án thành công",
                message: "Dự án đã được tạo mới thành công",
            })
            queryClient.invalidateQueries({ queryKey: ['list-project'] })
            form.reset();
        }
    })
    return (
        <AlertDialog open={open} onOpenChange={() => setOpen(!open)}>
            <AlertDialogTrigger>
                <Plus>Tạo mới dự án</Plus>
            </AlertDialogTrigger>
            <AlertDialogContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)}>
                        <AlertDialogHeader>
                            <AlertDialogTitle>Tạo mới dự án</AlertDialogTitle>
                            <AlertDialogDescription>
                                Hãy điền những thông tin cần thiết để chúng tôi tạo mới cho bạn một khu làm việc trên hệ thống
                            </AlertDialogDescription>
                        </AlertDialogHeader>
                        <div>
                            <FormField
                                control={form.control}
                                name="name"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Tên dự án</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Ghi tên dự án của bạn vào đây" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <FormField
                                control={form.control}
                                name="description"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Mô tả dự án</FormLabel>
                                        <FormControl>
                                            <Textarea placeholder="Ghi mô tả dự án của bạn vào đây" {...field} />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                            <div className="grid grid-cols-2 gap-4">
                                <FormField
                                    control={form.control}
                                    name="startDate"
                                    render={({ field }) => (
                                        <FormItem className="space-y-1">
                                            <FormLabel>Ngày bắt đầu</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="date"
                                                    {...field}
                                                    value={field.value ? field.value : ""} // Hiển thị giá trị mặc định nếu có
                                                    onChange={(e) => {
                                                        const value = e.target.value; // Giá trị là chuỗi (string)
                                                        field.onChange(value); // Cập nhật giá trị vào form
                                                    }}
                                                />
                                            </FormControl>
                                            <FormMessage /> {/* Hiển thị thông báo lỗi từ Zod */}
                                        </FormItem>
                                    )}
                                />
                                <FormField
                                    control={form.control}
                                    name="endDate"
                                    render={({ field }) => (
                                        <FormItem className="space-y-1">
                                            <FormLabel>Ngày kết thúc</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="date"
                                                    {...field}
                                                    value={field.value ? field.value : ""} // Hiển thị giá trị mặc định nếu có
                                                    onChange={(e) => {
                                                        const value = e.target.value; // Giá trị là chuỗi (string)
                                                        field.onChange(value); // Cập nhật giá trị vào form
                                                    }}
                                                />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                            </div>
                            <FormField
                                control={form.control}
                                name="image"
                                render={({ field }) => (
                                    <FormItem className="space-y-1">
                                        <FormLabel>Ảnh dự án</FormLabel>
                                        <FormControl>
                                            <div className="relative w-128 h-64 border-2 border-dashed border-gray-300 rounded-lg flex justify-center items-center">
                                                {field.value && field.value.size > 0 ? (
                                                    <img
                                                        src={URL.createObjectURL(field.value)}
                                                        alt="Selected"
                                                        className="w-full h-full object-cover rounded-lg"
                                                    />
                                                ) : (
                                                    <span className="text-gray-500">Nhấn vào đây để chọn ảnh</span>
                                                )}
                                                <Input
                                                    type="file"
                                                    accept="image/*"
                                                    onChange={(e) => {
                                                        const file = e.target.files;
                                                        if (file) {
                                                            field.onChange(file[0]); // Cập nhật giá trị vào form
                                                        }
                                                    }}
                                                    className="absolute inset-0 w-full h-full opacity-0 cursor-pointer"
                                                />
                                            </div>
                                        </FormControl>
                                        <FormMessage /> {/* Hiển thị thông báo lỗi từ Zod */}
                                    </FormItem>
                                )}
                            />
                        </div>
                        <div className="mt-4">
                            <Button type="submit" loading={isPending}>Tạo mới</Button>
                        </div>
                    </form>
                </Form>
            </AlertDialogContent>
        </AlertDialog>

    )
}
