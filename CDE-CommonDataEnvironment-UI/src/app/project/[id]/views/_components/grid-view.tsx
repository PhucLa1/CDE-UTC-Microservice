import { Card } from "@/components/ui/card";
import { MoreVertical } from "lucide-react";
import { Button } from "@/components/ui/button";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { View } from "@/data/schema/Project/view.schema";

type FormProps = {
    data: View[];
    projectId: number;
};

export default function GridStorage({ data, projectId }: FormProps) {
    const router = useRouter();
    const [isOpen, setIsOpen] = useState<boolean>(false);
    const [id, setId] = useState<number>(0)
    const [isFile, setIsFile] = useState<number>(0);  //1: File, 2 : Folder
    return (
        <div>
            {/* {id != 0 && isFile != 0 && <SheetStorage
                isFile={isFile == 1 ? true : false}
                projectId={projectId} id={id}
                node={<></>}
                isOpen={isOpen}
                setIsOpen={setIsOpen} />} */}
            {/* Thư mục */}


            {/* Tệp */}
            <p className="text-sm mb-4 font-bold">Chế độ xem</p>
            <div className="grid grid-cols-5 gap-4">
                {data.map((item, index) => (
                    <Card key={index} className="p-4 flex flex-col items-center gap-2 relative">
                        <img src={"https://scontent.fhan4-3.fna.fbcdn.net/v/t39.30808-1/481184629_1113245117156605_8969811852970285695_n.jpg?stp=dst-jpg_s200x200_tt6&_nc_cat=100&ccb=1-7&_nc_sid=1d2534&_nc_eui2=AeH1Qv7AYazTZ2-wi0G-3OXZ-yx8ktDPcjX7LHyS0M9yNTpFmOpBDrfJzh7zNmWVvbH-lpNmJcFcY-2l4Ued2r5v&_nc_ohc=jLol4QHziDIQ7kNvgEuETeP&_nc_oc=AdgKeDrofWEXIMvVWrwdg5PtSzwTyxc0dcaxpdwOgMrg8GCzVYj9TZDhCa-HRSAnzNMpKdSM14L7hSAZbT6-nyRp&_nc_zt=24&_nc_ht=scontent.fhan4-3.fna&_nc_gid=AcJic7jSZaJ9ib795IJVD_q&oh=00_AYBtyhuSliS0_QmDdVOMwIpvQl02noblcoKe0AiguxNuyA&oe=67C6754A"} alt="" className="w-32 object-cover" />
                        <span className="truncate w-full text-center text-sm">{item.name}</span>
                        {/* Icon ba chấm */}
                        <Button variant="ghost" size="icon" className="absolute top-2 right-2">
                            <MoreVertical size={20} />
                        </Button>
                    </Card>
                ))}
            </div>
        </div>
    );
}
