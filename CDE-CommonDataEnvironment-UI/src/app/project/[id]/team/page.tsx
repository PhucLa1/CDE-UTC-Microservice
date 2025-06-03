"use client"
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb'
import React, { useState } from 'react'
import { Card } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { InvitationPermission } from '@/data/enums/invitationpermission.enum';
import { Role } from '@/data/enums/role.enum';
import { useQuery } from '@tanstack/react-query';
import teamApiRequest from '@/apis/team.api';
import InviteForm from './_components/invite-form';
import groupApiRequest from '@/apis/group.api';
import FormCRUDGroup from './_components/form-crud-group';
import { State } from '@/data/enums/state.enum';
import { Edit, Trash } from 'lucide-react';
import TableAllUsers from './_components/table-all-users';
import userGroupApiRequest from '@/apis/usergroup.api';
import TableGroupUsers from './_components/table-group-user';
import { useRole } from '@/hooks/use-role';
const pathList: Array<PathItem> = [
    {
        name: "Đội ngũ trong dự án",
        url: "#"
    },
];
export default function Page({ params }: { params: { id: string } }) {
    const { roleDetail } = useRole()
    const [groupId, setGroupId] = useState<number>(0)
    const { data: dataGroups, isLoading: isLoadingGroups } = useQuery({
        queryKey: ['groups'],
        queryFn: () => groupApiRequest.getList(Number(params.id))
    })
    const { data, isLoading } = useQuery({
        queryKey: ['users-project'],
        queryFn: () => teamApiRequest.getUsersByProjectId(Number(params.id)),
        enabled: groupId === 0, // Chỉ gọi khi groupId = 0
    })
    const { data: dataUserGroups, isLoading: isLoadingUserGroups } = useQuery({
        queryKey: ['users-group', groupId],
        queryFn: () => userGroupApiRequest.getUsersByGroupId(groupId),
        enabled: groupId !== 0, // Chỉ gọi khi groupId = 0
    })
    if (isLoading || isLoadingGroups || isLoadingUserGroups) return <></>
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
                        <div className='flex items-center justify-between cursor-pointer'>
                            <h2 className="text-lg font-semibold">Thành viên dự án</h2>
                            {roleDetail?.role == Role.Admin ? <FormCRUDGroup trigger={<Button>Tạo nhóm</Button>} state={State.CREATE} group={{
                                projectId: Number(params.id),
                                name: ""
                            }} /> : <></>}
                        </div>
                        <div className="mt-4 space-y-2 cursor-pointer">
                            <div onClick={() => setGroupId(0)}
                            // className={`${groupId == 0 ? 'bg-gray-800 rounded-lg cursor-pointer' : 'cursor-pointer'}`}
                            >
                                <p className="font-semibold">Thành viên dự án ({data?.data.length} người)</p>
                            </div>
                            <Separator className='mt-4' />
                            <div className='mt-4'>
                                <p className="text-gray-500 mt-4">Nhóm riêng</p>
                                {
                                    dataGroups?.data.map((item, index) => {
                                        return <div key={index} className='flex items-center justify-between cursor-pointer'>
                                            <div onClick={() => setGroupId(item.id!)}
                                            // className={`${groupId == item.id! ? 'bg-gray-600 rounded-lg cursor-pointer' : 'cursor-pointer'}`}
                                            >
                                                <p className="font-medium mt-2">{item.name} ({item.userCount} thành viên)</p>
                                            </div>
                                            {roleDetail?.role == Role.Admin ? <div className='flex items-center justify-end mt-2'>
                                                <FormCRUDGroup trigger={<Edit className="w-4 h-4 mr-2" />} state={State.UPDATE} group={{
                                                    projectId: Number(params.id),
                                                    name: item.name,
                                                    id: item.id
                                                }} />
                                                <FormCRUDGroup trigger={<Trash className="w-4 h-4 mr-2" />} state={State.DELETE} group={{
                                                    projectId: Number(params.id),
                                                    name: item.name,
                                                    id: item.id
                                                }} />
                                            </div> : <></>}
                                        </div>
                                    })
                                }
                            </div>
                        </div>
                    </Card>

                    {groupId == 0 && <TableAllUsers currentUserId={roleDetail!.id} projectId={Number(params.id)} role={roleDetail!.role} data={data} />}
                    {groupId !== 0 && <TableGroupUsers currentUserId={roleDetail!.id} groupId={groupId} projectId={Number(params.id)} role={roleDetail!.role} data={dataUserGroups} dataDropdown={data} />}
                </div>
            </div>
        </>
    )
}
