import React from "react";

import { AlignJustifyIcon, ChevronDownIcon, EllipsisVerticalIcon } from "lucide-react";

import type { CourseModulesDialogs } from "@/pages/(private)/(dashboard)/courses/[id]/modules/page.tsx";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent } from "@/components/ui/collapsible.tsx";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";

import { useDeleteCourseModuleMutation } from "@/redux/services/course.ts";

import type { CourseModuleDTO } from "@/lib/types/course.ts";

type CourseModuleProps = {
  data: CourseModuleDTO;
  changeDialog: (id: number | null, type: Pick<CourseModulesDialogs, "type">["type"]) => void;
};

export const CourseModule = (props: CourseModuleProps) => {
  const courseModule = props.data;
  const [deleteCourseModule] = useDeleteCourseModuleMutation();

  const [open, setOpen] = React.useState(true);

  return (
    <Collapsible open={open} onOpenChange={setOpen} className="group/collapsible">
      <div className="flex w-full items-center justify-between space-x-2 border-b p-2">
        <div onClick={() => setOpen(!open)} className="flex grow items-center justify-center hover:cursor-pointer">
          <div className="flex items-center space-x-2">
            <AlignJustifyIcon />
            <span className="text-lg">{courseModule.name}</span>
          </div>
          <ChevronDownIcon className={`ml-auto transition-transform group-data-[state=open]/collapsible:rotate-180`} />
        </div>
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="outline" size="icon" onClick={() => console.log("hello")}>
              <EllipsisVerticalIcon />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent className="mr-4 mt-1">
            <DropdownMenuItem onClick={() => props.changeDialog(null, "add-course-module-attachment")}>
              Add an attachment
            </DropdownMenuItem>
            <DropdownMenuItem onClick={() => props.changeDialog(courseModule.id, "edit-course-module")}>
              Edit module
            </DropdownMenuItem>
            <DropdownMenuItem onClick={async () => await deleteCourseModule(courseModule.id).unwrap()}>
              Delete module
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
      <CollapsibleContent className="flex flex-col gap-y-2 py-2">
        {courseModule.attachments.map((attachment) => {
          return <div key={attachment.id}>{attachment.name}</div>;
        })}
      </CollapsibleContent>
    </Collapsible>
  );
};
