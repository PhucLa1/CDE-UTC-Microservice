"use client";
import projectApiRequest from "@/apis/project.api";
import { RoleDetail } from "@/data/schema/Project/role.schema";
import { useQuery } from "@tanstack/react-query";
import React, { ReactNode, createContext, useContext } from "react";

interface RoleContextType {
  roleDetail?: RoleDetail; // Có thể undefined nếu đang load
}

const RoleContext = createContext<RoleContextType | null>(null);

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
  return (
    <RoleContext.Provider value={{ roleDetail: data!.data }}>
      {children}
    </RoleContext.Provider>
  );
}

// Custom hook để dễ dàng sử dụng context
export const useRole = () => {
  const context = useContext(RoleContext);
  if (!context) {
    throw new Error("useRole must be used within RoleContext Provider");
  }
  return context;
};
