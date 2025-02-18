import { Button } from "@/components/ui/button"
import { Separator } from "@/components/ui/separator"
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
import { DownloadIcon, FolderIcon, MoreVertical, MoveIcon, MoveLeftIcon, Pencil, PercentCircle, TrashIcon } from "lucide-react"
import { ReactNode } from "react"
import {
    Tooltip,
    TooltipContent,
    TooltipProvider,
    TooltipTrigger,
} from "@/components/ui/tooltip"
import { Card } from "@/components/ui/card"
import { Avatar } from "@/components/ui/avatar"
import { Input } from "@/components/ui/input"
import { Textarea } from "@/components/ui/textarea"
type FormProps = {
    node: ReactNode
}
export default function SheetFolder({ node }: FormProps) {
    return (
        <Sheet>
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
                        <span className="text-[14px] font-semibold">Tên folder</span>
                        <Pencil className="h-5 w-5" />
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
                                <p>12giowf</p>
                            </div>
                            <div className="mt-2">
                                <span>Được tạo bởi</span>
                                <p>3 phiên bản trước đó</p>
                            </div>
                        </div>
                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Nhãn dán</h5>
                        <ul className="flex flex-wrap">
                            <li className="flex items-center bg-[#eaeaef] rounded-3xl text-[#6a6976] h-8 justify-between mt-2 min-h-8 overflow-hidden pl-3 w-auto mr-2 mb-2 max-w-[16rem] px-2">
                                <span>
                                    1434
                                </span>
                            </li>
                            <li className="flex items-center bg-[#eaeaef] rounded-3xl text-[#6a6976] h-8 justify-center mt-2 min-h-8 overflow-hidden pl-3 w-auto mr-2 mb-2 max-w-[16rem] px-2">
                                <span>
                                    15345
                                </span>
                            </li>
                        </ul>
                    </div>
                    <Separator className="my-2" />
                    <div>
                        <h5 className="font-bold">Bình luận</h5>
                        <div className="mt-2">
                            <Card className="mb-4 p-2">
                                <div className="flex items-center justify-between">
                                    <div className="flex items-start">
                                        <div className="mr-3">
                                            <Avatar />
                                        </div>
                                        <div className="text-xs">
                                            <p className="font-semibold">Phúc Là</p>
                                            <p className="text-sm text-gray-500">Feb 18, 2025</p>
                                            <p className="mt-2">csdfs</p>
                                        </div>
                                    </div>
                                    <MoreVertical className="w-4 h-4" />
                                </div>
                            </Card>
                            <div className="mt-4">
                                <Textarea placeholder="Viêt thêm vình luận vào đây..." />
                                <Button className="mt-2">Thêm bình luận</Button>
                            </div>
                        </div>
                    </div>
                </div>
            </SheetContent >
        </Sheet >
    )
}
