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
import { Tag, tagDefault, tagSchema } from '@/data/schema/Project/tag.schema';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { State } from '@/data/enums/state.enum';
import { Button } from '@/components/custom/button';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import tagApiRequest from '@/apis/tag.api';
import { handleSuccessApi } from '@/lib/utils';

type Props = {
    trigger: ReactNode;
    state: State,
    tag?: Tag,
    ids? : number[]
}

export default function FormCRUD({ trigger, state, tag, ids }: Props) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();

    const form = useForm<Tag>({
        resolver: zodResolver(tagSchema),
        defaultValues: tagDefault
    });

    useEffect(() => {
        if (tag) {
            form.setValue("name", tag.name);
            form.setValue("projectId", tag.projectId);
            form.setValue("id", tag.id);
        }
    }, [tag]); // ✅ Chỉ chạy khi `tag` thay đổi

    const { mutate: mutateCreate, isPending: isPendingCreate } = useMutation({
        mutationKey: ['create'],
        mutationFn: (value: Tag) => tagApiRequest.create(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Tạo mới thành công",
                message: "Bạn đã tạo mới thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-tags'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
        mutationKey: ['delete'],
        mutationFn: () => tagApiRequest.delete({
            ids: ids ? ids : [tag!.id!],
            projectId: tag!.projectId!
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: "Xóa nhãn dán thành công",
                message: "Bạn đã xóa nhãn dán thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-tags'] });
            setIsOpen(false); // ✅ Đóng dialog sau khi tạo thành công
        }
    });
    const { mutate: mutateUpdate, isPending: isPendingUpdate } = useMutation({
        mutationKey: ['update'],
        mutationFn: (value: Tag) => tagApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: "Sửa nhãn dán thành công",
                message: "Bạn đã sửa nhãn dán thành công"
            });
            queryClient.invalidateQueries({ queryKey: ['get-list-tags'] });
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

    const onSubmit = (values: Tag) => {
        if(state === State.CREATE) mutateCreate(values);
        else if(state === State.UPDATE) mutateUpdate(values)
        
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
                                                <Input placeholder="Dãn nhán A1" {...field} />
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
