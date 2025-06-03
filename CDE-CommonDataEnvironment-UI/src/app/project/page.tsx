"use client";
import projectApiRequest from "@/apis/project.api";
import { Button } from "@/components/custom/button";
import { Input } from "@/components/ui/input";
import { Project } from "@/data/schema/Project/project.schema";
import { useQuery } from "@tanstack/react-query";
import { LayoutGrid, List, Search } from "lucide-react";
import { useEffect, useState } from "react";
import GridProject from "./_components/grid-project";
import TableProject from "./_components/table-project";

import dynamic from 'next/dynamic';
 
// eslint-disable-next-line @typescript-eslint/no-unused-vars
const DynamicComponentWithNoSSR = dynamic(
  () => import('./_components/create-project'),
  { ssr: false }
)
export default function Page() {
  const [viewMode, setViewMode] = useState<"table" | "grid">("table");
  const [searchQuery, setSearchQuery] = useState("");
  const [filteredProjects, setFilteredProjects] = useState<Project[]>([]);
  const { data: projects, isLoading } = useQuery({
    queryKey: ["list-project"],
    queryFn: () => projectApiRequest.getList(),
  });

  useEffect(() => {
    if (projects) {
      const filteredProjects = projects.data.filter(
        (project) =>
          project.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
          project.description.toLowerCase().includes(searchQuery.toLowerCase())
      );
      const formatFilteredProjects = filteredProjects.map((project) => {
        return {
          ...project,
          startDate: project.startDate.slice(0, 10),
          endDate: project.endDate.slice(0, 10),
        };
      });
      setFilteredProjects(formatFilteredProjects);
      console.log(formatFilteredProjects);
    }
  }, [projects, searchQuery]);
  if (isLoading) return <></>;
  return (
    <div className="min-h-screen bg-background">
      <div className="container py-8">
        <div className="mb-8 space-y-6">
          <div className="flex justify-between items-center">
            <h1 className="text-3xl font-bold">Dự án</h1>
            <DynamicComponentWithNoSSR  />
          </div>

          <div className="flex justify-between items-center gap-4">
            <div className="relative flex-1 max-w-xs">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground h-4 w-4" />
              <Input
                type="text"
                placeholder="Tìm kiếm..."
                className="pl-9"
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
              />
            </div>

            <div className="flex gap-2">
              <Button
                variant="outline"
                size="icon"
                onClick={() => setViewMode("table")}
                className={viewMode === "table" ? "bg-accent" : ""}
              >
                <List className="h-4 w-4" />
              </Button>
              <Button
                variant="outline"
                size="icon"
                onClick={() => setViewMode("grid")}
                className={viewMode === "grid" ? "bg-accent" : ""}
              >
                <LayoutGrid className="h-4 w-4" />
              </Button>
            </div>
          </div>
        </div>

        {viewMode === "table" ? (
          <TableProject filteredProjects={filteredProjects} />
        ) : (
          <GridProject filteredProjects={filteredProjects} />
        )}
      </div>
    </div>
  );
}
