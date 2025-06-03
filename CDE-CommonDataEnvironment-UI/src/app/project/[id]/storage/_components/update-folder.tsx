import folderApiRequest from '@/apis/folder.api'
import tagApiRequest from "@/apis/tag.api"
import { Button } from '@/components/custom/button'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'
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
import { Folder, folderDefault, folderSchema } from '@/data/schema/Project/folder.schema'
import { handleSuccessApi } from '@/lib/utils'
import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { ReactNode, useEffect, useState } from "react"
import { useForm } from 'react-hook-form'
import Select from "react-select"
type FormProps = {
    node: ReactNode,
    projectId: number,
    folder: Folder
}

export function UpdateFolder({ node, projectId, folder }: FormProps) {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const queryClient = useQueryClient();
    const [selectedTagIds, setSelectedTagIds] = useState<{ label: string, value: number }[]>([]);

    const form = useForm<Folder>({
        resolver: zodResolver(folderSchema),
        defaultValues: folderDefault
    });
    const onSubmit = (values: Folder) => {
        values.id = folder.id
        values.projectId = projectId
        values.tagIds = selectedTagIds.map(tag => tag.value)
        mutate(values)
    };
    useEffect(() => {
        if (folder.tagResults) {
            setSelectedTagIds(folder.tagResults.map((item) => ({
                label: item.name,
                value: item.id!
            })));
        }
        form.setValue('name', folder.name);
    }, [folder]); // Loại bỏ `selectedTagIds` khỏi dependency array 

    const { data, isLoading } = useQuery({
        queryKey: ['get-all-tags'],
        queryFn: () => tagApiRequest.getList(projectId)
    })

    const { mutate, isPending } = useMutation({
        mutationKey: ['update-folder'],
        mutationFn: (value: Folder) => folderApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Cập nhật thư mục',
                message: 'Cập nhật thư mục thành công'
            })
            setIsOpen(false)
            form.reset()
            queryClient.invalidateQueries({ queryKey: ['get-detail-folder', folder.id] })
            queryClient.invalidateQueries({ queryKey: ['storage'] })
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
                    <SheetTitle>Cập nhật thư mục</SheetTitle>
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
                                            <Input placeholder="Thư mục A1" {...field} autoFocus />
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
