import React, { useState } from 'react'
import { Tabs, TabsList, TabsTrigger, TabsContent } from "@/components/ui/tabs"
import { Eye, ImageIcon, Laptop, Pencil, Trash } from 'lucide-react'
import { FaComment } from 'react-icons/fa'
import { View } from '@/data/schema/Project/view.schema'
import { File } from '@/data/schema/Project/file.schema'
import CreateTodoComment from './create-todo-comment'
import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar'
import { Card } from '@/components/ui/card'
import DeleteFormComment from './delete-form-comment'
import todoCommentApiRequest from '@/apis/todocomment.api'
import { useQuery } from '@tanstack/react-query'
import UpdateTodoComment from './update-todo-comment'
import { Role } from '@/data/enums/role.enum'
import { useRole } from '@/hooks/use-role'
interface Props{
  views: View[],
  files: File[],
  todoId: number
}
export default function TabView({views, files, todoId} : Props) {
  const {roleDetail} = useRole()
  const [updateComment, setUpdateComment] = useState<number>(0)
  const {data: dataTodoComment, isLoading: isLoadingComment} = useQuery({
    queryKey: ['get-todo-comments'],
    queryFn: () => todoCommentApiRequest.getListByTodoId(todoId),
  })
  return (
    <div className='h-[400px]'>
      <Tabs defaultValue="files" className="mt-2">
        <TabsList className="grid w-full grid-cols-3">
          <TabsTrigger value="comment" className="flex gap-1 items-center justify-center">
            <FaComment className="w-4 h-4" /> 
          </TabsTrigger>
          <TabsTrigger value="device" className="flex gap-1 items-center justify-center">
            <Laptop className="w-4 h-4" />
          </TabsTrigger>
          <TabsTrigger value="files" className="flex gap-1 items-center justify-center">
            <ImageIcon className="w-4 h-4" />
          </TabsTrigger>
        </TabsList>

        <TabsContent value="comment">
        <div>
                        <h5 className="font-bold">Bình luận</h5>
                        <div className="mt-2">
                            {
                                !isLoadingComment && dataTodoComment ?  dataTodoComment.data.map((item, index) => {
                                    if (updateComment != item.id!) {
                                        return <Card key={index} className="mb-4 p-2">
                                            <div className="flex items-center justify-between">
                                                <div className="flex items-start">
                                                    <div className="mr-3">
                                                        <Avatar>
                                                            <AvatarImage src={item.avatarUrl} />
                                                            <AvatarFallback>CN</AvatarFallback>
                                                        </Avatar>
                                                    </div>
                                                    <div className="text-xs">
                                                        <p className="font-semibold">{item.name}</p>
                                                        <p className="text-xs text-gray-500">{item.updatedAt}</p>
                                                        <p className="mt-2">{item.content}</p>
                                                    </div>
                                                </div>
                                                {roleDetail?.role == Role.Admin || roleDetail?.id == item.updatedBy ? <div className="flex justify-end items-center">
                                                    <DeleteFormComment
                                                        node={<Trash className="w-4 h-4 cursor-pointer text-gray-500 hover:text-gray-900" />}
                                                        id={item.id!}
                                                    />
                                                    <Pencil onClick={() => setUpdateComment(item.id!)} className="w-4 h-4 ml-2 cursor-pointer text-gray-500 hover:text-gray-900" />
                                                </div> : <></>}

                                            </div>
                                        </Card>
                                    }
                                    else {
                                         return <UpdateTodoComment
                                            key={index}
                                            setUpdateComment={setUpdateComment}
                                            todoComment={{
                                                content: item.content,
                                                id: item.id,
                                                todoId: todoId
                                            }} />
                                    }

                                })
                            : <></>}
                            <CreateTodoComment id={todoId}/>
                        </div>
                    </div>
        </TabsContent>
        <TabsContent value="device">
            <div className="mt-3 max-h-[300px] overflow-y-auto space-y-1">
            {views.map((item, index) => (
              <div
                key={index}
                className="flex items-center gap-2 px-2 py-1 hover:bg-muted rounded"
              >
                <Eye className="w-5 h-5 text-blue-500" />
                <label htmlFor={`view-${index}`} className="text-sm truncate cursor-pointer">
                  {item.name}
                </label>
              </div>
            ))}
          </div>
        </TabsContent>

        <TabsContent value="files">
        <div className="mt-3 max-h-[300px] overflow-y-auto space-y-1">
            {files.map((item, index) => (
              <div
                key={index}
                className="flex items-center gap-2 px-2 py-1 hover:bg-muted rounded"
              >
                <img src={item.url} alt="#" className='w-5 h-5' />
                <label htmlFor={`file-${index}`} className="text-sm truncate cursor-pointer">
                  {item.name}
                </label>
              </div>
            ))}
          </div>
        </TabsContent>
        </Tabs>
    </div>
  )
}
