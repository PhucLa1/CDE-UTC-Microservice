import viewApiRequest from '@/apis/view.api'
import { useQuery } from '@tanstack/react-query'
import React from 'react'
import { Separator } from "@/components/ui/separator"
import {
    Sheet,
    SheetContent,
    SheetDescription,
    SheetHeader,
    SheetTitle,
    SheetTrigger,
} from "@/components/ui/sheet"
import { DownloadIcon, FolderIcon, MoveLeftIcon, Pencil, PercentCircle, Trash, TrashIcon } from "lucide-react"
import { ReactNode, useState } from "react"
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip"
import { Card } from "@/components/ui/card"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Role } from "@/data/enums/role.enum"
import { Button } from "@/components/custom/button"
import { useRole } from '../../layout'
import CreateViewComment from './create-view-comment'
import DeleteViewComment from './delete-view-comment'
import UpdateViewComment from './update-view-comment'
import { UpdateView } from './update-view'
type FormProps = {
    node: ReactNode,
    id: number,
    projectId: number,
    isOpen: boolean,
    setIsOpen: (value: boolean) => void
}

export default function SheetView({ node, id, projectId, isOpen, setIsOpen }: FormProps) {
    const [updateComment, setUpdateComment] = useState<number>(0)
    const { roleDetail } = useRole()
    console.log(updateComment)
    const { data, isLoading } = useQuery({
        queryKey: ['get-detail-view', id],
        queryFn: () => viewApiRequest.getById(id)
    })
    if (isLoading) return <></>
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
                        <span className="text-[14px] font-semibold">
                            {data?.data.name}
                        </span>
                        {roleDetail?.role !== Role.Admin && data?.data.createdBy !== roleDetail?.id
                            ? <></>
                            : <UpdateView node={<Pencil className="h-5 w-5" />} projectId={projectId} view={data?.data!} />
                        }

                    </div>
                    <Separator className="my-2" />
                    <div className="flex items-center justify-center">
                        <FolderIcon className="w-10 h-10 font-semibold" />
                    </div>
                    <Separator className="my-2" />
                    <div className="flex h-5 items-center justify-center space-x-10 text-sm">
                        <Button>Xem views</Button>
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
                        {/* {roleDetail?.role == Role.Admin && <DeleteFolder
                            setSheetOpen={setIsOpen}
                            node={
                                <div className="flex items-center gap-1 text-red-500 cursor-pointer">
                                    <TrashIcon className="h-5 w-5" />
                                </div>
                            }
                            folder={{
                                id: id,
                                name: "",
                                projectId: projectId
                            }}
                        />} */}

                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Chi tiết chế độ xem</h5>
                        <div className="text-[12px]">
                            <div className="mt-2">
                                <span>Mô tả</span>
                                <p>{data?.data.description}</p>
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
                            {(data?.data.tagResults)?.map((item, index) => (
                                <li
                                    key={index}
                                    className="flex items-center bg-[#eaeaef] rounded-3xl text-[#6a6976] h-8 justify-between mt-2 min-h-8 overflow-hidden pl-3 w-auto mr-2 mb-2 max-w-[16rem] px-2"
                                >
                                    <span>{item.name}</span>
                                </li>
                            ))}
                        </ul>

                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Bình luận</h5>
                        <div className="mt-2">
                            {
                                (data?.data.userCommentResults)?.map((item, index) => {
                                    if (updateComment != item.id!) {
                                        return <Card key={index} className="mb-4 p-2">
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
                                                    <DeleteViewComment
                                                        node={<Trash className="w-4 h-4 cursor-pointer text-gray-500 hover:text-gray-900" />}
                                                        id={item.id!}
                                                        projectId={projectId}
                                                    />
                                                    <Pencil onClick={() => setUpdateComment(item.id!)} className="w-4 h-4 ml-2 cursor-pointer text-gray-500 hover:text-gray-900" />
                                                </div>

                                            </div>
                                        </Card>
                                    }
                                    else {
                                        return <UpdateViewComment
                                            key={index}
                                            setUpdateComment={setUpdateComment}
                                            viewComment={{
                                                content: item.content,
                                                id: item.id,
                                                projectId: projectId,
                                                viewId: id
                                            }} />
                                    }
                                })
                            }
                            <CreateViewComment id={id} projectId={projectId} />
                        </div>
                    </div>
                </div>
            </SheetContent >
        </Sheet >
    )
}
