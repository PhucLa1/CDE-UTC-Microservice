"use client"
import { Button } from '@/components/custom/button';
import { Input } from '@/components/ui/input';
import { LayoutGrid, List, Search, Plus } from 'lucide-react';
import React, { useEffect, useState } from 'react'
import CreateProject from './_components/create-project';
import TableProject from './_components/table-project';
import GridProject from './_components/grid-project';
import { useQuery } from '@tanstack/react-query';
import projectApiRequest from '@/apis/project.api';
import { Project } from '@/data/schema/Project/project.schema';




export default function page() {
    const [viewMode, setViewMode] = useState<'table' | 'grid'>('table');
    const [searchQuery, setSearchQuery] = useState('');
    const [filteredProjects, setFilteredProjects] = useState<Project[]>([]);
    const {data: projects, isLoading} = useQuery({
        queryKey: ['list-project'],
        queryFn: () => projectApiRequest.getList()
    })

    useEffect(() => {
        if(projects){
            const filteredProjects = projects.data.filter(project =>
                project.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
                project.description.toLowerCase().includes(searchQuery.toLowerCase())
            );
            setFilteredProjects(filteredProjects)
        }
    }, [projects])
    

    return (
        <div className="min-h-screen bg-background">
            <div className="container py-8">
                <div className="mb-8 space-y-6">
                    <div className="flex justify-between items-center">
                        <h1 className="text-3xl font-bold">Projects</h1>
                        <CreateProject/>
                    </div>

                    <div className="flex justify-between items-center gap-4">
                        <div className="relative flex-1 max-w-xs">
                            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
                            <Input
                                type="text"
                                placeholder="Search projects..."
                                className="pl-9"
                                value={searchQuery}
                                onChange={(e) => setSearchQuery(e.target.value)}
                            />
                        </div>

                        <div className="flex gap-2">
                            <Button
                                variant="outline"
                                size="icon"
                                onClick={() => setViewMode('table')}
                                className={viewMode === 'table' ? 'bg-accent' : ''}
                            >
                                <List className="h-4 w-4" />
                            </Button>
                            <Button
                                variant="outline"
                                size="icon"
                                onClick={() => setViewMode('grid')}
                                className={viewMode === 'grid' ? 'bg-accent' : ''}
                            >
                                <LayoutGrid className="h-4 w-4" />
                            </Button>
                        </div>
                    </div>
                </div>

                {viewMode === 'table' ? (
                    <TableProject filteredProjects={filteredProjects}/>
                ) : (
                    <GridProject filteredProjects={filteredProjects}/>
                )}
            </div>
        </div>
    );
}
