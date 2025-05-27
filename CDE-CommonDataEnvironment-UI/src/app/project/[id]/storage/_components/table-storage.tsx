import React from 'react'
import { FolderIcon, ChevronUpIcon, MoreVerticalIcon } from 'lucide-react';
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Checkbox } from "@/components/ui/checkbox";
import { Badge } from "@/components/ui/badge";
import { useState } from 'react';
import { Storage } from '@/data/schema/Project/storage.schema';
import SheetStorage from './sheet-storage';
import { useRouter } from 'next/navigation';
type FormProps = {
  data: Storage[],
  projectId: number
}
export default function TableStorage({ data, projectId }: FormProps) {
  const router = useRouter();
  const [selectedStorage, setSelectedStorage] = useState<Set<number>>(new Set());
  const [selectAll, setSelectAll] = useState(false);
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const [id, setId] = useState<number>(0)
  const [isFile, setIsFile] = useState<number>(0);  //1: File, 2 : Folder
  const handleSelectAll = (checked: boolean) => {
    setSelectAll(checked);
    if (checked) {
      setSelectedStorage(new Set(data.map(data => data.id!)));
    } else {
      setSelectedStorage(new Set());
    }
  };
  console.log(selectedStorage)

  const handleSelectFile = (id: number, checked: boolean) => {
    const newSelected = new Set(selectedStorage);
    if (checked) {
      newSelected.add(id);
    } else {
      newSelected.delete(id);
    }
    setSelectedStorage(newSelected);
    setSelectAll(newSelected.size === data.length);
  };
  return (
    <>
      {id != 0 && isFile != 0 && <SheetStorage
        isFile={isFile == 1 ? true : false}
        projectId={projectId} id={id}
        node={<></>}
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
              <TableRow
                className='h-[60px]' key={index}>
                <TableCell>
                  <Checkbox
                    checked={selectedStorage.has(item.id!)}
                    onCheckedChange={(checked) => handleSelectFile(item.id!, checked as boolean)}
                    aria-label={`Select ${item.id}`}
                  />
                </TableCell>
                <TableCell onClick={() => {
                  if (!item.isFile) {
                    router.push(`${item.id}`);
                  }
                }} className="font-medium cursor-pointer">
                  <div className="flex items-center gap-2">
                    {item.isFile === false ? (
                      <FolderIcon className="h-6 w-6 text-blue-500" />
                    ) : (
                      <img className="h-6 w-6" src={item.urlImage} />
                    )}
                    {item.name}
                  </div>
                </TableCell>
                <TableCell>{item.createdAt}</TableCell>
                <TableCell>{item.nameCreatedBy}</TableCell>
                <TableCell>
                  <div className="flex gap-1">
                    {item.tagNames?.map((tag, subIndex) => (
                      <Badge className='bg-gray-300' key={subIndex} variant="secondary">
                        {tag}
                      </Badge>
                    ))}
                  </div>
                </TableCell>
                <TableCell><MoreVerticalIcon onClick={() => {
                  setIsFile(item.isFile == true ? 1 : 2)
                  setIsOpen(true);
                  setId(item.id!);
                }} className='h-5 w-5 text-gray-800 cursor-pointer hover:text-gray-200' /></TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </>
  )
}
