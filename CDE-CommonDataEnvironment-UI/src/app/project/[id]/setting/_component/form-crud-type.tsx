"use client"
import React, { ReactNode, useEffect, useState } from 'react'
import {
    AlertDialog,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { State } from '@/data/enums/state.enum';
import { Button } from '@/components/custom/button';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import typeApiRequest from '@/apis/type.api';
import { handleSuccessApi } from '@/lib/utils';
import { Type, typeDefault, typeSchema } from '@/data/schema/Project/type.schema';

type Props = {
    trigger: ReactNode;
    state: State,
    type?: Type,
}

export default function FormCRUDType({ trigger, state, type }: Props) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();

    const form = useForm<Type>({
        resolver: zodResolver(typeSchema),
        defaultValues: typeDefault
    });

    useEffect(() => {
        if (type) {
            form.setValue("name", type.name);
            form.setValue("projectId", type.projectId);
            form.setValue("imageIconUrl", type.imageIconUrl);
            form.setValue("id", type.id);
        }
    }, [type]); // ✅ Chỉ chạy khi `type` thay đổi

    const { mutate: mutateCreate, isPending: isPendingCreate } = useMutation({
        mutationKey: ['create'],
        mutationFn: (value: FormData) => typeApiRequest.create(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Tạo mới thành công",
                message: "Bạn đã tạo mới thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-types'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete'],
        mutationFn: () => typeApiRequest.delete(type!),
        onSuccess: () => {
            handleSuccessApi({
                title: "Xóa thể loại thành công",
                message: "Bạn đã xóa thể loại thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-types'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateUpdate, isPending: isPendingUpdate } = useMutation({
        mutationKey: ['update'],
        mutationFn: (value: FormData) => typeApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Sửa thể loại thành công",
                message: "Bạn đã sửa thể loại thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-types'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });

    const getTitle = () => {
        switch (state) {
            case State.CREATE:
                return "Thêm mới dữ liệu";
            case State.UPDATE:
                return "Cập nhật dữ liệu";
            case State.DELETE:
                return "Xóa dữ liệu";
            default:
                return "";
        }
    };

    const getDescription = () => {
        switch (state) {
            case State.CREATE:
                return "Hãy nhập thông tin để tạo mới.";
            case State.UPDATE:
                return "Bạn có chắc chắn muốn cập nhật dữ liệu này?";
            case State.DELETE:
                return "Hành động này không thể hoàn tác. Bạn có chắc muốn xóa?";
            default:
                return "";
        }
    };

    const onSubmit = (values: Type) => {
        const formData = new FormData();
        formData.append("projectId", values.projectId!.toString());
        formData.append("name", values.name);
        formData.append("iconImage", values.iconImage);
        if (state === State.CREATE) {
            mutateCreate(formData);
        }
        else if (state === State.UPDATE) {
            formData.append("id", values.id!.toString());
            mutateUpdate(formData)
        }

    };

    return (
        <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
            <AlertDialogTrigger asChild>
                <div onClick={() => setIsOpen(true)}>
                    {trigger}
                </div>
            </AlertDialogTrigger>

            {state === State.CREATE || state === State.UPDATE ? (
                <AlertDialogContent>
                    <AlertDialogHeader>
                        <AlertDialogTitle>{getTitle()}</AlertDialogTitle>
                        <AlertDialogDescription>{getDescription()}</AlertDialogDescription>
                    </AlertDialogHeader>
                    <Form {...form}>
                        <form onSubmit={form.handleSubmit(onSubmit)}>
                            <div className="flex flex-col space-y-1.5 flex-1">
                                <FormField
                                    control={form.control}
                                    name="name"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Tên</FormLabel>
                                            <FormControl>
                                                <Input placeholder="Thể loại A1" {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                            </div>
                            <div className="flex flex-col space-y-1.5 flex-1">
                                <FormField
                                    control={form.control}
                                    name="iconImage"
                                    render={({ field }) => {
                                        const imageUrl = type!.imageIconUrl; // Lấy URL từ form nếu có
                                        return (
                                            <FormItem className="space-y-1">
                                                <FormLabel>Ảnh thể loại</FormLabel>
                                                <FormControl>
                                                    <div className="relative w-128 h-64 border-2 border-dashed border-gray-300 rounded-lg flex justify-center items-center">
                                                        {field.value && field.value.size > 0 ? (
                                                            <img
                                                                src={URL.createObjectURL(field.value)}
                                                                alt="Selected"
                                                                className="w-full h-full object-cover rounded-lg"
                                                            />
                                                        ) : imageUrl ? (
                                                            <img
                                                                src={imageUrl}
                                                                alt="Project"
                                                                className="w-full h-full object-cover rounded-lg"
                                                            />
                                                        ) : (
                                                            <span className="text-gray-500">Nhấn vào đây để chọn ảnh</span>
                                                        )}
                                                        <Input
                                                            type="file"
                                                            accept="image/*"
                                                            onChange={(e) => {
                                                                const file = e.target.files?.[0];
                                                                field.onChange(file ?? field.value); // Nếu không chọn ảnh mới, giữ nguyên giá trị cũ
                                                            }}
                                                            className="absolute inset-0 w-full h-full opacity-0 cursor-pointer"
                                                        />
                                                    </div>
                                                </FormControl>
                                                <FormMessage /> {/* Hiển thị thông báo lỗi từ Zod */}
                                            </FormItem>
                                        );
                                    }}
                                />
                            </div>
                            <AlertDialogFooter className='mt-4'>
                                <AlertDialogCancel>Hủy</AlertDialogCancel>
                                <Button type="submit" loading={isPendingCreate || isPendingUpdate}>Tiếp tục</Button>
                            </AlertDialogFooter>
                        </form>
                    </Form>
                </AlertDialogContent>
            ) : <AlertDialogContent>
                <AlertDialogHeader>
                    <AlertDialogTitle>{getTitle()}</AlertDialogTitle>
                    <AlertDialogDescription>
                        {getDescription()}
                    </AlertDialogDescription>
                </AlertDialogHeader>
                <AlertDialogFooter>
                    <AlertDialogCancel>Hủy</AlertDialogCancel>
                    <Button onClick={() => mutateDelete()} loading={isPendingDelete}>Tiếp tục</Button>
                </AlertDialogFooter>
            </AlertDialogContent>}
        </AlertDialog>
    );
}
