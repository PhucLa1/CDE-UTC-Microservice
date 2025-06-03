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
import { Separator } from '@/components/ui/separator';
import UserInfoSheet from './form-update-delete';
import { ApiResponse } from '@/data/type/response.type';
import { UserGroup } from '@/data/schema/Project/usergroup.schema';
import { UserProject } from '@/data/schema/Project/userproject.schema';
import AddUserGroup from './add-user-group';
type FormProps = {
    projectId: number,
    role: Role,
    data?: ApiResponse<UserGroup[]>,
    dataDropdown?: ApiResponse<UserProject[]>,
    groupId: number,
    currentUserId: number
}
//Dù nay là ngày của anh, nhưng anh vẫn muốn gửi lời chúc tới người iuu của anhh. Anh biết 1 2 ngày qua emm ngghi
export default function TableGroupUsers({ projectId, role, data, dataDropdown, groupId, currentUserId }: FormProps) {
    const [selectedRole, setSelectedRole] = useState<Role[]>([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [filteredData, setFilteredData] = useState<UserGroup[]>([]);
    const [dataCanAdd, setDataCanAdd] = useState<UserGroup[]>([]);
    const togglePosition = (value: Role) => {
        setSelectedRole((prev) =>
            prev.includes(value)
                ? prev.filter((item) => item !== value)
                : [...prev, value]
        );
    };
    console.log(data?.data)
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

    useEffect(() => {
        if (data && dataDropdown) {
            const existingIds = new Set(data.data.map(item => item.id)); // Tạo Set từ id của data
            const filtered = dataDropdown.data.filter(item => !existingIds.has(item.id) && item.userProjectStatus == UserProjectStatus.Active); // Lọc ra những phần tử không có trong data
            setDataCanAdd(filtered);
        }
    }, [data, dataDropdown]);

    return (
        <Card className="w-3/4">
            <CardContent className="p-4">
                <div className="flex justify-between items-center">
                    <h2 className="text-lg font-semibold">Thành viên trong nhóm</h2>
                    <div className="flex items-center gap-4">
                        {role == Role.Admin && <AddUserGroup projectId={projectId} dataDropdown={dataCanAdd} groupId={groupId} />}
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
                            <UserInfoSheet key={index} groupId={groupId} currentUserId={currentUserId} isInGroup={true} projectId={projectId} currentRole={role} userProject={item} node={
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
