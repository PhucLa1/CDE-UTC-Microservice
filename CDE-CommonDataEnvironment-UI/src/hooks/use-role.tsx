"use client";

import { RoleDetail } from "@/data/schema/Project/role.schema";
import { createContext, useContext } from "react";

interface RoleContextType {
  roleDetail?: RoleDetail;
}

export const RoleContext = createContext<RoleContextType | null>(null);

export const RoleProvider = RoleContext.Provider;

export const useRole = () => {
  const context = useContext(RoleContext);
  if (!context) {
    throw new Error("useRole must be used within RoleContext Provider");
  }
  return context;
};
