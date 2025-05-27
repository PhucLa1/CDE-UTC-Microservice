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
import { handleSuccessApi } from '@/lib/utils';
import { HexColorPicker } from "react-colorful";
import { Priority, priorityDefault, prioritySchema } from '@/data/schema/Project/priority.schema';
import priorityApiRequest from '@/apis/priority.api';
type Props = {
    trigger: ReactNode;
    state: State,
    priority?: Priority,
}

export default function FormCRUDPriority({ trigger, state, priority }: Props) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();

    const form = useForm<Priority>({
        resolver: zodResolver(prioritySchema),
        defaultValues: priorityDefault
    });

    useEffect(() => {
        if (priority) {
            form.setValue("name", priority.name);
            form.setValue("colorRGB", priority.colorRGB);
            form.setValue("id", priority.id);
            form.setValue("projectId", priority.projectId);
        }
    }, [priority]); // ✅ Chỉ chạy khi `type` thay đổi

    const { mutate: mutateCreate, isPending: isPendingCreate } = useMutation({
        mutationKey: ['create'],
        mutationFn: (value: Priority) => priorityApiRequest.create(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Tạo mới thành công",
                message: "Bạn đã tạo mới thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-priorities'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete'],
        mutationFn: () => priorityApiRequest.delete(priority!),
        onSuccess: () => {
            handleSuccessApi({
                title: "Xóa ưu tiên thành công",
                message: "Bạn đã xóa ưu tiên thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-priorities'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateUpdate, isPending: isPendingUpdate } = useMutation({
        mutationKey: ['update'],
        mutationFn: (value: Priority) => priorityApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Sửa ưu tiên thành công",
                message: "Bạn đã sửa ưu tiên thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-priorities'] });
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

    const onSubmit = (values: Priority) => {
        if (state === State.CREATE) mutateCreate(values);
        else if (state === State.UPDATE) mutateUpdate(values)

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
                                                <Input placeholder="Ưu tiên A1" {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                            </div>
                            <div className="flex flex-col space-y-1.5 flex-1 mt-2">
                                <FormField
                                    control={form.control}
                                    name="colorRGB"
                                    render={({ field }) => (
                                        <FormItem>
                                            <FormLabel>Chọn màu</FormLabel>
                                            <FormControl>
                                                <div className="flex items-center space-x-2">
                                                    <HexColorPicker
                                                        color={field.value}
                                                        onChange={field.onChange}
                                                        className="w-24 h-24 rounded-md border"
                                                    />
                                                    <Input
                                                        type="text"
                                                        value={field.value}
                                                        onChange={(e) => field.onChange(e.target.value)}
                                                        className="w-24 text-center"
                                                    />
                                                </div>
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
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
