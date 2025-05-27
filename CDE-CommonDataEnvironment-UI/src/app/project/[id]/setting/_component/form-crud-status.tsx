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
import { Status, statusDefault, statusSchema } from '@/data/schema/Project/status.schema';
import statusApiRequest from '@/apis/status.api';
import { Checkbox } from '@/components/ui/checkbox';
import { HexColorPicker } from "react-colorful";
type Props = {
    trigger: ReactNode;
    state: State,
    status?: Status,
}

export default function FormCRUDStatus({ trigger, state, status }: Props) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();

    const form = useForm<Status>({
        resolver: zodResolver(statusSchema),
        defaultValues: statusDefault
    });

    useEffect(() => {
        if (status) {
            form.setValue("name", status.name);
            form.setValue("colorRGB", status.colorRGB);
            form.setValue("id", status.id);
            form.setValue("projectId", status.projectId);
        }
    }, [status]); // ✅ Chỉ chạy khi `type` thay đổi

    const { mutate: mutateCreate, isPending: isPendingCreate } = useMutation({
        mutationKey: ['create'],
        mutationFn: (value: Status) => statusApiRequest.create(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Tạo mới thành công",
                message: "Bạn đã tạo mới thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-statues'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete'],
        mutationFn: () => statusApiRequest.delete(status!),
        onSuccess: () => {
            handleSuccessApi({
                title: "Xóa trạng thái thành công",
                message: "Bạn đã xóa trạng thái thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-statues'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateUpdate, isPending: isPendingUpdate } = useMutation({
        mutationKey: ['update'],
        mutationFn: (value: Status) => statusApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Sửa trạng thái thành công",
                message: "Bạn đã sửa trạng thái thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-statues'] });
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

    const onSubmit = (values: Status) => {
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
                                                <Input placeholder="Trạng thái A1" {...field} />
                                            </FormControl>
                                            <FormMessage />
                                        </FormItem>
                                    )}
                                />
                            </div>
                            <div className="flex flex-col space-y-1.5 flex-1 mt-2">
                                <FormField
                                    control={form.control}
                                    name="isDefault"
                                    render={({ field }) => (
                                        <FormItem className="flex items-center space-x-2">
                                            <FormControl>
                                                <Checkbox
                                                    id="isDefault"
                                                    checked={field.value}
                                                    onCheckedChange={field.onChange}
                                                />
                                            </FormControl>
                                            <FormLabel htmlFor="isDefault">
                                                Mặc định chọn giá trị này khi tạo mới dữ liệu
                                            </FormLabel>
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
