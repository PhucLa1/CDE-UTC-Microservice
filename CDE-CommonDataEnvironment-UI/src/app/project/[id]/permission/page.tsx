"use client"
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb'
import React, { useEffect, useState } from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card';
import { Button } from '@/components/custom/button';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { RadioGroup, RadioGroupItem } from '@/components/ui/radio-group';
import { Label } from '@/components/ui/label';
import permissionApiRequest from '@/apis/permission.api';
import { InvitationPermission } from '@/data/enums/invitationpermission.enum';
import { Role } from '@/data/enums/role.enum';
import { TodoVisibility } from '@/data/enums/todovisibility.enum';
import { Permission } from '@/data/schema/Project/permission.schema';
import { handleSuccessApi } from '@/lib/utils';
import { useRole } from '@/hooks/use-role';
const pathList: Array<PathItem> = [
    {
        name: "Quyền trong dự án",
        url: "#"
    },
];
export default function Page({ params }: { params: { id: string } }) {
    const { roleDetail } = useRole()
    const queryClient = useQueryClient()
    const [invitationPermission, setInvitationPermission] = useState<string>("");
    const [todoVisibility, setTodoVisibility] = useState<string>("");

    const { data, isLoading } = useQuery({
        queryKey: ['permission'],
        queryFn: () => permissionApiRequest.get(Number(params.id))
    })

    const { mutate, isPending } = useMutation({
        mutationKey: ['update'],
        mutationFn: (value: Permission) => permissionApiRequest.update(value),
        onSuccess: () => {
            handleSuccessApi({
                title: 'Quyền dự án được cập nhật thành công',
                message: 'Quyền dự án đã được cập nhật thành công'
            })
            queryClient.invalidateQueries({ queryKey: ['permission'] })
        }
    })
    useEffect(() => {
        if (data) {
            setInvitationPermission(data.data.invitationPermission.toString())
            setTodoVisibility(data.data.todoVisibility.toString())
        }
    }, [data])

    if (isLoading) return <></>
    return (
        <>
            <div className='mb-2 flex items-center justify-between space-y-2'>
                <div>
                    <h2 className='text-2xl font-bold tracking-tight'>Quyền trong dự án</h2>
                    <AppBreadcrumb pathList={pathList} className="mt-2" />
                </div>
                {roleDetail?.role == Role.Admin ? <div>
                    <Button onClick={() => mutate({
                        projectId: Number(params.id),
                        invitationPermission: Number(invitationPermission),
                        todoVisibility: Number(todoVisibility)
                    })} 
                    loading={isPending}>Lưu</Button>
                </div> : <></>}
            </div>
            <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                    <CardHeader>
                        <CardTitle>Quyền được mời thành viên khác</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <RadioGroup disabled={roleDetail?.role !== Role.Admin} value={invitationPermission}
                            onValueChange={(val) => setInvitationPermission(val)}>
                            <div className="flex items-center space-x-2">
                                <RadioGroupItem value={InvitationPermission.UserCanInvite.toString()} />
                                <Label htmlFor="option-one">Tất cả người dùng trong dự án đều có thể mời người khác</Label>
                            </div>
                            <div className="flex items-center space-x-2 mt-4">
                                <RadioGroupItem value={InvitationPermission.OnlyAdminCanInvite.toString()} />
                                <Label htmlFor="option-two">Chỉ có admin mới có quyền có thể mời người khác</Label>
                            </div>
                        </RadioGroup>
                    </CardContent>
                </Card>
                <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4 mt-12">
                    <CardHeader>
                        <CardTitle>Quyền tạo việc cần làm</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <RadioGroup disabled={roleDetail?.role !== Role.Admin} value={todoVisibility}
                            onValueChange={(val) => setTodoVisibility(val)}>
                            <div className="flex items-center space-x-2">
                                <RadioGroupItem value={TodoVisibility.Default.toString()} />
                                <Label htmlFor="option-one">Việc cần làm có thể tạo bởi tất cả mọi người</Label>
                            </div>
                            <div className="flex items-center space-x-2 mt-4">
                                <RadioGroupItem value={TodoVisibility.Restricted.toString()} />
                                <Label htmlFor="option-two">Việc cần làm chỉ có thể tạo bởi admin</Label>
                            </div>
                        </RadioGroup>
                    </CardContent>
                </Card>
            </div>
        </>
    )
}
