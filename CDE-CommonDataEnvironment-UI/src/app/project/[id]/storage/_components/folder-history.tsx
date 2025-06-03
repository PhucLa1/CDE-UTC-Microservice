import {
    Sheet,
    SheetContent,
    SheetDescription,
    SheetHeader,
    SheetTitle,
    SheetTrigger
} from "@/components/ui/sheet"
import { FolderHistory } from "@/data/schema/Project/folderhistory.schema"
import { FolderIcon } from "lucide-react"
import { ReactNode } from "react"

type FormProps = {
    folderHistories?: FolderHistory[],
    node: ReactNode
}

export default function FolderHistoryPage({ folderHistories, node }: FormProps) {
    return (
        <Sheet>
            <SheetTrigger asChild>
                {node}
            </SheetTrigger>
            <SheetContent>
                <SheetHeader>
                    <SheetTitle>Thông tin về những lần chỉnh sửa cũ</SheetTitle>
                    <SheetDescription>
                        Dưới đây là lịch sử chỉnh sửa của folder này.
                    </SheetDescription>
                </SheetHeader>
                <div className="space-y-4 mt-4">
                    {folderHistories?.map((history, index) => (
                        <div key={index} className="flex items-center gap-3 border-b pb-3 last:border-b-0">
                            <FolderIcon className="w-6 h-6 text-gray-500" />
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
