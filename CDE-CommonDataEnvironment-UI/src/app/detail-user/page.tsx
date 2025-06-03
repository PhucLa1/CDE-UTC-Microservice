"use client"
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb';
import React, { useEffect, useState } from 'react'
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import { Input } from "@/components/ui/input"
import Link from 'next/link';
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { useForm } from 'react-hook-form';
import { Info, infoDefault, infoSchema } from '@/data/schema/Auth/info.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import authApiRequest from '@/apis/auth.api';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { DateDisplay } from '@/data/enums/datedisplay.enum';
import { TimeDisplay } from '@/data/enums/timedisplay.enum';
import jobTitleApiRequest from '@/apis/jobTitle.api';
import provinceApiRequest from '@/apis/province.api';
import { handleSuccessApi } from '@/lib/utils';
import { Button } from '@/components/custom/button';
const pathList: Array<PathItem> = [
    {
        name: "Thông tin cá nhân",
        url: "/detail-user"
    },
];
export default function Page() {
    const queryClient = useQueryClient()
    const [districtId, setDistrictId] = useState<number>(0)
    const [cityId, setCityId] = useState<number>(0)
    const [avatarUrl, setAvatarUrl] = useState("https://github.com/shadcn.png");
    const [selectedFile, setSelectedFile] = useState<File | null>(null);
    const form = useForm<Info>({
        resolver: zodResolver(infoSchema),
        defaultValues: infoDefault,
    })
    const onSubmit = (values: Info) => {
        const formData = new FormData();

        if (values.firstName != null) formData.append("firstName", values.firstName);
        if (values.lastName != null) formData.append("lastName", values.lastName);
        if (values.mobilePhoneNumber != null) formData.append("mobilePhoneNumber", values.mobilePhoneNumber);
        if (values.workPhoneNumber != null) formData.append("workPhoneNumber", values.workPhoneNumber);
        if (values.employer != null) formData.append("employer", values.employer);
        if (values.dateDisplay != null) formData.append("dateDisplay", values.dateDisplay.toString());
        if (values.timeDisplay != null) formData.append("timeDisplay", values.timeDisplay.toString());
        if (values.jobTitleId != null) formData.append("jobTitleId", values.jobTitleId.toString());
        if (values.cityId != null) formData.append("cityId", values.cityId.toString());
        if (values.districtId != null) formData.append("districtId", values.districtId.toString());
        if (values.wardId != null) formData.append("wardId", values.wardId.toString());
        if (selectedFile != null) formData.append("image", selectedFile);

        mutate(formData)

    }

    const { mutate, isPending } = useMutation({
        mutationKey: ['change-info'],
        mutationFn: (value: FormData) => authApiRequest.changeInfoUser(value),
        onSuccess: () => {
            handleSuccessApi
                ({
                    title: "Sửa đổi thông tin thành công",
                    message: "Bạn đã sửa đổi thông tin cá nhân thành công"
                })
            queryClient.invalidateQueries({ queryKey: ["infoUser"] })
            setSelectedFile(null)
        }
    })

    //Gọi API
    const { data: infoUser, isLoading: isLoadingInfoUser } = useQuery({
        queryKey: ['infoUser'],
        queryFn: () => authApiRequest.getInfoUser()
    })
    const { data: jobTitles, isLoading: isLoadingJobTitle } = useQuery({
        queryKey: ['jobTitle'],
        queryFn: () => jobTitleApiRequest.getAllJobTitle()
    })
    const { data: cities, isLoading: isLoadingProvinces } = useQuery({
        queryKey: ['provinces'],
        queryFn: () => provinceApiRequest.getAllProvince("")
    })
    const { data: districts, isLoading: isLoadingDistricts } = useQuery({
        queryKey: ['districts', cityId],
        queryFn: () => provinceApiRequest.getAllProvince("?CityId=" + cityId)
    })
    const { data: wards, isLoading: isLoadingWards } = useQuery({
        queryKey: ['wards', districtId],
        queryFn: () => provinceApiRequest.getAllProvince("?DistrictId=" + districtId)
    })


    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = () => {
                setAvatarUrl(reader.result as string);
            };
            reader.readAsDataURL(file);
            setSelectedFile(file); // Lưu lại file đã chọn
        }
    };
    useEffect(() => {
        if (infoUser != null) {
            if (infoUser.data.cityId != null) {
                setCityId(infoUser.data.cityId)
            }
            if (infoUser.data.districtId != null) {
                setDistrictId(infoUser.data.districtId)
            }
            if (infoUser.data.imageUrl != "" && infoUser.data.imageUrl != null) {
                setAvatarUrl(infoUser.data.imageUrl)
            }

            form.setValue("firstName", infoUser.data.firstName)
            form.setValue("lastName", infoUser.data.lastName)
            form.setValue("email", infoUser.data.email)
            form.setValue("mobilePhoneNumber", infoUser.data.mobilePhoneNumber || "")
            form.setValue("workPhoneNumber", infoUser.data.workPhoneNumber || "")
            form.setValue("employer", infoUser.data.employer)
            form.setValue("dateDisplay", infoUser.data.dateDisplay)
            form.setValue("timeDisplay", infoUser.data.timeDisplay)
            form.setValue("jobTitleId", infoUser.data.jobTitleId)
            form.setValue("cityId", infoUser.data.cityId)
            form.setValue("districtId", infoUser.data.districtId)
            form.setValue("wardId", infoUser.data.wardId)
        }
    }, [infoUser])

    if (isLoadingInfoUser || isLoadingJobTitle || isLoadingProvinces || isLoadingDistricts || isLoadingWards) return <></>
    return (
        <>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
                    <div className='mb-2 flex items-center justify-between space-y-2'>
                        <div>
                            <h2 className='text-2xl font-bold tracking-tight'>Trang cá nhân</h2>
                            <AppBreadcrumb pathList={pathList} className="mt-2" />
                        </div>
                        <div>
                            <Button loading={isPending}>Lưu</Button>
                        </div>
                    </div>
                    <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
                        <div className='grid gap-4 md:grid-cols-2 lg:grid-cols-8'>
                            <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                                <CardHeader>
                                    <CardTitle>Thông tin cơ bản</CardTitle>
                                </CardHeader>
                                <CardContent>
                                    <form>
                                        <div className="grid w-full items-center gap-4">
                                            <div className='flex justify-center'>
                                                <Avatar onClick={() => document.getElementById("avatarInput")?.click()} className='w-[150px] h-[150px] hover:bg-gray-800 cursor-pointer'>
                                                    <AvatarImage src={avatarUrl} alt="@shadcn" />
                                                    <AvatarFallback>CN</AvatarFallback>
                                                </Avatar>
                                                <Input
                                                    id="avatarInput"
                                                    type="file"
                                                    accept="image/*"
                                                    className="hidden"
                                                    onChange={handleFileChange}
                                                />
                                            </div>
                                            <div className="flex space-x-4">
                                                <div className="flex flex-col space-y-1.5 flex-1">
                                                    <FormField
                                                        control={form.control}
                                                        name="firstName"
                                                        render={({ field }) => (
                                                            <FormItem>
                                                                <FormLabel>Họ</FormLabel>
                                                                <FormControl>
                                                                    <Input placeholder="Họ" {...field} />
                                                                </FormControl>
                                                                <FormMessage />
                                                            </FormItem>
                                                        )}
                                                    />
                                                </div>
                                                <div className="flex flex-col space-y-1.5 flex-1">
                                                    <FormField
                                                        control={form.control}
                                                        name="lastName"
                                                        render={({ field }) => (
                                                            <FormItem>
                                                                <FormLabel>Tên</FormLabel>
                                                                <FormControl>
                                                                    <Input placeholder="Tên" {...field} />
                                                                </FormControl>
                                                                <FormMessage />
                                                            </FormItem>
                                                        )}
                                                    />
                                                </div>
                                            </div>
                                            <div className="flex flex-col space-y-1.5 flex-1">
                                                <FormField
                                                    control={form.control}
                                                    name="email"
                                                    render={({ field }) => (
                                                        <FormItem>
                                                            <FormLabel>Email</FormLabel>
                                                            <FormControl>
                                                                <Input disabled={true} placeholder="" {...field} />
                                                            </FormControl>
                                                            <FormMessage />
                                                        </FormItem>
                                                    )}
                                                />
                                            </div>
                                        </div>
                                    </form>
                                </CardContent>
                                <CardFooter className="flex justify-between">
                                    <Link href={"/change-password"} className="block rounded px-2 py-1 hover:bg-gray-200">
                                        <p className="text-[14px] font-semibold tracking-tight">Đổi mật khẩu</p>
                                    </Link>
                                </CardFooter>
                            </Card>
                            <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                                <CardHeader>
                                    <CardTitle>Tùy chọn cấu hình</CardTitle>
                                </CardHeader>
                                <CardContent>
                                    <form>
                                        <div className="grid w-full items-center gap-4">
                                            <div className="flex flex-col space-y-1.5">
                                                <FormField
                                                    control={form.control}
                                                    name="cityId"
                                                    render={({ field }) => (
                                                        <FormItem>
                                                            <FormLabel>Thành phố</FormLabel>
                                                            <FormControl>
                                                                <Select
                                                                    key={field.value}
                                                                    value={field.value?.toString()} // Lấy giá trị từ field
                                                                    onValueChange={(value) => {
                                                                        console.log(value)
                                                                        setCityId(Number(value))
                                                                        field.onChange(Number(value))
                                                                    }}>
                                                                    <SelectTrigger id="framework">
                                                                        <SelectValue placeholder="Chọn thành phố" />
                                                                    </SelectTrigger>
                                                                    <SelectContent position="popper">
                                                                        {
                                                                            cities?.data.map((item) => {
                                                                                return <SelectItem key={item.id} value={item.id!.toString()}>{item.name}</SelectItem>
                                                                            })
                                                                        }
                                                                    </SelectContent>
                                                                </Select>
                                                            </FormControl>
                                                            <FormMessage />
                                                        </FormItem>
                                                    )}
                                                />
                                            </div>
                                            <div className="flex flex-col space-y-1.5">
                                                <FormField
                                                    control={form.control}
                                                    name="districtId"
                                                    render={({ field }) => (
                                                        <FormItem>
                                                            <FormLabel>Huyện</FormLabel>
                                                            <FormControl>
                                                                <Select
                                                                    disabled={cityId == 0}
                                                                    key={field.value}
                                                                    value={field.value?.toString()} // Lấy giá trị từ field
                                                                    onValueChange={(value) => {
                                                                        setDistrictId(Number(value))
                                                                        field.onChange(Number(value))
                                                                    }}>
                                                                    <SelectTrigger id="framework">
                                                                        <SelectValue placeholder="Chọn huyện" />
                                                                    </SelectTrigger>
                                                                    <SelectContent position="popper">
                                                                        {
                                                                            districts?.data.map((item) => {
                                                                                return <SelectItem key={item.id} value={item.id!.toString()}>{item.name}</SelectItem>
                                                                            })
                                                                        }
                                                                    </SelectContent>
                                                                </Select>
                                                            </FormControl>
                                                            <FormMessage />
                                                        </FormItem>
                                                    )}
                                                />
                                            </div>
                                            <div className="flex flex-col space-y-1.5">
                                                <FormField
                                                    control={form.control}
                                                    name="wardId"
                                                    render={({ field }) => (
                                                        <FormItem>
                                                            <FormLabel>Phường</FormLabel>
                                                            <FormControl>
                                                                <Select
                                                                    disabled={districtId == 0}
                                                                    key={field.value}
                                                                    value={field.value?.toString()} // Lấy giá trị từ field
                                                                    onValueChange={(value) => { field.onChange(Number(value)); console.log(value) }}>
                                                                    <SelectTrigger id="framework">
                                                                        <SelectValue placeholder="Chọn phường" />
                                                                    </SelectTrigger>
                                                                    <SelectContent position="popper">
                                                                        {
                                                                            wards?.data.map((item) => {
                                                                                return <SelectItem key={item.id} value={item.id!.toString()}>{item.name}</SelectItem>
                                                                            })
                                                                        }
                                                                    </SelectContent>
                                                                </Select>
                                                            </FormControl>
                                                            <FormMessage />
                                                        </FormItem>
                                                    )}
                                                />
                                            </div>
                                            <div className="flex flex-col space-y-1.5">
                                                <FormField
                                                    control={form.control}
                                                    name="dateDisplay"
                                                    render={({ field }) => (
                                                        <FormItem>
                                                            <FormLabel>Định dạng ngày</FormLabel>
                                                            <FormControl>
                                                                <Select
                                                                    key={field.value}
                                                                    value={field.value?.toString()} // Lấy giá trị từ field
                                                                    onValueChange={(value) => {
                                                                        const enumValue = parseInt(value, 10); // Chuyển giá trị chuỗi về kiểu số
                                                                        field.onChange(enumValue); // Truyền lại giá trị enum số vào field
                                                                    }}
                                                                >
                                                                    <SelectTrigger id="framework">
                                                                        <SelectValue placeholder="Chọn định dạng ngày" />
                                                                    </SelectTrigger>
                                                                    <SelectContent position="popper">
                                                                        <SelectItem value={DateDisplay.Iso8601.toString()}>YYYY-MM-DD</SelectItem>
                                                                        <SelectItem value={DateDisplay.American.toString()}>MM/DD/YYYY</SelectItem>
                                                                        <SelectItem value={DateDisplay.British.toString()}>DD/MM/YYYY</SelectItem>
                                                                        <SelectItem value={DateDisplay.Vietnamese.toString()}>DD-MM-YYYY</SelectItem>
                                                                        <SelectItem value={DateDisplay.Short.toString()}>DD MMM YYYY</SelectItem>
                                                                        <SelectItem value={DateDisplay.Full.toString()}>Day, DD Month YYYY</SelectItem>
                                                                        <SelectItem value={DateDisplay.Compact.toString()}>YY/MM/DD</SelectItem>
                                                                    </SelectContent>
                                                                </Select>
                                                            </FormControl>
                                                            <FormMessage />
                                                        </FormItem>
                                                    )}
                                                />
                                            </div>
                                            <div className="flex flex-col space-y-1.5">
                                                <FormField
                                                    control={form.control}
                                                    name="timeDisplay"
                                                    render={({ field }) => (
                                                        <FormItem>
                                                            <FormLabel>Định dạng giờ&phút</FormLabel>
                                                            <FormControl>
                                                                <Select
                                                                    key={field.value}
                                                                    value={field.value?.toString()} // Lấy giá trị từ field
                                                                    onValueChange={(value) => {
                                                                        const enumValue = parseInt(value, 10); // Chuyển giá trị chuỗi về kiểu số
                                                                        field.onChange(enumValue); // Truyền lại giá trị enum số vào field
                                                                    }}
                                                                >
                                                                    <SelectTrigger id="framework">
                                                                        <SelectValue placeholder="Chọn định dạng giờ&phút" />
                                                                    </SelectTrigger>
                                                                    <SelectContent position="popper">
                                                                        <SelectItem value={TimeDisplay.TwelveHour.toString()}>hh:mm:ss AM/PM</SelectItem>
                                                                        <SelectItem value={TimeDisplay.TwentyFourHour.toString()}>HH:mm:ss</SelectItem>
                                                                        <SelectItem value={TimeDisplay.Compact.toString()}>HH:mm</SelectItem>
                                                                    </SelectContent>
                                                                </Select>
                                                            </FormControl>
                                                            <FormMessage />
                                                        </FormItem>
                                                    )}
                                                />
                                            </div>
                                        </div>
                                    </form>
                                </CardContent>
                            </Card>
                        </div>
                        <div className='grid gap-4 md:grid-cols-2 lg:grid-cols-8 mt-8'>
                            <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                                <CardHeader>
                                    <CardTitle>Thông tin liên lạc</CardTitle>
                                </CardHeader>
                                <CardContent>
                                    <form>
                                        <div className="grid w-full items-center gap-4">
                                            <div className="flex space-x-4">
                                                <div className="flex flex-col space-y-1.5 flex-1">
                                                    <FormField
                                                        control={form.control}
                                                        name="mobilePhoneNumber"
                                                        render={({ field }) => (
                                                            <FormItem>
                                                                <FormLabel>Điện thoại cá nhân</FormLabel>
                                                                <FormControl>
                                                                    <Input placeholder="" {...field} />
                                                                </FormControl>
                                                                <FormMessage />
                                                            </FormItem>
                                                        )}
                                                    />
                                                </div>
                                                <div className="flex flex-col space-y-1.5 flex-1">
                                                    <FormField
                                                        control={form.control}
                                                        name="workPhoneNumber"
                                                        render={({ field }) => (
                                                            <FormItem>
                                                                <FormLabel>Điện thoại công sở</FormLabel>
                                                                <FormControl>
                                                                    <Input
                                                                        placeholder="Nhập số điện thoại công sở"
                                                                        {...field}
                                                                        value={field.value || ''} // Đảm bảo giá trị được hiển thị dưới dạng chuỗi
                                                                    />
                                                                </FormControl>
                                                                <FormMessage />
                                                            </FormItem>
                                                        )}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </CardContent>
                            </Card>
                            <Card className="rounded-xl border bg-card text-card-foreground shadow col-span-4">
                                <CardHeader>
                                    <CardTitle>Công việc</CardTitle>
                                </CardHeader>
                                <CardContent>
                                    <form>
                                        <div className="grid w-full items-center gap-4">
                                            <div className="flex space-x-4">
                                                <div className="flex flex-col space-y-1.5 flex-1">
                                                    <FormField
                                                        control={form.control}
                                                        name="employer"
                                                        render={({ field }) => (
                                                            <FormItem>
                                                                <FormLabel>Tên công việc</FormLabel>
                                                                <FormControl>
                                                                    <Input placeholder="" {...field} />
                                                                </FormControl>
                                                                <FormMessage />
                                                            </FormItem>
                                                        )}
                                                    />
                                                </div>
                                                <div className="flex flex-col space-y-1.5 flex-1">
                                                    <FormField
                                                        control={form.control}
                                                        name="jobTitleId"
                                                        render={({ field }) => (
                                                            <FormItem>
                                                                <FormLabel>Chức danh</FormLabel>
                                                                <FormControl>
                                                                    <Select
                                                                        key={field.value}
                                                                        value={field.value?.toString()} // Lấy giá trị từ field
                                                                        onValueChange={(value) => field.onChange(value)}>
                                                                        <SelectTrigger id="framework">
                                                                            <SelectValue placeholder="Chọn chức danh" />
                                                                        </SelectTrigger>
                                                                        <SelectContent position="popper">
                                                                            {
                                                                                jobTitles?.data.map((item) => {
                                                                                    return <SelectItem key={item.id} value={item.id!.toString()}>{item.name}</SelectItem>
                                                                                })
                                                                            }
                                                                        </SelectContent>
                                                                    </Select>
                                                                </FormControl>
                                                                <FormMessage />
                                                            </FormItem>
                                                        )}
                                                    />
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </CardContent>
                            </Card>
                        </div>
                    </div>
                </form>
            </Form>
        </>
    )
}
