import { Button } from "@/components/custom/button"
import { Separator } from "@/components/ui/separator"
import {
    Sheet,
    SheetContent,
    SheetDescription,
    SheetHeader,
    SheetTitle,
    SheetTrigger,
} from "@/components/ui/sheet"
import { DownloadIcon, FolderIcon, MoreVertical, MoveIcon, MoveLeftIcon, Pencil, PercentCircle, Trash, TrashIcon } from "lucide-react"
import { ReactNode, useState } from "react"
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip"
import { Card } from "@/components/ui/card"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Textarea } from "@/components/ui/textarea"
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"
import folderApiRequest from "@/apis/folder.api"
import { useRole } from "../../layout"
import { Role } from "@/data/enums/role.enum"
import { UpdateFolder } from "./update-folder"
import { FolderComment, folderCommentDefault, folderCommentSchema } from "@/data/schema/Project/foldercomment.schema"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form"
import folderCommentApiRequest from "@/apis/foldercomment.api"
import DeleteForm from "./delete-form"
import UpdateComment from "./update-comment"
type FormProps = {
    node: ReactNode,
    id: number,
    isOpen: boolean,
    setIsOpen: (value: boolean) => void,
    projectId: number
}
export default function SheetFolder({ node, id, isOpen, setIsOpen, projectId }: FormProps) {
    const [updateComment, setUpdateComment] = useState<number>(0)
    const { data, isLoading } = useQuery({
        queryKey: ['get-detail-folder', id],
        queryFn: () => folderApiRequest.getDetail(id),
        enabled: id !== 0
    })
    const { roleDetail } = useRole()
    const form = useForm<FolderComment>({
        resolver: zodResolver(folderCommentSchema),
        defaultValues: folderCommentDefault
    });
    const queryClient = useQueryClient()
    const onSubmit = (values: FolderComment) => {
        values.folderId = id
        values.projectId = projectId
        mutate(values)
    };

    const { mutate, isPending } = useMutation({
        mutationKey: ['create-comment'],
        mutationFn: (value: FolderComment) => folderCommentApiRequest.create(value),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['get-detail-folder', id] })
            form.reset()

        }
    })

    if (isLoading && data) return <></>
    return (
        <Sheet open={isOpen} onOpenChange={setIsOpen}>
            <SheetTrigger asChild>
                {node}
            </SheetTrigger>
            <SheetContent className="overflow-y-auto">
                <SheetHeader>
                    <SheetTitle>Thông tin chi tiết</SheetTitle>
                    <SheetDescription>
                        Tạo ra những thay đổi của bạn ở đâyy.
                    </SheetDescription>
                </SheetHeader>
                <div className="grid gap-4 py-4">
                    <div className="flex items-center justify-between">
                        <span className="text-[14px] font-semibold">{data?.data.name}</span>
                        {roleDetail?.role !== Role.Admin && data?.data.createdBy !== roleDetail?.id
                            ? <></>
                            : <UpdateFolder folder={data!.data} projectId={projectId} node={<Pencil className="h-5 w-5" />} />}
                    </div>
                    <Separator className="my-2" />
                    <div className="flex items-center justify-center">
                        <FolderIcon className="w-10 h-10 font-semibold" />
                    </div>
                    <Separator className="my-" />
                    <div className="flex h-5 items-center justify-center space-x-10 text-sm">
                        <TooltipProvider>
                            <Tooltip>
                                <TooltipTrigger asChild>
                                    <div className="flex items-center gap-1">
                                        <PercentCircle className="h-5 w-5" />
                                    </div>
                                </TooltipTrigger>
                                <TooltipContent>
                                    <p>Chỉnh quyền</p>
                                </TooltipContent>
                            </Tooltip>
                        </TooltipProvider>
                        <TooltipProvider>
                            <Tooltip>
                                <TooltipTrigger asChild>
                                    <div className="flex items-center gap-1">
                                        <DownloadIcon className="h-5 w-5" />
                                    </div>
                                </TooltipTrigger>
                                <TooltipContent>
                                    <p>Tải thư mục</p>
                                </TooltipContent>
                            </Tooltip>
                        </TooltipProvider>
                        <TooltipProvider>
                            <Tooltip>
                                <TooltipTrigger asChild>
                                    <div className="flex items-center gap-1">
                                        <MoveLeftIcon className="h-5 w-5" />
                                    </div>
                                </TooltipTrigger>
                                <TooltipContent>
                                    <p>Di chuyển thư mục</p>
                                </TooltipContent>
                            </Tooltip>
                        </TooltipProvider>
                        <TooltipProvider>
                            <Tooltip>
                                <TooltipTrigger asChild>
                                    <div className="flex items-center gap-1 text-red-500 cursor-pointer">
                                        <TrashIcon className="h-5 w-5" />
                                    </div>
                                </TooltipTrigger>
                                <TooltipContent>
                                    <p>Xóa thư mục</p>
                                </TooltipContent>
                            </Tooltip>
                        </TooltipProvider>
                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Chi tiết thư mục</h5>
                        <div className="text-[12px]">
                            <div className="mt-2">
                                <span>Phiên bản</span>
                                <p>3 phiên bản trước đó</p>
                            </div>
                            <div className="mt-2">
                                <span>Ngày tạo</span>
                                <p>{data?.data.createdAt}</p>
                            </div>
                            <div className="mt-2">
                                <span>Được tạo bởi</span>
                                <p>{data?.data.nameCreatedBy}</p>
                            </div>
                        </div>
                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Nhãn dán</h5>
                        <ul className="flex flex-wrap">
                            {
                                data?.data.tagResults?.map((item, index) => {
                                    return <li key={index} className="flex items-center bg-[#eaeaef] rounded-3xl text-[#6a6976] h-8 justify-between mt-2 min-h-8 overflow-hidden pl-3 w-auto mr-2 mb-2 max-w-[16rem] px-2">
                                        <span>
                                            {item.name}
                                        </span>
                                    </li>
                                })
                            }
                        </ul>
                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Bình luận</h5>
                        <div className="mt-2">
                            {
                                data?.data.userCommentResults?.map((item, index) => {
                                    if (updateComment != item.id!) {
                                        return <Card onClick={() => setUpdateComment(item.id!)} key={index} className="mb-4 p-2">
                                            <div className="flex items-center justify-between">
                                                <div className="flex items-start">
                                                    <div className="mr-3">
                                                        <Avatar>
                                                            <AvatarImage src={item.avatarUrl} />
                                                            <AvatarFallback>CN</AvatarFallback>
                                                        </Avatar>
                                                    </div>
                                                    <div className="text-xs">
                                                        <p className="font-semibold">{item.name}</p>
                                                        <p className="text-xs text-gray-500">{item.updatedAt}</p>
                                                        <p className="mt-2">{item.content}</p>
                                                    </div>
                                                </div>
                                                <div className="flex justify-end items-center">
                                                    <DeleteForm
                                                        node={<Trash className="w-4 h-4 cursor-pointer text-gray-500 hover:text-gray-900" />}
                                                        folderComment={{
                                                            content: '',
                                                            projectId: projectId,
                                                            id: item.id,
                                                            folderId: id
                                                        }}
                                                    />

                                                    <Pencil className="w-4 h-4 ml-2 cursor-pointer text-gray-500 hover:text-gray-900" />
                                                </div>

                                            </div>
                                        </Card>
                                    }
                                    else {
                                        return <UpdateComment
                                            setUpdateComment={setUpdateComment}
                                            folderComment={{
                                                content: item.content,
                                                id: item.id,
                                                projectId: projectId,
                                                folderId: id
                                            }} />
                                    }

                                })
                            }
                            <Form {...form}>
                                <form onSubmit={form.handleSubmit(onSubmit)}>
                                    <div className="mt-4">
                                        <div className="flex flex-col space-y-1.5 flex-1">
                                            <FormField
                                                control={form.control}
                                                name="content"
                                                render={({ field }) => (
                                                    <FormItem>
                                                        <FormControl>
                                                            <Textarea {...field} placeholder="Viêt thêm vình luận vào đây..." />
                                                        </FormControl>
                                                        <FormMessage />
                                                    </FormItem>
                                                )}
                                            />
                                        </div>
                                        <Button loading={isPending} type="submit" className="mt-2">Thêm bình luận</Button>
                                    </div>
                                </form>
                            </Form>
                        </div>
                    </div>
                </div>
            </SheetContent >
        </Sheet >
    )
}
