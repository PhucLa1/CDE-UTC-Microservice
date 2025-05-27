"use client";
import viewApiRequest from '@/apis/view.api';
import { Button } from '@/components/custom/button';
import {
  AlertDialog,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { Input } from '@/components/ui/input';
import { Textarea } from '@/components/ui/textarea';
import { annotationSchema, View, viewDefault, viewSchema } from '@/data/schema/Project/view.schema';
import { handleSuccessApi } from '@/lib/utils';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation } from '@tanstack/react-query';
import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import PDFViewer from '../../_components/pdf-viewer';
import View3D from '../../_components/view-3d';

const Url3DFiles = ['gltf', 'glb', 'obj', 'fbx'];
export default function ViewerPageFile({ params }: { params: { fileId: string } }) {
  const [fileUrl, setFileUrl] = useState<string | null>(null);
  const [annotationList, setAnnotationList] = useState<z.infer<typeof annotationSchema>[]>([])

  const addAnnotation = (annotation: z.infer<typeof annotationSchema>) => {
    setAnnotationList((prevList) => {
      if (!prevList) {
        return [annotation]; // Nếu prevList là undefined, tạo mảng mới với annotation
      }
      return [...prevList, annotation]; // Thêm annotation vào danh sách hiện tại
    });
  }
  const form = useForm<View>({
    resolver: zodResolver(viewSchema),
    defaultValues: viewDefault
  });

  const { mutate, isPending } = useMutation({
    mutationKey: ['create-view'],
    mutationFn: (view: View) => viewApiRequest.create(view),
    onSuccess: () => {
      handleSuccessApi({
        title: "Tạo mới chế độ xem thành công",
        message: "Chế độ xem được tạo thành công"
      })
    }
  })

  const onSubmit = (values: View) => {
    values.fileId = Number(params.fileId)
    values.annotationModels = annotationList;
    console.log(values)
    mutate(values)
  };

  useEffect(() => {
    // Lấy URL từ query parameter trên client-side
    const currentUrl = window.location.href;
    const urlParams = new URL(currentUrl).searchParams;
    const url = urlParams.get('url');

    if (url) {
      setFileUrl(decodeURIComponent(url)); // Giải mã URL để lấy giá trị gốc
    }
  }, []); // Chỉ chạy một lần khi component mount


  if (!fileUrl) {
    return <div>Đang tải...</div>; // Hiển thị loading trong khi lấy URL
  }

  return (
    <div>
      <div className='flex items-center justify-start'>
        {!Url3DFiles.includes(fileUrl.split('.').pop() ?? '') && <AlertDialog>
          <AlertDialogTrigger asChild>
            <Button className='ml-4'>Tạo mới chế độ xem</Button>
          </AlertDialogTrigger>
          <AlertDialogContent>
            <AlertDialogHeader>
              <AlertDialogTitle>Tạo mới một chế độ xem?</AlertDialogTitle>
              <AlertDialogDescription>
                Hành động này sẽ tạo mới một chế độ xem
              </AlertDialogDescription>
            </AlertDialogHeader>
            <Form {...form}>
              <form onSubmit={form.handleSubmit(onSubmit)}>
                <div className="flex flex-col space-y-1.5 flex-1">
                  <FormField
                    control={form.control}
                    name="name"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Tên</FormLabel>
                        <FormControl>
                          <Input placeholder="Chế độ xem A1" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>
                <div className="flex flex-col space-y-1.5 flex-1">
                  <FormField
                    control={form.control}
                    name="description"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel>Mô tả</FormLabel>
                        <FormControl>
                          <Textarea placeholder="Nhập miêu tả" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>

                <AlertDialogFooter>
                  <AlertDialogCancel>Hủy</AlertDialogCancel>
                  <Button loading={isPending} type='submit'>Tiếp tục</Button>
                </AlertDialogFooter>
              </form>
            </Form>
          </AlertDialogContent>
        </AlertDialog>}
      </div>
      {!Url3DFiles.includes(fileUrl.split('.').pop() ?? '') && <PDFViewer viewId={0} annotationList={[]} addAnnotation={addAnnotation} url={fileUrl} />}
      {Url3DFiles.includes(fileUrl.split('.').pop() ?? '') && (
        <View3D
          fileUrl={fileUrl}
          fileType={
            (['gltf', 'glb', 'obj', 'fbx'].includes(fileUrl.split('.').pop() ?? '')
              ? (fileUrl.split('.').pop() as 'gltf' | 'glb' | 'obj' | 'fbx')
              : 'gltf')
          }
        />
      )}
    </div>
  );
}