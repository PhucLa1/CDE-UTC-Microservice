import viewApiRequest from "@/apis/view.api";
import { Button } from "@/components/custom/button";
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
import { handleSuccessApi } from "@/lib/utils";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ReactNode, useState } from "react";

type FormProps = {
  id: number;
  node: ReactNode;
  setSheetOpen: (value: boolean) => void;
};

export default function DeleteView({ id, node, setSheetOpen }: FormProps) {
  const [isOpen, setIsOpen] = useState<boolean>(false);
  const queryClient = useQueryClient();
  const { mutate: mutateDelete, isPending: isPendingDelete } = useMutation({
    mutationKey: ["delete-view"],
    mutationFn: () => viewApiRequest.delete(id),
    onSuccess: () => {
      handleSuccessApi({
        title: "Xóa chế độ xem",
        message: "Xóa chế độ xem thành công",
      });
      queryClient.invalidateQueries({ queryKey: ["views"] });
      setIsOpen(false);
      setSheetOpen(false);
    },
  });
  return (
    <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
      <AlertDialogTrigger asChild>{node}</AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>
            Bạn có chác chắn muốn xóa chế độ xem này không
          </AlertDialogTitle>
          <AlertDialogDescription>
            Hành động này sẽ không thể khôi phục nên làm ơn chú ý
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Hủy</AlertDialogCancel>
          <Button
            loading={isPendingDelete}
            onClick={() => {
              mutateDelete();
            }}
          >
            Tiếp tục
          </Button>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}
