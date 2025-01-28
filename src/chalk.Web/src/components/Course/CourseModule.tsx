import React from "react";

import { AlignJustifyIcon, ChevronDownIcon, EditIcon, EllipsisVerticalIcon, LinkIcon, TrashIcon } from "lucide-react";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent } from "@/components/ui/collapsible.tsx";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";

import { useDeleteCourseModuleMutation } from "@/redux/services/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";
import type { CourseModuleDTO } from "@/lib/types/course.ts";

type CourseModuleProps = {
  data: CourseModuleDTO;
};

export const CourseModule = (props: CourseModuleProps) => {
  const dispatch = useAppDispatch();
  const [deleteCourseModule] = useDeleteCourseModuleMutation();

  const courseModule = props.data;

  const [open, setOpen] = React.useState(true);

  return (
    <Collapsible open={open} onOpenChange={setOpen} className="group/collapsible">
      <div className="flex w-full items-center justify-between gap-x-2 border-b p-2">
        <div onClick={() => setOpen(!open)} className="flex grow items-center justify-center hover:cursor-pointer">
          <div className="flex items-center gap-x-2">
            <AlignJustifyIcon />
            <span className="text-lg">{courseModule.name}</span>
          </div>
          <ChevronDownIcon className={`ml-auto transition-transform group-data-[state=open]/collapsible:rotate-180`} />
        </div>
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="outline" size="icon">
              <EllipsisVerticalIcon />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent className="mr-4 mt-1">
            <DropdownMenuItem onClick={() => dispatch(setDialog({ entity: null, type: DialogType.CreateAttachment }))}>
              <span className="flex items-center gap-x-2">
                <LinkIcon />
                <p>Attach</p>
              </span>
            </DropdownMenuItem>
            <DropdownMenuItem
              onClick={() => dispatch(setDialog({ entity: courseModule, type: DialogType.UpdateCourseModule }))}
            >
              <span className="flex items-center gap-x-2">
                <EditIcon />
                <p>Edit</p>
              </span>
            </DropdownMenuItem>
            <DropdownMenuItem onClick={async () => await deleteCourseModule(courseModule.id).unwrap()}>
              <span className="flex items-center gap-x-2">
                <TrashIcon />
                <p>Delete</p>
              </span>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
      {(!!courseModule.description || courseModule.attachments.length > 0) && (
        <CollapsibleContent className="py-4">
          <ul className="mx-3.5 flex flex-col gap-y-2 border-l px-2.5">
            {courseModule.description && (
              <li className="p-2">
                <p>{courseModule.description}</p>
              </li>
            )}
            {courseModule.attachments.map((attachment) => {
              return (
                <li key={attachment.id} className="p-2">
                  {attachment.name}
                </li>
              );
            })}
          </ul>
        </CollapsibleContent>
      )}
    </Collapsible>
  );
};
