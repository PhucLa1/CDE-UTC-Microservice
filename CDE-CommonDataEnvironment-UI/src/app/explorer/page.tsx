import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb'
import { Button } from '@/components/custom/button';
import { FaTh, FaList, FaEllipsisV } from "react-icons/fa";
import { MdAdd } from "react-icons/md";
import React from 'react'
import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableFooter,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
const invoices = [
    {
        invoice: "INV001",
        paymentStatus: "Paid",
        totalAmount: "$250.00",
        paymentMethod: "Credit Card",
    },
    {
        invoice: "INV002",
        paymentStatus: "Pending",
        totalAmount: "$150.00",
        paymentMethod: "PayPal",
    },
    {
        invoice: "INV003",
        paymentStatus: "Unpaid",
        totalAmount: "$350.00",
        paymentMethod: "Bank Transfer",
    },
    {
        invoice: "INV004",
        paymentStatus: "Paid",
        totalAmount: "$450.00",
        paymentMethod: "Credit Card",
    },
    {
        invoice: "INV005",
        paymentStatus: "Paid",
        totalAmount: "$550.00",
        paymentMethod: "PayPal",
    },
    {
        invoice: "INV006",
        paymentStatus: "Pending",
        totalAmount: "$200.00",
        paymentMethod: "Bank Transfer",
    },
    {
        invoice: "INV007",
        paymentStatus: "Unpaid",
        totalAmount: "$300.00",
        paymentMethod: "Credit Card",
    },
]
export default function page() {
    let pathList: Array<PathItem> = [
        {
            name: "Tệp&thư mục",
            url: "/explorer"
        },
    ];
    return (
        <>
            <div className='mb-2 flex items-center justify-between space-y-2'>
                <div>
                    <h2 className='text-2xl font-bold tracking-tight'>Quản lí tệp&thư mục</h2>
                    <AppBreadcrumb pathList={pathList} className="mt-2" />
                </div>
                <div className="flex items-center space-x-4">
                    <Button className="p-2 rounded text-white hover:bg-white hover:text-gray-900">
                        <FaTh />
                    </Button>
                    <Button className="p-2 rounded text-white hover:bg-white hover:text-gray-900">
                        <FaList />
                    </Button>
                    <Button className="p-2 bg-blue-500 text-white hover:bg-blue-600">
                        <MdAdd />
                    </Button>
                </div>
            </div>
            <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                <Table>
                    <TableHeader>
                        <TableRow>
                            <TableHead></TableHead>
                            <TableHead>Tên</TableHead>
                            <TableHead>Ngày tạo</TableHead>
                            <TableHead>Ngày sửa</TableHead>
                            <TableHead>Dung lượng</TableHead>
                            <TableHead>Nhãn</TableHead>
                        </TableRow>
                    </TableHeader>
                    <TableBody>
                        {invoices.map((invoice) => (
                            <TableRow className="h-[70px]" key={invoice.invoice}>
                                <TableCell className="font-medium">{invoice.invoice}</TableCell>
                                <TableCell>{invoice.paymentStatus}</TableCell>
                                <TableCell>{invoice.paymentMethod}</TableCell>
                                <TableCell >{invoice.totalAmount}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </div>
        </>
    )
}
