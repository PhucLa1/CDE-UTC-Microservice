import { Project } from '@/data/schema/Project/project.schema'
import { useRouter } from 'next/navigation'
import React from 'react'

export default function TableProject({ filteredProjects }: { filteredProjects: Project[] }) {
    const router = useRouter()
    return (
        <div className="rounded-lg border bg-card">
            <div className="overflow-x-auto">
                <table className="w-full">
                    <thead className="border-b bg-muted/50">
                        <tr>
                            <th className="h-10 px-4 text-left align-middle font-medium text-muted-foreground">Dự án</th>
                            <th className="h-10 px-4 text-left align-middle font-medium text-muted-foreground">Mô tả</th>
                            <th className="h-10 px-4 text-left align-middle font-medium text-muted-foreground">Ngày bắt đầu</th>
                            <th className="h-10 px-4 text-left align-middle font-medium text-muted-foreground">Ngày kết thúc</th>
                        </tr>
                    </thead>
                    <tbody>
                        {filteredProjects.map((project) => (
                            <tr key={project.id} className="border-b transition-colors hover:bg-muted/50" onClick={() => {
                                router.push(`project/${project.id}/unit`);
                                localStorage.setItem('projectId', project.id!.toString());
                            }}>

                                <td className="p-4">
                                    <div className="flex items-center gap-3">
                                        <img className="h-10 w-10 rounded-lg object-cover" src={project.imageUrl} alt="" />
                                        <div className="font-medium">{project.name}</div>
                                    </div>
                                </td>
                                <td className="p-4 text-muted-foreground">
                                    {project.description}
                                </td>
                                <td className="p-4 text-muted-foreground">{project.startDate}</td>
                                <td className="p-4 text-muted-foreground">{project.endDate}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}
