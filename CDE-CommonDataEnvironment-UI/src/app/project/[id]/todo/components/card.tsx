// components/TaskCard.tsx
import { Card, CardContent } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Bookmark, Circle, Eye } from "lucide-react";
import { Todo } from "@/data/schema/Project/todo.schema";

interface Props {
  todo: Todo;
  setIsOpen: React.Dispatch<React.SetStateAction<boolean | Todo>>;
  setMode: (value: "ADD" | "UPDATE" | "VIEW") => void;
  indexx: number;
}

export function TaskCard({ todo, setIsOpen, setMode, indexx }: Props) {
  return (
    <Card className="mb-4 w-full ">
      <CardContent className="p-8">
        <div className="flex justify-between items-start">
          <div>
            <p className="text-xs font-semibold tracking-wide text-muted-foreground">
              Việc cần làm - {indexx}
            </p>
            <h3 className="text-base font-medium">{todo.name}</h3>
            <p className="text-sm mt-1">
              <span className="font-semibold">Tạo bởi:</span>{" "}
              {todo.nameCreatedBy}
            </p>
            <p className="text-xs mt-2">
              <span className="font-semibold">Mô tả :</span> :{" "}
              {todo.description}
            </p>
            <div className="flex flex-wrap items-center gap-2 mt-2">
              <p className="text-xs text-gray-800">Đặc điểm: </p>
              <Badge className="p-2" variant="outline">
                <Bookmark
                  className={`w-3.5 h-3.5 mr-1`}
                  color={todo.priority?.colorRGB}
                />{" "}
                {todo.priority?.name}
              </Badge>
              <Badge className="p-2" variant="outline">
                <Circle
                  className="w-3.5 h-3.5 mr-1"
                  color={todo.status?.colorRGB}
                />{" "}
                {todo.status?.name}
              </Badge>
              <Badge className="p-2" variant="outline">
                <img
                  className="w-3.5 h-3.5 mr-1"
                  src={`https://localhost:5052/Types/${todo.type?.iconImageUrl}`}
                  alt="#"
                />{" "}
                {todo.type?.name}
              </Badge>
            </div>
            <div className="flex flex-wrap items-center gap-2 mt-2">
              <p className="text-xs text-gray-800">Nhãn dán: </p>
              {todo.tags?.map((item, index) => {
                return (
                  <Badge key={index} className="p-2" variant="outline">
                    {item.name}
                  </Badge>
                );
              })}
            </div>
          </div>
          <div className="text-right mt-4">
            <p className="text-sm text-muted-foreground text-center">
              {todo.startDate?.slice(0, 10)} - {todo.dueDate?.slice(0, 10)}
            </p>
            <Eye
              onClick={() => {
                setIsOpen(todo);
                setMode("VIEW");
              }}
              className="w-4 h-4 mt-2 text-muted-foreground cursor-pointer"
            />
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
