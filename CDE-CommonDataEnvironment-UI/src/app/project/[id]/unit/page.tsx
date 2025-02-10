"use client"
import unitApiRequest from '@/apis/unit.api';
import AppBreadcrumb, { PathItem } from '@/components/custom/_breadcrumb';
import { Button } from '@/components/custom/button';
import { Checkbox } from '@/components/ui/checkbox';
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form';
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select';
import { UnitAngle } from '@/data/enums/unitangle.enum';
import { UnitAnglePrecision } from '@/data/enums/unitangleprecision.enum';
import { UnitArea } from '@/data/enums/unitarea.enum';
import { UnitAreaPrecision } from '@/data/enums/unitareaprecision.enm';
import { UnitLength } from '@/data/enums/unitlength.enum';
import { UnitLengthPrecision } from '@/data/enums/unitlengthprecision.enum';
import { UnitSystem } from '@/data/enums/unitsystem.enum';
import { UnitVolume } from '@/data/enums/unitvolume.enum';
import { UnitVolumePrecision } from '@/data/enums/unitvolumeprecision.enum';
import { UnitWeight } from '@/data/enums/unitweight.enum';
import { UnitWeightPrecision } from '@/data/enums/unitweigthprecision.enum';
import { Unit, unitDefault, unitSchema } from '@/data/schema/Project/unit.schema';
import { handleSuccessApi } from '@/lib/utils';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import React, { useEffect } from 'react';
import { useForm } from 'react-hook-form';


const pathList: Array<PathItem> = [
  {
    name: "Thông tin về độ đo",
    url: "#"
  },
];

