import {
  AlertDialog,
  AlertDialogTrigger,
  AlertDialogContent,
  AlertDialogHeader,
  AlertDialogFooter,
  AlertDialogTitle,
  AlertDialogCancel,
} from "@/components/ui/alert-dialog"
import { Tabs, TabsList, TabsTrigger, TabsContent } from "@/components/ui/tabs"
import { Button } from "@/components/ui/button"
import { ImageIcon, Laptop } from "lucide-react"
import StorageChoose from "./storage-choose"
import ViewChoose from "./view-choose"
import { View } from "@/data/schema/Project/view.schema"
import { File } from "@/data/schema/Project/file.schema"



interface Props {
  isOpen: boolean;
  setIsOpen: (value: boolean) => void
  projectId: number;
  selectedFiles: File[];
  setSelectedFiles: React.Dispatch<React.SetStateAction<File[]>>;
  selectedViews: View[];
  setSelectedViews: React.Dispatch<React.SetStateAction<View[]>>;
}

export default function FileReferenceDialog({isOpen, setIsOpen, projectId, selectedFiles, setSelectedFiles, selectedViews, setSelectedViews} : Props) {
  return (
    <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
      <AlertDialogTrigger asChild>
        {}
      </AlertDialogTrigger>
      <AlertDialogContent className="w-[650px] max-w-full">
        <AlertDialogHeader>
          <AlertDialogTitle className="text-lg">Thêm liên kết</AlertDialogTitle>
        </AlertDialogHeader>

        <Tabs defaultValue="files" className="mt-2">
          <TabsList className="grid w-full grid-cols-2">
            <TabsTrigger value="files" className="flex gap-1 items-center justify-center">
              <ImageIcon className="w-4 h-4" /> 
            </TabsTrigger>
            <TabsTrigger value="device" className="flex gap-1 items-center justify-center">
              <Laptop className="w-4 h-4" />
            </TabsTrigger>
          </TabsList>

          <TabsContent value="files">
           <StorageChoose projectId={projectId} selectedFiles={selectedFiles} setSelectedFiles={setSelectedFiles} />
          </TabsContent>

          <TabsContent value="device">
            <ViewChoose projectId={projectId} selectedViews={selectedViews} setSelectedViews={setSelectedViews}/>
          </TabsContent>

        </Tabs>

        <AlertDialogFooter className="mt-4">
          <AlertDialogCancel onClick={() => {
            setSelectedFiles([])
            setSelectedViews([])
          }}>Hủy</AlertDialogCancel>
          <Button onClick={() => setIsOpen(false)}>Thêm tham chiếu</Button>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  )
}
