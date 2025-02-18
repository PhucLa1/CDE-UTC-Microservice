import React from 'react'
import { FileIcon, FolderIcon, ChevronUpIcon } from 'lucide-react';
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
import SheetFolder from './sheet-folder';
type FormProps = {
  data: Storage[]

}
export default function TableStorage({ data }: FormProps) {
  const [selectedStorage, setSelectedStorage] = useState<Set<number>>(new Set());
  const [selectAll, setSelectAll] = useState(false);
  const handleSelectAll = (checked: boolean) => {
    setSelectAll(checked);
    if (checked) {
      setSelectedStorage(new Set(data.map(data => data.id!)));
    } else {
      setSelectedStorage(new Set());
    }
  };

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
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((item, index) => (
            <SheetFolder node={<TableRow className='h-[60px]' key={index}>
              <TableCell>
                <Checkbox
                  checked={selectedStorage.has(item.id!)}
                  onCheckedChange={(checked) => handleSelectFile(item.id!, checked as boolean)}
                  aria-label={`Select ${item.id}`}
                />
              </TableCell>
              <TableCell className="font-medium">
                <div className="flex items-center gap-2">
                  {item.isFile === false ? (
                    <FolderIcon className="h-6 w-6 text-blue-500" />
                  ) : (
                    <FileIcon className="h-6 w-6 text-gray-500" />
                  )}
                  {item.name}
                </div>
              </TableCell>
              <TableCell>{item.createdAt}</TableCell>
              <TableCell>{item.nameCreatedBy}</TableCell>
              <TableCell>
                <div className="flex gap-1">
                  {item.tagNames?.map((tag, subIndex) => (
                    <Badge key={subIndex} variant="secondary">
                      {tag}
                    </Badge>
                  ))}
                </div>
              </TableCell>
            </TableRow>} />
          ))}
        </TableBody>
      </Table>
    </div>
  )
}
