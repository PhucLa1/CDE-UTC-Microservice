"use client"
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb'
import React, { useEffect, useState } from 'react'
import { Card, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Separator } from "@/components/ui/separator";
import { Input } from '@/components/ui/input';
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
    DropdownMenuCheckboxItem,
} from "@/components/ui/dropdown-menu"
import { useRole } from '../layout';
import { InvitationPermission } from '@/data/enums/invitationpermission.enum';
import { Role } from '@/data/enums/role.enum';
import { useQuery } from '@tanstack/react-query';
import teamApiRequest from '@/apis/team.api';
import { UserProjectStatus } from '@/data/enums/userprojectstatus.enum';
import { UserProject } from '@/data/schema/Project/userproject.schema';
import InviteForm from './_components/invite-form';
const pathList: Array<PathItem> = [
    {
        name: "Đội ngũ trong dự án",
        url: "#"
    },
];
export default function page({ params }: { params: { id: string } }) {
    const [selectedRole, setSelectedRole] = useState<Role[]>([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [filteredData, setFilteredData] = useState<UserProject[]>([]);
    const togglePosition = (value: Role) => {
        setSelectedRole((prev) =>
            prev.includes(value)
                ? prev.filter((item) => item !== value)
                : [...prev, value]
        );
    };
    const { roleDetail } = useRole()
    const { data, isLoading } = useQuery({
        queryKey: ['users-project'],
        queryFn: () => teamApiRequest.getUsersByProjectId(Number(params.id))
    })
    useEffect(() => {
        if (data) {
            const filteredData = data.data.filter(item =>
                (item.fullName!.toLowerCase().includes(searchQuery.toLowerCase()) ||
                    item.email.toLowerCase().includes(searchQuery.toLowerCase())) &&
                (selectedRole.includes(item.role) || selectedRole.length == 0)
            );
            setFilteredData(filteredData)
            console.log(filteredData)
        }
    }, [data, searchQuery, selectedRole])
    if (isLoading) return <></>
    return (
        <>
            <div className='mb-2 flex items-center justify-between space-y-2'>
                <div>
                    <h2 className='text-2xl font-bold tracking-tight'>Thành viên trong dự án</h2>
                    <AppBreadcrumb pathList={pathList} className="mt-2" />
                </div>
                {roleDetail?.invitationPermission == InvitationPermission.OnlyAdminCanInvite && roleDetail.role == Role.Member ? <></> : <div className='mr-6'>
                    <InviteForm projectId={Number(params.id)} button={<Button >Mời thành viên khác vào dự án</Button>} />
                </div>}
            </div>
            <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                <div className="flex gap-2 p-6">
                    <Card className="w-1/4 p-4">
                        <h2 className="text-lg font-semibold">Thành viên dự án</h2>
                        <div className="mt-4 space-y-2">
                            <p className="font-medium">Tất cả thành viên dự án ({data?.data.length} người)</p>
                            <Separator className='mt-4' />
                            <div className='mt-4'>
                                <p className="text-gray-500 mt-4">Nhóm riêng</p>
                                <p className="font-medium mt-2">Hello (1 User)</p>
                            </div>
                        </div>
                    </Card>

                    <Card className="w-3/4">
                        <CardContent className="p-4">
                            <div className="flex justify-between items-center">
                                <h2 className="text-lg font-semibold">Tất cả thành viên</h2>
                                <div className="flex items-center gap-4">
                                    <DropdownMenu>
                                        <DropdownMenuTrigger asChild>
                                            <Button variant="outline">Vị trí</Button>
                                        </DropdownMenuTrigger>
                                        <DropdownMenuContent className="w-56">
                                            <DropdownMenuLabel>Vị trí</DropdownMenuLabel>
                                            <DropdownMenuSeparator />
                                            <DropdownMenuCheckboxItem
                                                checked={selectedRole.includes(Role.Admin)}
                                                onCheckedChange={() => togglePosition(Role.Admin)}
                                                onSelect={(e) => e.preventDefault()}
                                            >
                                                Quản trị viên
                                            </DropdownMenuCheckboxItem>
                                            <DropdownMenuCheckboxItem
                                                checked={selectedRole.includes(Role.Member)}
                                                onCheckedChange={() => togglePosition(Role.Member)}
                                                onSelect={(e) => e.preventDefault()}
                                            >
                                                Người dùng
                                            </DropdownMenuCheckboxItem>
                                        </DropdownMenuContent>
                                    </DropdownMenu>
                                    <Input value={searchQuery} onChange={(e) => setSearchQuery(e.target.value)} type="text" placeholder="Tìm kiếm..." />
                                </div>
                            </div>
                            <Separator className="my-4" />
                            <Table>
                                <TableHeader>
                                    <TableRow>
                                        <TableHead>Tên</TableHead>
                                        <TableHead>Vị trí</TableHead>
                                        <TableHead>Trạng thái</TableHead>
                                        <TableHead>Ngày tham gia</TableHead>
                                    </TableRow>
                                </TableHeader>
                                <TableBody>
                                    {filteredData.map((item, index) => (
                                        <TableRow key={index}>
                                            <TableCell className="flex items-center gap-3">
                                                <Avatar>
                                                    <AvatarImage src={item.imageUrl} alt="@shadcn" />
                                                    <AvatarFallback>{item.fullName!.charAt(0)}</AvatarFallback>
                                                </Avatar>
                                                <div>
                                                    <p className="font-medium">{item.fullName}</p>
                                                    <a href={`mailto:${item.email}`} className="text-blue-500">
                                                        {item.email}
                                                    </a>
                                                </div>
                                            </TableCell>
                                            <TableCell
                                                className={item.role === Role.Admin ? "text-red-500 font-semibold" : "text-gray-600"}
                                            >
                                                {item.role === Role.Admin ? "Quản trị viên" : "Người dùng"}
                                            </TableCell>
                                            {/* Thêm màu cho trạng thái */}
                                            <TableCell
                                                className={item.userProjectStatus === UserProjectStatus.Active
                                                    ? "text-green-500 font-semibold"
                                                    : "text-yellow-500 font-semibold"
                                                }
                                            >
                                                {item.userProjectStatus === UserProjectStatus.Active ? "Đã vào" : "Đang chờ"}
                                            </TableCell>
                                            <TableCell>{item.dateJoined}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </CardContent>
                    </Card>
                </div>
            </div>
        </>
    )
}
