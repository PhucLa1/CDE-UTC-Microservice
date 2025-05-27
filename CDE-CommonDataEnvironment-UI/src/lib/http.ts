/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import envConfig from "@/config";
import { Service } from "@/data/enums/service.enum";
import { assertApiResponse, handleErrorApi } from "@/lib/utils";

type CustomOptions = RequestInit & {
  baseUrl?: string | undefined;
};

const request = async <T>(
  method: "GET" | "POST" | "PUT" | "DELETE",
  url: string,
  options?: CustomOptions | undefined,
  service?: Service.AuthService | Service.EventService | Service.ProjectService | Service.AIService
) => {
  let body: FormData | string | undefined = undefined;
  if (options?.body instanceof FormData) {
    body = options.body;
  } else if (options?.body) {
    body = JSON.stringify(options.body);
  }
  const baseHeaders: { [key: string]: string } =
    body instanceof FormData ? {} : { "Content-Type": "application/json" };

  // Nếu không truyền baseUrl (hoặc baseUrl = undefined) thì lấy từ envConfig.NEXT_PUBLIC_API_ENDPOINT
  // Nếu truyền baseUrl thì lấy giá trị truyền vào, truyền vào '' thì đồng nghĩa với việc chúng ta gọi API đến Next.js Server
  let baseUrl = options?.baseUrl ?? envConfig.NEXT_PUBLIC_API_ENDPOINT;
  if (service != null) {
    baseUrl = baseUrl + service;
  }

  const fullUrl = url.startsWith("/")
    ? `${baseUrl}${url}`
    : `${baseUrl}/${url}`;

  const res = await fetch(fullUrl, {
    ...options,
    headers: {
      ...baseHeaders,
      ...options?.headers,
      "X-Project-Id": localStorage.getItem("projectId") || "",
    } as any,
    body,
    method,
    credentials: "include",
  });
  if (!res.ok) {
    let errorMessage = "Unknown error"; // Giá trị mặc định nếu không nhận được message từ API
    try {
      // Cố gắng đọc phản hồi từ API dưới dạng JSON
      const errorResponse = await res.json();
      errorMessage = errorResponse.detail || errorMessage;
    } catch (error) {
      // Nếu không thể parse JSON, sử dụng statusText làm fallback
      errorMessage = res.statusText || errorMessage;
    }

    // Gọi hàm xử lý lỗi (handleErrorApi) với thông tin lỗi
    handleErrorApi({
      error: errorMessage,
    });

    // Ném lỗi để dừng luồng xử lý
    throw new Error(
      `Request failed with status ${res.status}: ${errorMessage}`
    );
  }
  const payload: Response = await res.json();

  try {
    const resData = assertApiResponse<T>(payload);
    // console.log("resData",resData)
    return resData;
  } catch (error) {
    console.log();
    console.error(error);
    throw error;
  }
};

const http = {
  get<T>(
    url: string,
    options?: Omit<CustomOptions, "body"> | undefined,
    service?:
      | Service.AuthService
      | Service.EventService
      | Service.ProjectService
      | Service.AIService
  ) {
    return request<T>("GET", url, options, service);
  },
  post<T>(
    url: string,
    body: any,
    options?: Omit<CustomOptions, "body"> | undefined,
    service?:
      | Service.AuthService
      | Service.EventService
      | Service.ProjectService
      | Service.AIService
  ) {
    return request<T>("POST", url, { ...options, body }, service);
  },
  put<T>(
    url: string,
    body: any,
    options?: Omit<CustomOptions, "body"> | undefined,
    service?:
      | Service.AuthService
      | Service.EventService
      | Service.ProjectService
      | Service.AIService
  ) {
    return request<T>("PUT", url, { ...options, body }, service);
  },
  delete<T>(
    url: string,
    body: any,
    options?: Omit<CustomOptions, "body"> | undefined,
    service?:
      | Service.AuthService
      | Service.EventService
      | Service.ProjectService
      | Service.AIService
  ) {
    return request<T>("DELETE", url, { ...options, body }, service);
  },
};

export default http;
