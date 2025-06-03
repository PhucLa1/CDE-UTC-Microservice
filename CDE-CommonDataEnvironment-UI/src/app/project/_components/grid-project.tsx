import { Project } from "@/data/schema/Project/project.schema";
import Link from "next/link";
import React from "react";

export default function GridProject({
  filteredProjects,
}: {
  filteredProjects: Project[];
}) {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
      {filteredProjects.map((project, index) => (
        <Link key={index} href={`/project/${project.id}/unit`}>
          <div
            key={project.id}
            className="rounded-lg border bg-card overflow-hidden transition-all hover:shadow-lg"
          >
            <img
              src={project.imageUrl}
              alt=""
              className="w-full h-48 object-cover"
            />
            <div className="p-4 space-y-4">
              <h3 className="font-semibold">{project.name}</h3>
              <p className="text-sm text-muted-foreground">
                {project.description}
              </p>
              <div className="flex justify-between items-center">
                <span className="text-sm text-muted-foreground">
                  {project.startDate}
                </span>
                <span className="text-sm text-muted-foreground">
                  {project.endDate}
                </span>
              </div>
            </div>
          </div>
        </Link>
      ))}
    </div>
  );
}
