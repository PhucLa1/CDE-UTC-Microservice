"use client"
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb'
import React from 'react'
import GetTypes from './_component/get-types';
import { useRole } from '../layout';
import GetStatuses from './_component/get-statuses';
import GetPriorities from './_component/get-priorities';
const pathList: Array<PathItem> = [
    {
        name: "Cấu hình dự án",
        url: "#"
    },
];
export default function page({ params }: { params: { id: string } }) {
    const { role } = useRole()
    return (
        <>
            <div className='mb-2 flex items-center justify-between space-y-2'>
                <div>
                    <h2 className='text-2xl font-bold tracking-tight'>Cấu hình dự án</h2>
                    <AppBreadcrumb pathList={pathList} className="mt-2" />
                </div>
            </div>
            <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                <GetTypes projectId={Number(params.id)} role={role!} />
                <GetStatuses projectId={Number(params.id)} role={role!} />
                <GetPriorities projectId={Number(params.id)} role={role!} />
            </div>
        </>
    )
}
