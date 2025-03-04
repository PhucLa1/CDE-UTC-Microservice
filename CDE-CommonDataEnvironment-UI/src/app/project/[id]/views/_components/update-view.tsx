import { Input } from "@/components/ui/input"
import {
    Sheet,
    SheetClose,
    SheetContent,
    SheetDescription,
    SheetFooter,
    SheetHeader,
    SheetTitle,
    SheetTrigger,
} from "@/components/ui/sheet"
import { ReactNode, useEffect, useState } from "react"
import { Button } from '@/components/custom/button'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { useForm } from 'react-hook-form'
import { View, viewDefault, viewSchema } from '@/data/schema/Project/view.schema'
import { zodResolver } from '@hookform/resolvers/zod'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
import viewApiRequest from '@/apis/view.api'
import { handleSuccessApi } from '@/lib/utils'
import Select from "react-select";
import tagApiRequest from "@/apis/tag.api"
type FormProps = {
    node: ReactNode,
    projectId: number,
    view: View
}

export function UpdateView({ node, projectId, view }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();
    const [selectedTagIds, setSelectedTagIds] = useState<{ label: string, value: number }[]>([]);

    const form = useForm<View>({
        resolver: zodResolver(viewSchema),
        defaultValues: viewDefault
    });
    const onSubmit = (values: View) => {
        values.id = view.id
        values.projectId = projectId
        values.tagIds = selectedTagIds.map(tag => tag.value)
        console.log(values)
        mutate(values)
    };
    useEffect(() => {
        if (view.tagResults) {
            setSelectedTagIds(view.tagResults.map((item) => ({
                label: item.name,
                value: item.id!
            })));
        }
        form.setValue('name', view.name);
        form.setValue('description', view.description);
    }, [view]); // Loại bỏ `selectedTagIds` khỏi dependency array 

    const { data, isLoading } = useQuery({
        queryKey: ['get-all-tags'],
        queryFn: () => tagApiRequest.getList(projectId)
    })

    const { mutate, isPending } = useMutation({
        mutationKey: ['update-view'],
        mutationFn: (value: View) => viewApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Cập nhật chế độ xem',
                message: 'Cập nhật chế độ xem thành công'
            })
            setIsOpen(false)
            form.reset()
            queryClient.invalidateQueries({ queryKey: ['get-detail-view', view.id] })
            queryClient.invalidateQueries({ queryKey: ['views'] })
            //Gọi lại API
        },
        onError: () => {
            setIsOpen(false)
            form.reset()
        }
    })
    if (isLoading) return <></>
    return (
        <Sheet open={isOpen} onOpenChange={setIsOpen}>
            <SheetTrigger asChild>
                {node}
            </SheetTrigger>
            <SheetContent>
                <SheetHeader>
                    <SheetTitle>Cập nhật chế độ xem</SheetTitle>
                    <SheetDescription>
                        Tạo ra những thay đổi của bạn tại đâyy.
                    </SheetDescription>
                </SheetHeader>
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
                                            <Input placeholder="Chế độ xem A1" {...field} autoFocus />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>
                        <div className="flex flex-col space-y-1.5 flex-1">
                            <FormField
                                control={form.control}
                                name="description"
                                render={({ field }) => (
                                    <FormItem>
                                        <FormLabel>Mô tả</FormLabel>
                                        <FormControl>
                                            <Input placeholder="Mô tả" {...field} autoFocus />
                                        </FormControl>
                                        <FormMessage />
                                    </FormItem>
                                )}
                            />
                        </div>
                        <div className="my-6"></div>
                        <Select
                            isMulti
                            options={data!.data
                                .map(item => ({ label: item.name, value: item.id! }))}
                            value={selectedTagIds}
                            className="w-full"
                            placeholder="Thêm nhãn dán"
                            onChange={(newValue) => setSelectedTagIds([...newValue])} // Chuyển từ readonly sang mảng mutable
                        />
                        <SheetFooter className='mt-2'>
                            <SheetClose>
                                <Button className="mr-2" loading={isPending} type='submit'>Tiêp tục</Button>
                                <Button variant={'destructive'}>Hủy</Button>
                            </SheetClose>
                        </SheetFooter>
                    </form>
                </Form>
            </SheetContent>
        </Sheet>
    )
}
