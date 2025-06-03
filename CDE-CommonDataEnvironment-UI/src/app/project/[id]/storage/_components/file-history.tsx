import {
    Sheet,
    SheetContent,
    SheetDescription,
    SheetHeader,
    SheetTitle,
    SheetTrigger,
} from "@/components/ui/sheet"
import { FileHistory } from "@/data/schema/Project/filehistory.schema"
import { ReactNode } from "react"

type FormProps = {
    fileHistories?: FileHistory[],
    node: ReactNode
}

export default function FileHistoryPage({ fileHistories, node }: FormProps) {
    return (
        <Sheet>
            <SheetTrigger asChild>
                {node}
            </SheetTrigger>
            <SheetContent className="overflow-auto">
                <SheetHeader>
                    <SheetTitle>Thông tin về những lần chỉnh sửa cũ</SheetTitle>
                    <SheetDescription>
                        Dưới đây là lịch sử chỉnh sửa của tệp này.
                    </SheetDescription>
                </SheetHeader>
                <div className="space-y-4 mt-4">
                    {fileHistories?.map((history, index) => (
                        <div key={index} className="flex items-center gap-3 border-b pb-3 last:border-b-0">
                            <img src={history.imageUrl} className="w-6 h-6" />
                            <div className="flex flex-col">
                                <span className="font-semibold">V.{history.version} • {history.name}</span>
                                <span className="text-sm text-gray-500">{history.nameCreatedBy}</span>
                                <span className="text-xs text-gray-400">{history.createdAt}</span>
                            </div>
                        </div>
                    ))}
                </div>
            </SheetContent>
        </Sheet>
    )
}
