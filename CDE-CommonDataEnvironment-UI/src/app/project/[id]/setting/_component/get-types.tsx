
"use client"
import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import typeApiRequest from '@/apis/type.api';
import { useQuery } from '@tanstack/react-query';
import { Button } from '@/components/custom/button';
import { Pencil, Trash } from 'lucide-react';
import FormCRUDType from './form-crud-type';
import { State } from '@/data/enums/state.enum';
import { Role } from '@/data/enums/role.enum';

export default function GetTypes({ projectId, role }: { projectId: number, role: Role }) {
    const { data, isLoading } = useQuery({
        queryKey: ['get-list-types'],
        queryFn: () => typeApiRequest.getList(projectId)
    })
    if (isLoading) return <></>
    return (
        <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
            <CardHeader>
                <CardTitle>Danh sách loại tài liệu</CardTitle>
            </CardHeader>
            <CardContent>
                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    {/* Phần mô tả */}
                    <div className="flex flex-col justify-center">
                        <h2 className="text-xl font-semibold">Mô tả</h2>
                        <p className="text-muted-foreground">
                            Đây là danh sách loại tài liệu để dùng cho việc gán lên những việc cần làm.
                        </p>
                    </div>

                    {/* Phần bảng */}
                    <div>
                        <div className="flex justify-between items-center mb-4">
                            <h3 className="text-lg font-semibold">Danh sách thể loại</h3>
                            <div className="flex justify-between items-center">
                                {role == Role.Admin && <FormCRUDType trigger={<Button variant="default">Thêm mới thể loại</Button>} state={State.CREATE} type={{
                                    projectId: projectId,
                                    name: '',
                                    iconImage: new File([], "default.png"),
                                }} />}
                            </div>
                        </div>
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead>#</TableHead>
                                    <TableHead>Ảnh</TableHead>
                                    <TableHead>Tên loại</TableHead>
                                    {role == Role.Admin && <TableHead>Hành động</TableHead>}
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {data!.data.map((item, index) => (
                                    <TableRow key={index}>
                                        <TableCell>{index + 1}</TableCell>
                                        <TableCell><img className='h-6' src={item.imageIconUrl} alt="" /></TableCell>
                                        <TableCell>{item.name}</TableCell>
                                        {role == Role.Admin && !item.isBlock ? <TableCell>
                                            <div className='flex items-center justify-start'>
                                                <FormCRUDType
                                                    trigger={
                                                        <Button variant="ghost" size="icon" className="hover:text-white  hover:bg-black">
                                                            <Pencil className="w-4 h-4" />
                                                        </Button>
                                                    }
                                                    state={State.UPDATE}
                                                    type={{
                                                        projectId: projectId,
                                                        iconImage: new File([], "default.png"),
                                                        imageIconUrl: item.imageIconUrl,
                                                        name: item.name,
                                                        id: item.id
                                                    }} />
                                                <FormCRUDType
                                                    trigger={
                                                        <Button variant="ghost" size="icon" className="hover:text-white  hover:bg-black">
                                                            <Trash className="w-4 h-4" />
                                                        </Button>
                                                    }
                                                    state={State.DELETE}
                                                    type={{
                                                        projectId: projectId,
                                                        name: "",
                                                        iconImage: new File([], "default.png"),
                                                        id: item.id
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
