
"use client"
import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import { useQuery } from '@tanstack/react-query';
import { Button } from '@/components/custom/button';
import { Pencil, Trash } from 'lucide-react';
import { State } from '@/data/enums/state.enum';
import { Role } from '@/data/enums/role.enum';
import priorityApiRequest from '@/apis/priority.api';
import FormCRUDPriority from './form-crud-priority';

export default function GetPriorities({ projectId, role }: { projectId: number, role: Role }) {
    const { data, isLoading } = useQuery({
        queryKey: ['get-list-priorities'],
        queryFn: () => priorityApiRequest.getList(projectId)
    })
    if (isLoading) return <></>
    return (
        <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4 mt-8">
            <CardHeader>
                <CardTitle>Danh sách loại ưu tiên</CardTitle>
            </CardHeader>
            <CardContent>
                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    {/* Phần mô tả */}
                    <div className="flex flex-col justify-center">
                        <h2 className="text-xl font-semibold">Mô tả</h2>
                        <p className="text-muted-foreground">
                            Đây là danh sách loại ưu tiên để dùng cho việc gán lên những việc cần làm.
                        </p>
                    </div>

                    {/* Phần bảng */}
                    <div>
                        <div className="flex justify-between items-center mb-4">
                            <h3 className="text-lg font-semibold">Danh sách ưu tiên</h3>
                            <div className="flex justify-between items-center">
                                {role == Role.Admin ? <FormCRUDPriority trigger={<Button variant="default">Thêm mới ưu tiên</Button>} state={State.CREATE} priority={{
                                    projectId: projectId,
                                    name: '',
                                    colorRGB: '',
                                }} /> : <></>}
                            </div>
                        </div>
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead>#</TableHead>
                                    <TableHead>Hiển thị</TableHead>
                                    <TableHead>Tên</TableHead>
                                    {role == Role.Admin ?  <TableHead>Hành động</TableHead> : <></>}
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {data!.data.map((item, index) => (
                                    <TableRow key={index}>
                                        <TableCell>{index + 1}</TableCell>
                                        <TableCell>
                                            <div
                                                style={{
                                                    width: "24px",
                                                    height: "24px",
                                                    backgroundColor: item.colorRGB,
                                                    borderRadius: "4px",
                                                    border: "1px solid #ccc",
                                                }}
                                            />
                                        </TableCell>
                                        <TableCell>{item.name}</TableCell>
                                        {role == Role.Admin && !item.isBlock ? <TableCell>
                                            <div className='flex items-center justify-start'>
                                                <FormCRUDPriority
                                                    trigger={
                                                        <Button variant="ghost" size="icon" className="hover:text-white  hover:bg-black">
                                                            <Pencil className="w-4 h-4" />
                                                        </Button>
                                                    }
                                                    state={State.UPDATE}
                                                    priority={{
                                                        projectId: projectId,
                                                        colorRGB: item.colorRGB,
                                                        name: item.name,
                                                        id: item.id
                                                    }} />
                                                <FormCRUDPriority
                                                    trigger={
                                                        <Button variant="ghost" size="icon" className="hover:text-white  hover:bg-black">
                                                            <Trash className="w-4 h-4" />
                                                        </Button>
                                                    }
                                                    state={State.DELETE}
                                                    priority={{
                                                        projectId: projectId,
                                                        colorRGB: '',
                                                        id: item.id,
                                                        name: ''
                                                    }} />
                                            </div>
                                        </TableCell> : <></>}
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </div>
                </div>
            </CardContent>
        </Card>
    )
}
