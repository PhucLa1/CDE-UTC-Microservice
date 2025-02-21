"use client"
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb'
import { Button } from '@/components/custom/button';
import { Input } from '@/components/ui/input';
import { LayoutGrid, List, Search } from 'lucide-react';
import React, { useState } from 'react'
import CreateFolder from './_components/create-folder';
import { FaFolder, FaUpload } from 'react-icons/fa';
import UploadFile from './_components/upload-file';
import { useQuery } from '@tanstack/react-query';
import storageApiRequest from '@/apis/storage.api';
import TableStorage from './_components/table-storage';
const pathList: Array<PathItem> = [
    {
        name: "Tệp & Thư mục",
        url: "#"
    },
];
export default function page({ params }: { params: { id: string } }) {
    const [viewMode, setViewMode] = useState<'table' | 'grid'>('table');
    const [searchQuery, setSearchQuery] = useState('');
    const [isOpen, setIsOpen] = useState(false);
    const [parentId, setParentId] = useState<number>(0)

    const { data, isLoading } = useQuery({
        queryKey: ['storage'],
        queryFn: () => storageApiRequest.getList(Number(params.id), parentId),
    })
    if (isLoading) return <></>
    return (
        <>
            <div className='mb-2 flex items-center justify-between space-y-2'>
                <div>
                    <h2 className='text-2xl font-bold tracking-tight'>Lưu trữ</h2>
                    <AppBreadcrumb pathList={pathList} className="mt-2" />
                </div>
                <div className='mr-1'>
                    <div className="bg-gray-100 flex items-start justify-center">
                        <div className="relative">
                            <Button onClick={() => setIsOpen(!isOpen)}>Thêm mới</Button>
                            <div className={`${isOpen ? '' : 'hidden'} absolute right-0 mt-2 w-64 bg-white rounded-lg shadow-lg py-2 z-10`}>
                                <CreateFolder
                                    node={<button
                                        className="w-full px-4 py-2 hover:bg-gray-100 flex items-center gap-3"
                                        onClick={() => setIsOpen(false)}
                                    >
                                        <span className="text-gray-600 text-[14px]"><FaFolder className="w-5 h-5" /></span>
                                        <span className="text-gray-700 text-[14px]">Thêm thư mục</span>
                                    </button>}
                                    parentId={parentId}
                                    projectId={Number(params.id)}
                                />
                                <UploadFile
                                    folderId={parentId}
                                    projectId={Number(params.id)} node={<button
                                        className="w-full px-4 py-2 hover:bg-gray-100 flex items-center gap-3"
                                        onClick={() => setIsOpen(false)}
                                    >
                                        <span className="text-gray-600 text-[14px]"><FaUpload className="w-5 h-5" /></span>
                                        <span className="text-gray-700 text-[14px]">Thêm tệp</span>
                                    </button>} />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="flex justify-between items-center gap-4">
                <div className="relative flex-1 max-w-xs">
                    <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
                    <Input
                        type="text"
                        placeholder="Tìm kiếm..."
                        className="pl-9"
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                    />
                </div>

                <div className="flex gap-2">
                    <Button
                        variant="outline"
                        size="icon"
                        onClick={() => setViewMode('table')}
                        className={viewMode === 'table' ? 'bg-accent' : ''}
                    >
                        <List className="h-4 w-4" />
                    </Button>
                    <Button
                        variant="outline"
                        size="icon"
                        onClick={() => setViewMode('grid')}
                        className={viewMode === 'grid' ? 'bg-accent' : ''}
                    >
                        <LayoutGrid className="h-4 w-4" />
                    </Button>
                </div>
            </div>
            <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                {viewMode === 'table' && (//+
                    <TableStorage
                        projectId={Number(params.id)}
                        data={data!.data.sort((a, b) => {
                            const nameComparison = b.createdAt!.localeCompare(a.createdAt!);
                            return nameComparison !== 0 ? nameComparison : b.id! - a.id!;
                        })}
                    />
                )}
            </div>
        </>
    )
}

