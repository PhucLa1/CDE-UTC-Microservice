import storageApiRequest from '@/apis/storage.api';
import { Checkbox } from "@/components/ui/checkbox";
import { File } from '@/data/schema/Project/file.schema';
import { useQuery } from '@tanstack/react-query';
import { Folder } from "lucide-react";
import React, { useState } from 'react';
interface Props {
  projectId: number;
  selectedFiles: File[];
  setSelectedFiles: React.Dispatch<React.SetStateAction<File[]>>;
}
interface Folder {
  name: string;
  id: number
}
export default function StorageChoose({projectId, selectedFiles, setSelectedFiles} : Props) {
  const [parentId, setParentId] = useState<number>(0)
  const [folders, setFolders] = useState<Folder[]>([{
    name: "Gốc",
    id: 0
  }])
  // const [selectedFiles, setSelectedFiles] = useState<number[]>([]);
  const {data: dataStorage, isLoading: isLoadingStorage} = useQuery({
    queryKey: ['get-list-storage', parentId],
    queryFn: () => storageApiRequest.getList(projectId,parentId)
  })
  const toggleFile = (file: File) => {
    setSelectedFiles(prev =>
      prev.some(f => f.id === file.id)
        ? prev.filter(f => f.id !== file.id)
        : [...prev, file]
    );
  };
  return (
    <div>
       <p className="text-sm text-muted-foreground mt-2">
              Bạn có thể chọn 1 lúc nhiều file
            </p>
            <div className="flex items-center flex-wrap text-sm text-gray-600 space-x-1">
            {folders.map((item, index) => {
              const isLast = index === folders.length - 1;
              return (
                <div
                  key={index}
                  className="flex items-center"
                  onClick={() => {
                    if (!isLast) {
                      setParentId(item.id)
                      setFolders(folders.slice(0, index + 1));
                    };
                  }}
                >
                  <p
                    className={`${
                      isLast
                        ? 'text-gray-500 cursor-default'
                        : 'text-blue-600 hover:underline cursor-pointer'
                    }`}
                  >
                    {item.name}
                  </p>
                  {index < folders.length - 1 && (
                    <span className="mx-2 text-gray-400">{'>'}</span>
                  )}
                </div>
              );
            })}
            </div>
            <div className="mt-3 max-h-[300px] overflow-y-auto space-y-1">
              {!isLoadingStorage && dataStorage ? dataStorage.data.map((item, index) => {
                if(item.isFile){
                  return ( <div key={index} className="flex items-center gap-2 px-2 py-1 hover:bg-muted rounded">
                    <Checkbox
                      id={`file-${index}`}
                      checked={selectedFiles.some(f => f.id === item.id)}
                      onCheckedChange={() => toggleFile(item as File)}
                    />
                    <img width={25} src={item.urlImage} alt="" />
                    <label htmlFor={`file-${index}`} className="text-sm truncate cursor-pointer">
                      {item.name}
                    </label>
                  </div>)
                }else{
                  return (
                    <div onClick={() => {
                      setParentId(item.id!)
                      setFolders([...folders, {name: item.name, id: item.id!} as Folder])
                      }} key={index} className="flex items-center gap-2 px-2 py-1 hover:bg-muted rounded">
                      <Folder className="w-[25px] text-yellow-500" />
                    <span className="text-sm font-medium">{item.name}</span>
                </div>
                  )
                }
              }) : []}
            </div>
    </div>
  )
}
