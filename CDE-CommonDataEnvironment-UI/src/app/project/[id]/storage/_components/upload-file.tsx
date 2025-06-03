'use client';
import fileApiRequest from "@/apis/file.api";
import { Button } from "@/components/ui/button";
import { FileState, MultiFileDropzone } from "@/components/ui/multi-dropzone";
import {
    Sheet,
    SheetContent,
    SheetDescription,
    SheetFooter,
    SheetHeader,
    SheetTitle,
    SheetTrigger
} from "@/components/ui/sheet";
import { File } from "@/data/schema/Project/file.schema";
import { useEdgeStore } from "@/lib/edgestore";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import path from "path";
import { ReactNode, useState } from "react";

type FormProps = {
    node: ReactNode;
    folderId: number,
    projectId: number
};

export default function UploadFile({ node, folderId, projectId }: FormProps) {
    const [open, setOpen] = useState(false);
    const [fileStates, setFileStates] = useState<FileState[]>([]);
    const { edgestore } = useEdgeStore();
    const queryClient = useQueryClient()

    const { mutate } = useMutation({
        mutationKey: ['create-file'],
        mutationFn: (value: File) => fileApiRequest.create(value),
        onSuccess: () => {
            setOpen(false)
            queryClient.invalidateQueries({ queryKey: ['storage'] })
            //Gọi lại API
        },
        onError: () => {
            setOpen(false)
        }
    })
    const updateFileProgress = (key: string, progress: FileState['progress']) => {
        setFileStates((fileStates) => {
            const newFileStates = structuredClone(fileStates);
            const fileState = newFileStates.find(
                (fileState) => fileState.key === key,
            );
            if (fileState) {
                fileState.progress = progress;
            }
            return newFileStates;
        });
    }
    return (
        <Sheet open={open} onOpenChange={setOpen}>
            <SheetTrigger asChild>{node}</SheetTrigger>
            <SheetContent className="w-96">
                <SheetHeader>
                    <SheetTitle>Tải lên tệp</SheetTitle>
                    <SheetDescription>
                        Bạn có thể chọn một hoặc nhiều tệp.
                    </SheetDescription>
                </SheetHeader>
                <div>
                    <MultiFileDropzone
                        className="mt-2"
                        value={fileStates}
                        onChange={(files) => {
                            setFileStates(files);
                        }}
                        onFilesAdded={async (addedFiles) => {
                            setFileStates([...fileStates, ...addedFiles]);
                            await Promise.all(
                                addedFiles.map(async (addedFileState) => {
                                    try {
                                        const res = await edgestore.publicFiles.upload({
                                            file: addedFileState.file,
                                            onProgressChange: async (progress) => {
                                                updateFileProgress(addedFileState.key, progress);
                                                if (progress === 100) {
                                                    // wait 1 second to set it to complete
                                                    // so that the user can see the progress bar at 100%
                                                    await new Promise((resolve) => setTimeout(resolve, 1000));
                                                    updateFileProgress(addedFileState.key, 'COMPLETE');
                                                }
                                            },
                                        });
                                        mutate({
                                            name: path.parse(addedFileState.file.name).name,
                                            url: res.url,
                                            size: res.size,
                                            folderId: folderId,
                                            projectId: projectId,
                                            mimeType: addedFileState.file.type == "" ? "mimeType" : addedFileState.file.type,
                                            extension: path.parse(addedFileState.file.name).ext == "" ? ".extension" : path.parse(addedFileState.file.name).ext
                                        })
                                        console.log({
                                            name: path.parse(addedFileState.file.name).name,
                                            url: res.url,
                                            size: res.size,
                                            folderId: folderId,
                                            projectId: projectId,
                                            mimeType: addedFileState.file.type == "" ? "mimeType" : addedFileState.file.type,
                                            extension: path.parse(addedFileState.file.name).ext == "" ? ".extension" : path.parse(addedFileState.file.name).ext
                                        })
                                    } catch (err) {
                                        updateFileProgress(addedFileState.key, 'ERROR');
                                    }
                                }),
                            );
                        }}
                    />
                </div>
                <SheetFooter className="mt-2">
                    <Button variant="outline" onClick={() => setOpen(false)}>
                        Đóng
                    </Button>
                </SheetFooter>
            </SheetContent>
        </Sheet>
    );
}
