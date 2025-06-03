"use client"
import tagApiRequest from '@/apis/tag.api';
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb';
import { Button } from '@/components/custom/button';
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import { useQuery } from '@tanstack/react-query';
import React, { useEffect, useState } from 'react'
import FormCRUD from './_components/formCRUD';
import { State } from '@/data/enums/state.enum';
import { Pencil, Trash } from 'lucide-react';
import { Role } from '@/data/enums/role.enum';
import { useRole } from '@/hooks/use-role';
const pathList: Array<PathItem> = [
    {
        name: "Dãn nhán",
        url: "#"
    },
];
export default function Page({ params }: { params: { id: string } }) {
    const [ids, setIds] = useState<number[]>()
    const { roleDetail } = useRole();
    const { data, isLoading } = useQuery({
        queryKey: ['get-list-tags'],
        queryFn: () => tagApiRequest.getList(Number(params.id))
    })
    useEffect(() => {
        if (data) {
            setIds(data.data.map((item) => item.id!))
        }
    }, [data])
    if (isLoading) return <></>
    return (
        <>
            <div className='mb-2 flex items-center justify-between space-y-2'>
                <div>
                    <h2 className='text-2xl font-bold tracking-tight'>Dãn nhán</h2>
                    <AppBreadcrumb pathList={pathList} className="mt-2" />
                </div>
            </div>
            <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                    <CardHeader>
                        <CardTitle>Danh sách dãn nhán</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                            {/* Phần mô tả */}
                            <div className="flex flex-col justify-center">
                                <h2 className="text-xl font-semibold">Mô tả</h2>
                                <p className="text-muted-foreground">
                                    Đây là danh sách dãn nhán để dùng cho việc gán nhãn lên những tài liệu, những việc cần làm.
                                </p>
                            </div>

                            {/* Phần bảng */}
                            <div>
                                <div className="flex justify-between items-center mb-4">
                                    <h3 className="text-lg font-semibold">Danh sách nhãn</h3>
                                    <div className="flex justify-between items-center">
                                        <FormCRUD trigger={<Button variant="default">Thêm mới nhãn</Button>} state={State.CREATE} tag={{
                                            projectId: Number(params.id),
                                            name: ''
                                        }} />
                                        {roleDetail!.role === Role.Admin && <FormCRUD trigger={<Button className='ml-1' variant="destructive">Xóa tất cả</Button>} state={State.DELETE} tag={{
                                            projectId: Number(params.id),
                                            name: ''
                                        }} ids={ids} />}
                                    </div>
                                </div>
                                <Table>
                                    <TableHeader>
                                        <TableRow>
                                            <TableHead>#</TableHead>
                                            <TableHead>Tên nhãn</TableHead>
                                            {roleDetail!.role === Role.Admin && <TableHead>Hành động</TableHead>}
                                        </TableRow>
                                    </TableHeader>
                                    <TableBody>
                                        {data!.data.map((item, index) => (
                                            <TableRow key={index}>
                                                <TableCell>{index + 1}</TableCell>
                                                <TableCell>{item.name}</TableCell>
                                                {roleDetail!.role === Role.Admin && <TableCell>
                                                    <div className='flex items-center justify-start'>
                                                        <FormCRUD
                                                            trigger={
                                                                <Button disabled={item.isBlock} variant="ghost" size="icon" className="hover:text-white  hover:bg-black">
                                                                    <Pencil className="w-4 h-4" />
                                                                </Button>
                                                            }
                                                            state={State.UPDATE}
                                                            tag={{
                                                                projectId: Number(params.id),
                                                                name: item.name,
                                                                id: item.id
                                                            }} />
                                                        <FormCRUD
                                                            trigger={
                                                                <Button variant="ghost" size="icon" className="hover:text-white  hover:bg-black">
                                                                    <Trash className="w-4 h-4" />
                                                                </Button>
                                                            }
                                                            state={State.DELETE}
                                                            tag={{
                                                                projectId: Number(params.id),
                                                                name: item.name,
                                                                id: item.id
                                                            }} />
                                                    </div>
                                                </TableCell>}
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </div>
                        </div>
                    </CardContent>
                </Card>
            </div>
        </>
    )
}
