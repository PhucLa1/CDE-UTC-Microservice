
import {
  IconFolder,
  IconEye,
  IconActivity,
  IconListCheck,
  IconUsers,
  IconSettings,
  IconInfoCircle,
  IconBell,
  IconRuler,
  IconTag,
  IconShieldCheck,
  IconRobot,
} from '@tabler/icons-react';

export interface NavLink {
  title: string
  label?: string
  href: string
  icon: JSX.Element
}

export interface SideLink extends NavLink {
  sub?: NavLink[]
}

export const sidelinks: SideLink[] = [
  {
    title: 'Dữ liệu',
    label: '',
    href: '#',
    icon: <IconFolder size={18} />, // Icon thư mục
    sub: [
      {
        title: 'Tệp & Thư mục',
        label: '',
        href: '/storage/0',
        icon: <IconFolder size={18} />, // Icon thư mục
      },
      {
        title: 'Chế độ xem',
        label: '',
        href: '/views',
        icon: <IconEye size={18} />, // Icon con mắt
      },
    ],
  },
   {
    title: 'Trò chuyện với AI',
    label: '',
    href: '/chatbot',
    icon: <IconRobot size={18} />, // Icon hoạt động
  },
  {
    title: 'Hoạt động',
    label: '',
    href: '/activity',
    icon: <IconActivity size={18} />, // Icon hoạt động
  },
  {
    title: 'Việc cần làm',
    label: '',
    href: '/todo',
    icon: <IconListCheck size={18} />, // Icon danh sách kiểm tra
  },
  {
    title: 'Đội nhóm',
    label: '',
    href: '/team',
    icon: <IconUsers size={18} />, // Icon người dùng/đội nhóm
  },
  {
    title: 'Cài đặt',
    label: '',
    href: '#',
    icon: <IconSettings size={18} />, // Icon cài đặt
    sub: [
      {
        title: 'Chi tiết dự án',
        label: '',
        href: '/detail',
        icon: <IconInfoCircle size={18} />, // Icon thông tin chi tiết
      },
      {
        title: 'Cấu hình dự án',
        label: '',
        href: '/setting',
        icon: <IconSettings size={18} />, // Icon cài đặt
      },
      {
        title: 'Thông báo',
        label: '',
        href: '/notifications',
        icon: <IconBell size={18} />, // Icon chuông thông báo
      },
      {
        title: 'Đơn vị đo',
        label: '',
        href: '/unit',
        icon: <IconRuler size={18} />, // Icon thước đo
      },
      {
        title: 'Nhãn',
        label: '',
        href: '/tag',
        icon: <IconTag size={18} />, // Icon nhãn
      },
      {
        title: 'Quyền',
        label: '',
        href: '/permission',
        icon: <IconShieldCheck size={18} />, // Icon khiên bảo vệ
      },
    ],
  },
];
