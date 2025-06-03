import { Card } from "@/components/ui/card";
import { MoreVertical } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { View } from "@/data/schema/Project/view.schema";
import SheetView from "./sheet-view";

type FormProps = {
    data: View[];
    projectId: number;
};

export default function GridStorage({ data, projectId }: FormProps) {
    const router = useRouter();
    console.log(router);
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const [id, setId] = useState<number>(0)
    return (
        <div>
            {id != 0 && <SheetView
                node={<></>}
                projectId={projectId}
                id={id}
                isOpen={isOpen}
                setIsOpen={setIsOpen} />}


            {/* Tệp */}
            <p className="text-sm mb-4 font-bold">Chế độ xem</p>
            <div className="grid grid-cols-5 gap-4">
                {data.map((item, index) => (
                    <Card key={index} className="p-4 flex flex-col items-center gap-2 relative">
                        <img src="/images/note.png" width={35} />
                        <span className="truncate w-full text-center text-sm">{item.name}</span>
                        {/* Icon ba chấm */}
                        <Button onClick={() => {
                            setId(item.id!)
                            setIsOpen(true)
                        }} variant="ghost" size="icon" className="absolute top-2 right-2">
                            <MoreVertical size={20} />
                        </Button>
                    </Card>
                ))}
            </div>
        </div>
    );
}
