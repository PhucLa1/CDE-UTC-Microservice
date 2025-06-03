import React, { useEffect, useState } from 'react'
import { Input } from '@/components/ui/input';
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
    DropdownMenuCheckboxItem,
} from "@/components/ui/dropdown-menu"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Card, CardContent } from "@/components/ui/card";
import { UserProjectStatus } from '@/data/enums/userprojectstatus.enum';
import { Button } from '@/components/custom/button';
import { Role } from '@/data/enums/role.enum';
import { UserProject } from '@/data/schema/Project/userproject.schema';
import { Separator } from '@/components/ui/separator';
import UserInfoSheet from './form-update-delete';
import { ApiResponse } from '@/data/type/response.type';
type FormProps = {
    projectId: number,
    role: Role,
    data?: ApiResponse<UserProject[]>,
    currentUserId: number
}
export default function TableAllUsers({ projectId, role, data, currentUserId }: FormProps) {
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
    return (
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
                            <UserInfoSheet key={index} groupId={0} currentUserId={currentUserId} isInGroup={false} projectId={projectId} currentRole={role} userProject={item} node={
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
                            } />
                        ))}
                    </TableBody>
                </Table>
            </CardContent>
        </Card>
    )
}