export default function Page({ params }: { params: { id: string } }) {
  const queryClient = useQueryClient()
  const form = useForm<Unit>({
    resolver: zodResolver(unitSchema),
    defaultValues: unitDefault
  });

  const onSubmit = (values: Unit) => {
    mutate(values);
  };
  const { data, isLoading } = useQuery({
    queryKey: ['unit'],
    queryFn: () => unitApiRequest.getUnit(Number(params.id)),
  })
  const { mutate, isPending } = useMutation({
    mutationKey: ['update-unit'],
    mutationFn: (data: Unit) => unitApiRequest.updateUnit(data),
    onSuccess: () => {
      handleSuccessApi({
        title: "Sửa thành công",
        message: "Sửa thành công hệ thống đơn vị"
      })
      queryClient.invalidateQueries({ queryKey: ['unit'] })
    }
  })
  useEffect(() => {
    console.log(form.formState.errors)
  }, [form.formState.errors])
  useEffect(() => {
    if (data) {
      console.log("f", typeof params.id)
      form.setValue("projectId", Number(params.id))
      form.setValue("unitSystem", data.data.unitSystem)
      form.setValue("unitLength", data.data.unitLength)
      form.setValue("unitLengthPrecision", data.data.unitLengthPrecision)
      form.setValue("isCheckMeasurement", data.data.isCheckMeasurement)
      form.setValue("unitArea", data.data.unitArea)
      form.setValue("unitAreaPrecision", data.data.unitAreaPrecision)
      form.setValue("unitWeight", data.data.unitWeight)
      form.setValue("unitWeightPrecision", data.data.unitWeightPrecision)
      form.setValue("unitVolume", data.data.unitVolume)
      form.setValue("unitVolumePrecision", data.data.unitVolumePrecision)
      form.setValue("unitAngle", data.data.unitAngle)
      form.setValue("unitAnglePrecision", data.data.unitAnglePrecision)

    }
  }, [data])
  if (isLoading) return <></>
  return (
    <Form {...form} >
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <div className='mb-2 flex items-center justify-between space-y-2'>
          <div>
            <h2 className='text-2xl font-bold tracking-tight'>Đơn vị đo</h2>
            <AppBreadcrumb pathList={pathList} className="mt-2" />
          </div>
          <div>
            <Button type='submit' loading={isPending}>Lưu</Button>
          </div>
        </div>
        <div className='-mx-4 flex-1 overflow-auto px-4 py-8 lg:flex-row'>
          <div className="flex flex-col space-y-1.5 flex-1">
            <FormField
              control={form.control}
              name="unitSystem"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Hệ thống đơn vị</FormLabel>
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
                        <SelectValue placeholder="Chọn hệ thống đơn vị" />
                      </SelectTrigger>
                      <SelectContent position="popper">
                        <SelectItem key={UnitSystem.Metric.toString()} value={UnitSystem.Metric.toString()}>Hệ đơn vị mét</SelectItem>
                      </SelectContent>
                    </Select>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="flex space-x-4 mt-2">
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitLength"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Đơn vị độ dài</FormLabel>
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
                          <SelectValue placeholder="Chọn đơn vị độ dài" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitLength.Feet.toString()} value={UnitLength.Feet.toString()}>Feet</SelectItem>
                          <SelectItem key={UnitLength.Meters.toString()} value={UnitLength.Meters.toString()}>Mét</SelectItem>
                          <SelectItem key={UnitLength.Inches.toString()} value={UnitLength.Inches.toString()}>Inch</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitLengthPrecision"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Hiển thị độ chính xác</FormLabel>
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
                          <SelectValue placeholder="Chọn độ chính xác" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitLengthPrecision.Zero.toString()} value={UnitLengthPrecision.Zero.toString()}>0</SelectItem>
                          <SelectItem key={UnitLengthPrecision.OneTenth.toString()} value={UnitLengthPrecision.OneTenth.toString()}>0.1</SelectItem>
                          <SelectItem key={UnitLengthPrecision.OneHundredth.toString()} value={UnitLengthPrecision.OneHundredth.toString()}>0.01</SelectItem>
                          <SelectItem key={UnitLengthPrecision.OneThousandth.toString()} value={UnitLengthPrecision.OneThousandth.toString()}>0.001</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>
          <div className="flex flex-col space-y-1.5 flex-1 mt-2">
            <FormField
              control={form.control}
              name="isCheckMeasurement"
              render={({ field }) => (
                <FormItem className="flex items-center space-x-2">
                  <FormControl>
                    <Checkbox
                      id="isCheckMeasurement"
                      checked={field.value}
                      onCheckedChange={field.onChange}
                    />
                  </FormControl>
                  <FormLabel htmlFor="isCheckMeasurement">
                    Sử dụng cùng một cài đặt cho tất cả các phép đo
                  </FormLabel>
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <div className="flex space-x-4 mt-2">
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitArea"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Đơn vị diện tích</FormLabel>
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
                          <SelectValue placeholder="Chọn đơn vị độ dài" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitArea.SquareMeters.toString()} value={UnitArea.SquareMeters.toString()}>m^2</SelectItem>
                          <SelectItem key={UnitArea.SquareDecimeters.toString()} value={UnitArea.SquareDecimeters.toString()}>dm^2</SelectItem>
                          <SelectItem key={UnitArea.SquareCentimeters.toString()} value={UnitArea.SquareCentimeters.toString()}>cm^2</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitAreaPrecision"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Hiển thị độ chính xác</FormLabel>
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
                          <SelectValue placeholder="Chọn độ chính xác" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitAreaPrecision.Zero.toString()} value={UnitAreaPrecision.Zero.toString()}>0</SelectItem>
                          <SelectItem key={UnitAreaPrecision.OneTenth.toString()} value={UnitAreaPrecision.OneTenth.toString()}>0.1</SelectItem>
                          <SelectItem key={UnitAreaPrecision.OneHundredth.toString()} value={UnitAreaPrecision.OneHundredth.toString()}>0.01</SelectItem>
                          <SelectItem key={UnitAreaPrecision.OneThousandth.toString()} value={UnitAreaPrecision.OneThousandth.toString()}>0.001</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>
          <div className="flex space-x-4 mt-2">
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitVolume"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Đơn vị thể tích</FormLabel>
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
                          <SelectValue placeholder="Chọn đơn vị thể tích" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitVolume.CubicCentimeters.toString()} value={UnitVolume.CubicCentimeters.toString()}>cm^3</SelectItem>
                          <SelectItem key={UnitVolume.CubicDecimeters.toString()} value={UnitVolume.CubicDecimeters.toString()}>dm^3</SelectItem>
                          <SelectItem key={UnitVolume.CubicMeters.toString()} value={UnitVolume.CubicMeters.toString()}>m^3</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitVolumePrecision"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Hiển thị độ chính xác</FormLabel>
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
                          <SelectValue placeholder="Chọn độ chính xác" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitVolumePrecision.Zero.toString()} value={UnitVolumePrecision.Zero.toString()}>0</SelectItem>
                          <SelectItem key={UnitVolumePrecision.OneTenth.toString()} value={UnitVolumePrecision.OneTenth.toString()}>0.1</SelectItem>
                          <SelectItem key={UnitVolumePrecision.OneHundredth.toString()} value={UnitVolumePrecision.OneHundredth.toString()}>0.01</SelectItem>
                          <SelectItem key={UnitVolumePrecision.OneThousandth.toString()} value={UnitVolumePrecision.OneThousandth.toString()}>0.001</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>
          <div className="flex space-x-4 mt-2">
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitWeight"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Đơn vị khối lượng</FormLabel>
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
                          <SelectValue placeholder="Chọn đơn vị độ dài" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitWeight.Kilograms.toString()} value={UnitWeight.Kilograms.toString()}>kg</SelectItem>
                          <SelectItem key={UnitWeight.Miligrams.toString()} value={UnitWeight.Miligrams.toString()}>mg</SelectItem>
                          <SelectItem key={UnitWeight.Grams.toString()} value={UnitWeight.Grams.toString()}>g</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitWeightPrecision"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Hiển thị độ chính xác</FormLabel>
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
                          <SelectValue placeholder="Chọn độ chính xác" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitWeightPrecision.Zero.toString()} value={UnitWeightPrecision.Zero.toString()}>0</SelectItem>
                          <SelectItem key={UnitWeightPrecision.OneTenth.toString()} value={UnitWeightPrecision.OneTenth.toString()}>0.1</SelectItem>
                          <SelectItem key={UnitWeightPrecision.OneHundredth.toString()} value={UnitWeightPrecision.OneHundredth.toString()}>0.01</SelectItem>
                          <SelectItem key={UnitWeightPrecision.OneThousandth.toString()} value={UnitWeightPrecision.OneThousandth.toString()}>0.001</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          </div>
          <div className="flex space-x-4 mt-2">
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitAngle"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Đơn vị góc</FormLabel>
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
                          <SelectValue placeholder="Chọn đơn vị góc" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitAngle.Degrees.toString()} value={UnitAngle.Degrees.toString()}>Deg</SelectItem>
                          <SelectItem key={UnitAngle.Radians.toString()} value={UnitAngle.Radians.toString()}>Rad</SelectItem>
                        </SelectContent>
                      </Select>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
            <div className="flex flex-col space-y-1.5 flex-1">
              <FormField
                control={form.control}
                name="unitAnglePrecision"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Hiển thị độ chính xác</FormLabel>
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
                          <SelectValue placeholder="Chọn độ chính xác" />
                        </SelectTrigger>
                        <SelectContent position="popper">
                          <SelectItem key={UnitAnglePrecision.Zero.toString()} value={UnitAnglePrecision.Zero.toString()}>0</SelectItem>
                          <SelectItem key={UnitAnglePrecision.OneTenth.toString()} value={UnitAnglePrecision.OneTenth.toString()}>0.1</SelectItem>
                          <SelectItem key={UnitAnglePrecision.OneHundredth.toString()} value={UnitAnglePrecision.OneHundredth.toString()}>0.01</SelectItem>
                          <SelectItem key={UnitAnglePrecision.OneThousandth.toString()} value={UnitAnglePrecision.OneThousandth.toString()}>0.001</SelectItem>
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
    </Form>
  );
}