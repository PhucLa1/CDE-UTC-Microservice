/* eslint-disable @typescript-eslint/no-explicit-any */

import { toaster } from "@/components/custom/_toast"
import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const handleErrorApi = ({ error, duration }: {
  error: any,
  duration?: number
}) => {
  toaster.error({
    title: 'Đã có lỗi xảy ra',
    message: error ?? "Undefined Error",
  }, {
    position: "bottom-right",
    autoClose: duration ?? 2000
  })
  throw new Error('API fetching error', error);
}
export const handleSuccessApi = ({ title, message, duration }: {
  title?: string,
  message?: string,
  duration?: number
}) => {
  toaster.success({
    title: title ?? "Process Completed",
    message: message ?? "Process Completed"
  }, {
    position: "bottom-right",
    autoClose: duration ?? 2000
  })
}
export function assertApiResponse<T>(data: any): T {
  // //console.log("http data:", data)
  // if (typeof data.isSuccess !== 'boolean' || 
  //     (Array.isArray(data.message) === false && data.message !== null)) {
  //       throw new Error('Invalid API response structure');
  // }
  // else if(data.isSuccess == false){
  //     handleErrorApi({error:data.message});
  //     throw new Error('Some errors appear on server');
  //   }
  return data as T;
}

