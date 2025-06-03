import viewApiRequest from '@/apis/view.api';
import { Checkbox } from "@/components/ui/checkbox";
import { View } from '@/data/schema/Project/view.schema';
import { useQuery } from '@tanstack/react-query';
import { Eye } from "lucide-react";
import React from 'react';

interface Props {
  projectId: number;
  selectedViews: View[];
  setSelectedViews: React.Dispatch<React.SetStateAction<View[]>>;
}



export default function ViewChoose({ projectId, selectedViews, setSelectedViews }: Props) {
  // const [selectedViews, setSelectedViews] = useState<number[]>([]);

  const { data: dataViews, isLoading: isLoadingViews } = useQuery({
    queryKey: ['get-list-views', projectId],
    queryFn: () => viewApiRequest.getList(projectId)
  });

  const toggleView = (view: View) => {
    setSelectedViews(prev =>
      prev.some(f => f.id === view.id)
        ? prev.filter(f => f.id !== view.id)
        : [...prev, view]
    );
  };

  return (
    <div>
      <p className="text-sm text-muted-foreground mt-2">
        Bạn có thể chọn nhiều mục hiển thị (view)
      </p>

      <div className="mt-3 max-h-[300px] overflow-y-auto space-y-1">
        {!isLoadingViews && dataViews ? dataViews.data.map((item, index) => (
          <div
            key={index}
            className="flex items-center gap-2 px-2 py-1 hover:bg-muted rounded"
          >
           <Checkbox
            id={`view-${index}`}
            checked={selectedViews.some(f => f.id === item.id)}
            onCheckedChange={() => toggleView(item)}
            />
            <Eye className="w-5 h-5 text-blue-500" />
            <label htmlFor={`view-${index}`} className="text-sm truncate cursor-pointer">
              {item.name}
            </label>
          </div>
        )) : []}
      </div>
    </div>
  );
}
