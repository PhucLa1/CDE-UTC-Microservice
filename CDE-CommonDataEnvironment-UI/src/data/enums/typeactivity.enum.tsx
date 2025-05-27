import { ReactNode } from "react";
import {
  FileIcon,
  FolderIcon,
  UsersIcon,
  EyeIcon,
  ListTodoIcon,
  MessageCircleIcon,
  MoreHorizontalIcon,
  FolderKanbanIcon,
  ProjectorIcon,
} from "lucide-react"; // ví dụ dùng lucide-react
export enum TypeActivity {
  File = 1,
  Folder = 2,
  Team = 3,
  View = 4,
  Todo = 5,
  BCFTopic = 6,
  Comment = 7,
  Other = 8,
  Project = 9,
  Group = 10,
}

export const activityOptions = [
  {
    label: "Tệp",
    value: TypeActivity.File,
    icon: <FileIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Tác động liên quan đến tệp đính kèm hoặc tập tin.",
  },
  {
    label: "Thư mục",
    value: TypeActivity.Folder,
    icon: <FolderIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Tác động đến thư mục chứa tệp hoặc dữ liệu.",
  },
  {
    label: "Nhóm",
    value: TypeActivity.Team,
    icon: <UsersIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Hoạt động liên quan đến nhóm người dùng.",
  },
  {
    label: "Xem",
    value: TypeActivity.View,
    icon: <EyeIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Hành động chỉ xem thông tin hoặc nội dung.",
  },
  {
    label: "Công việc",
    value: TypeActivity.Todo,
    icon: <ListTodoIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Các tác vụ hoặc công việc được giao.",
  },
  {
    label: "Chủ đề BCF",
    value: TypeActivity.BCFTopic,
    icon: <FolderKanbanIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Chủ đề liên quan đến quy trình BCF.",
  },
  {
    label: "Bình luận",
    value: TypeActivity.Comment,
    icon: <MessageCircleIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Bình luận hoặc phản hồi từ người dùng.",
  },
  {
    label: "Khác",
    value: TypeActivity.Other,
    icon: <MoreHorizontalIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Loại hoạt động không xác định rõ.",
  },
  {
    label: "Dự án",
    value: TypeActivity.Project,
    icon: <ProjectorIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Tác động đến dự án hoặc toàn bộ tiến độ.",
  },
  {
    label: "Nhóm",
    value: TypeActivity.Group,
    icon: <UsersIcon className="w-4 h-4 mr-1 mt-1" />,
    description: "Hoạt động trong nhóm hoặc tổ chức cụ thể.",
  },
] as {
  label: string;
  value: TypeActivity;
  icon: ReactNode;
  description: string;
}[];

// export const timeOptions = [
//   {
//     label: "1 ngày trước",
//     startDate: startOfDay(subDays(new Date(), 1)),
//     endDate: endOfDay(subDays(new Date(), 1)),
//   },
//   {
//     label: "7 ngày trước",
//     startDate: startOfDay(subDays(new Date(), 7)),
//     endDate: endOfDay(new Date()), // đến hôm nay
//   },
//   {
//     label: "30 ngày trước",
//     startDate: startOfDay(subDays(new Date(), 30)),
//     endDate: endOfDay(new Date()),
//   },
//   {
//     label: "Tùy chọn ngày",
//     startDate: null,
//     endDate: null,
//   },
// ];
