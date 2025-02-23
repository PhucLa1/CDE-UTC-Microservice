import {
    Breadcrumb,
    BreadcrumbItem,
    BreadcrumbLink,
    BreadcrumbList,
    BreadcrumbPage,
    BreadcrumbSeparator,
} from "@/components/ui/breadcrumb"
import { Fragment } from "react";
export interface PathItem {
    name: string,
    url?: string
};

export default function AppBreadcrumbSheet({ pathList, className, setParentId }: { pathList: Array<PathItem>, className?: string, setParentId: (value: number) => void }) {
    return (
        <div className={className}>
            <Breadcrumb>
                <BreadcrumbList>
                    {pathList && pathList.length > 0 && pathList.map((x, i) => {
                        const isLastItem = i == pathList.length - 1;
                        return (
                            <Fragment key={i}>
                                <BreadcrumbItem className="cursor-pointer">
                                    {isLastItem && <BreadcrumbPage>{x.name}</BreadcrumbPage>}
                                    {!isLastItem && <BreadcrumbLink onClick={() => setParentId(Number(x.url))}>{x.name}</BreadcrumbLink>}
                                </BreadcrumbItem>
                                {!isLastItem && <BreadcrumbSeparator />}
                            </Fragment>)
                    })}
                </BreadcrumbList>
            </Breadcrumb>
        </div>

    )
}
