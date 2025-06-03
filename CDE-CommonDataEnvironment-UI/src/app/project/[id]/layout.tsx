"use client";
import projectApiRequest from "@/apis/project.api";
import { RoleContext } from "@/hooks/use-role";
import { useQuery } from "@tanstack/react-query";
import { ReactNode } from "react";



interface Props {
  children: ReactNode;
  params: { id: string };
}

export default function Layout({ children, params }: Props) {
  const { data, isLoading } = useQuery({
    queryKey: ["role"],
    queryFn: () => projectApiRequest.getRole(Number(params.id)),
  });

  if (isLoading) {
    return <p></p>; // Hoặc return null nếu không muốn hiển thị gì
  }
  console.log("Xin chào");
  return (
    <RoleContext.Provider value={{ roleDetail: data!.data }}>
      {children}
    </RoleContext.Provider>
  );
}
