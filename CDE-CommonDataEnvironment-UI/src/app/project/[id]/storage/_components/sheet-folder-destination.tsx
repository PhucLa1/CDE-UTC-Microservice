import fileApiRequest from "@/apis/file.api"
import folderApiRequest from "@/apis/folder.api"
import storageApiRequest from "@/apis/storage.api"
import AppBreadcrumbSheet from "@/components/custom/_breadcrumb_sheet"
import { Button } from "@/components/custom/button"
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
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { handleSuccessApi } from "@/lib/utils"
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"
import { ChevronUpIcon, FolderIcon, ArrowRightIcon } from "lucide-react"
import { useState } from "react"

type FormProps = {
    fileIds: number[]
    folderIds: number[],
    isOpen: boolean,
    setIsOpen: (value: boolean) => void,
    isFile: boolean
}
export function SheetFolderDestination({ fileIds, folderIds, isOpen, setIsOpen, isFile }: FormProps) {
    const [parentId, setParentId] = useState<number>(0)
    const queryClient = useQueryClient()
    const { data, isLoading } = useQuery({
        queryKey: ['get-folder-destination', parentId],
        queryFn: () => folderApiRequest.getFoldersDestination({
            fileIds: fileIds,
            folderIds: folderIds,
            parentId: parentId
        })
    })
    const { data: dataPath, isLoading: isLoadingPath } = useQuery({
        queryKey: ['full-path', parentId],
        queryFn: () => storageApiRequest.getFullPath(parentId),
    })

    const { mutate: mutateFile, isPending: isPendingFile } = useMutation({
        mutationKey: ['move-file'],
        mutationFn: () => fileApiRequest.moveFile({
            id: fileIds[0]!,
            folderId: parentId,
            name: ""
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Di chuyển file thành công',
                message: 'File đã được di chuyển thành công',
            })
            queryClient.invalidateQueries({ queryKey: ['storage'] })
            setIsOpen(false)
        }
    })
    const { mutate: mutateFolder, isPending: isPendingFolder } = useMutation({
        mutationKey: ['move-folder'],
        mutationFn: () => folderApiRequest.moveFolder({
            id: folderIds[0]!,
            parentId: parentId,
            name: ""
        }),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Di chuyển thư mục thành công',
                message: 'Thư mục đã được di chuyển thành công',
            })
            queryClient.invalidateQueries({ queryKey: ['storage'] })
            setIsOpen(false)
        }
    })
    return (
        <Sheet open={isOpen} onOpenChange={setIsOpen}>
            <SheetTrigger asChild>
            </SheetTrigger>
            <SheetContent>
                <SheetHeader>
                    <SheetTitle>Di chuyển thư mục & tệp</SheetTitle>
                    <SheetDescription>
                        Di chuyển thư mục & tệp tới thư mục khác
                    </SheetDescription>
                </SheetHeader>
                <div className="grid gap-4 py-4">
                    {isLoadingPath ? <></> : <AppBreadcrumbSheet pathList={dataPath!.data.map((item) => {
                        return {
                            name: item.name,
                            url: `${item.folderId}`
                        }
                    })}
                        setParentId={setParentId}
                        className="mt-2" />}
                    <Table>
                        <TableHeader>
                            <TableRow>
                                <TableHead className="w-[400px]">
                                    Tên <ChevronUpIcon className="inline-block ml-2 h-4 w-4" />
                                </TableHead>
                                <TableHead></TableHead>
                            </TableRow>
                        </TableHeader>
                        <TableBody>
                            {isLoading ? <></> : data?.data.map((item, index) => (
                                <TableRow
                                    className='h-[60px]' key={index}>
                                    <TableCell className="font-medium cursor-pointer">
                                        <div className="flex items-center gap-2">
                                            <FolderIcon className="h-6 w-6 text-blue-500" />
                                            {item.name}
                                        </div>
                                    </TableCell>
                                    <TableCell><ArrowRightIcon onClick={() => setParentId(item.id!)} className='h-5 w-5 text-gray-800 cursor-pointer hover:text-gray-200' /></TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </div>
                <SheetFooter>
                    <SheetClose asChild>
                        <Button loading={isPendingFile || isPendingFolder}
                            onClick={() => {
                                if(isFile) mutateFile()
                                else mutateFolder()
                                
                            }}
                            type="submit">Chuyển tới thư mục này</Button>
                    </SheetClose>
                </SheetFooter>
            </SheetContent>
        </Sheet>
    )
}
