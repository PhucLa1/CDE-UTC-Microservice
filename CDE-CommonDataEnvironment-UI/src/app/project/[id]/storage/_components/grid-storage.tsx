import { Card } from "@/components/ui/card";
import { Storage } from "@/data/schema/Project/storage.schema";
import { Folder, MoreVertical } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import { useState } from "react";
import SheetStorage from "./sheet-storage";

type FormProps = {
    data: Storage[];
    projectId: number;
};

export default function GridStorage({ data, projectId }: FormProps) {
    const router = useRouter();
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const [id, setId] = useState<number>(0)
    const [isFile, setIsFile] = useState<number>(0);  //1: File, 2 : Folder
    return (
        <div>
            {id != 0 && isFile != 0 && <SheetStorage
                isFile={isFile == 1 ? true : false}
                projectId={projectId} id={id}
                node={<></>}
                isOpen={isOpen}
                setIsOpen={setIsOpen} />}
            {/* Thư mục */}
            <p className="text-sm mb-4 font-bold">Thư mục</p>
            <div className="grid grid-cols-5 gap-4 mb-6">
                {data.filter(item => !item.isFile).map((item, index) => (
                    <Card key={index} className="p-4 flex items-center justify-between cursor-pointer">
                        <div onClick={() => {
                            router.push(`${item.id}`);
                        }} className="flex items-center gap-3 flex-1 overflow-hidden">
                            <Folder className="text-yellow-500" size={24} />
                            <span className="truncate overflow-hidden text-ellipsis whitespace-nowrap">{item.name}</span>
                        </div>
                        {/* Icon ba chấm */}
                        <Button onClick={() => {
                            setIsFile(2)
                            setIsOpen(true);
                            setId(item.id!);
                        }} variant="ghost" size="icon">
                            <MoreVertical size={20} />
                        </Button>
                    </Card>
                ))}
            </div>

            {/* Tệp */}
            <p className="text-sm mb-4 font-bold">Tệp</p>
            <div className="grid grid-cols-5 gap-4">
                {data.filter(item => item.isFile).map((item, index) => (
                    <Card key={index} className="p-4 flex flex-col items-center gap-2 relative">
                        <img src={item.urlImage} alt="" className="w-32 object-cover" />
                        <span className="truncate w-full text-center text-sm">{item.name}</span>
                        {/* Icon ba chấm */}
                        <Button onClick={() => {
                            setIsFile(1)
                            setIsOpen(true);
                            setId(item.id!);
                        }} variant="ghost" size="icon" className="absolute top-2 right-2">
                            <MoreVertical size={20} />
                        </Button>
                    </Card>
                ))}
            </div>
        </div>
    );
}
