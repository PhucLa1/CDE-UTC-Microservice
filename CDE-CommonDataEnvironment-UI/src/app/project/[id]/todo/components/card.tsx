// components/TaskCard.tsx
import { Card, CardContent } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { MoreVertical, MessageSquare, Bookmark, Circle } from "lucide-react"
import { Tag } from "@/data/schema/Project/tag.schema"

type TaskCardProps = {
  id: number
  title: string
  author: string
  status?: string
  priority?: string
  comment?: string
  extra?: string
  priorityColor?: string
  statusColor?: string
  type?: string
  typeUrl?: string,
  tags?: Tag[],
  startDate?: string,
  dueDate?: string,
  des?: string
}

export function TaskCard({
  id,
  title,
  author,
  status,
  statusColor,
  priority,
  priorityColor,
  type,
  typeUrl,
  startDate,
  dueDate,
  tags,
  des
}: TaskCardProps) {
  return (
    <Card className="mb-4 w-full ">
      <CardContent className="p-8">
        <div className="flex justify-between items-start">
          <div>
            <p className="text-xs font-semibold tracking-wide text-muted-foreground">
              Việc cần làm - {id}
            </p>
            <h3 className="text-base font-medium">{title}</h3>
            <p className="text-sm mt-1">
              <span className="font-semibold">Tạo bởi:</span> {author}
            </p>
            <p className="text-xs mt-2">Mô tả : {des}</p>
            <div className="flex flex-wrap items-center gap-2 mt-2">
              <p className="text-xs text-gray-800">Đặc điểm: </p>
              <Badge className="p-2" variant="outline">
                <Bookmark className={`w-3.5 h-3.5 mr-1`} color={priorityColor} /> {priority}
              </Badge>
              <Badge className="p-2" variant="outline">
                <Circle className="w-3.5 h-3.5 mr-1" color={statusColor}/> {status}
              </Badge>
              <Badge className="p-2" variant="outline">
                <img className="w-3.5 h-3.5 mr-1" src={`https://localhost:5052/Types/${typeUrl}`} alt="#" /> {type}
              </Badge>
            </div>
            <div className="flex flex-wrap items-center gap-2 mt-2">
              <p className="text-xs text-gray-800">Nhãn dán: </p>
              {tags?.map((item, index) => {
                return (
                  <Badge key={index} className="p-2" variant="outline">
                    {item.name}
                  </Badge>
                )
              })}
            </div>
          </div>
          <div className="text-right mt-4">
            <p className="text-sm text-muted-foreground">{startDate} - {dueDate}</p>
            <MoreVertical className="w-4 h-4 mt-2 text-muted-foreground" />
          </div>
        </div>
      </CardContent>
    </Card>
  )
}
