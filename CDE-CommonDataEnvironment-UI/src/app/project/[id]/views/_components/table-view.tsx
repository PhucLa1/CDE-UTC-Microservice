import { Badge } from "@/components/ui/badge";
import { Checkbox } from "@/components/ui/checkbox";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { View } from "@/data/schema/Project/view.schema";
import { ChevronUpIcon, MoreVerticalIcon } from "lucide-react";
import { useRouter } from "next/navigation";
import { useState } from "react";
import SheetView from "./sheet-view";
type FormProps = {
  data: View[];
  projectId: number;
};
export default function TableView({ data, projectId }: FormProps) {
  const router = useRouter();
  console.log(router);
  const [selectedView, setSelectedView] = useState<Set<number>>(new Set());
  const [selectAll, setSelectAll] = useState(false);
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [id, setId] = useState<number>(0);
  const handleSelectAll = (checked: boolean) => {
    setSelectAll(checked);
    if (checked) {
      setSelectedView(new Set(data.map((data) => data.id!)));
    } else {
      setSelectedView(new Set());
    }
  };

  const handleSelectFile = (id: number, checked: boolean) => {
    const newSelected = new Set(selectedView);
    if (checked) {
      newSelected.add(id);
    } else {
      newSelected.delete(id);
    }
    setSelectedView(newSelected);
    setSelectAll(newSelected.size === data.length);
  };
  return (
    <>
     {id != 0 && <SheetView
                node={<></>}
                projectId={projectId}
                id={id}
                isOpen={isOpen}
                setIsOpen={setIsOpen} />}
      <div>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="w-[50px]">
                <Checkbox
                  checked={selectAll}
                  onCheckedChange={handleSelectAll}
                  aria-label="Chọn tất cả"
                />
              </TableHead>
              <TableHead className="w-[400px]">
                Tên <ChevronUpIcon className="inline-block ml-2 h-4 w-4" />
              </TableHead>
              <TableHead>Được tạo vào</TableHead>
              <TableHead>Được tạo bởi</TableHead>
              <TableHead>Nhãn dán</TableHead>
              <TableHead></TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {data.map((item, index) => (
              <TableRow className="h-[60px]" key={index}>
                <TableCell>
                  <Checkbox
                    checked={selectedView.has(item.id!)}
                    onCheckedChange={(checked) =>
                      handleSelectFile(item.id!, checked as boolean)
                    }
                    aria-label={`Select ${item.id}`}
                  />
                </TableCell>
                <TableCell className="font-medium cursor-pointer">
                  <div className="flex items-center gap-2">
                    <img src="/images/note.png" width={25} />
                    {item.name}
                  </div>
                </TableCell>
                <TableCell>{item.createdAt}</TableCell>
                <TableCell>{item.nameCreatedBy}</TableCell>
                <TableCell>
                  <div className="flex gap-1">
                    {item.tagNames?.map((tag, subIndex) => (
                      <Badge
                        className="bg-gray-300"
                        key={subIndex}
                        variant="secondary"
                      >
                        {tag}
                      </Badge>
                    ))}
                  </div>
                </TableCell>
                <TableCell onClick={() => {
                            setId(item.id!)
                            setIsOpen(true)
                        }}>
                  <MoreVerticalIcon className="h-5 w-5 text-gray-800 cursor-pointer hover:text-gray-200" />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </>
  );
}
