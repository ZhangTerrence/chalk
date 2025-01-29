import React from "react";

import { AlignJustifyIcon, ChevronDownIcon, EditIcon, EllipsisVerticalIcon, LinkIcon, TrashIcon } from "lucide-react";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent } from "@/components/ui/collapsible.tsx";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";

import { useDeleteModuleMutation } from "@/redux/services/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";
import type { ModuleDTO } from "@/lib/types/course.ts";

type CourseModuleProps = {
  courseId: number;
  data: ModuleDTO;
};

export const CourseModule = (props: CourseModuleProps) => {
  const dispatch = useAppDispatch();
  const [deleteModule] = useDeleteModuleMutation();

  const module = props.data;

  const [open, setOpen] = React.useState(true);

  return (
    <Collapsible open={open} onOpenChange={setOpen} className="group/collapsible">
      <div className="flex w-full items-center justify-between gap-x-2 border-b p-2">
        <div onClick={() => setOpen(!open)} className="flex grow items-center justify-center hover:cursor-pointer">
          <div className="flex items-center gap-x-2">
            <AlignJustifyIcon />
            <span className="text-lg">{module.name}</span>
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
            <DropdownMenuItem onClick={() => dispatch(setDialog({ entity: module, type: DialogType.CreateFile }))}>
              <span className="flex items-center gap-x-2">
                <LinkIcon />
                <p>Attach</p>
              </span>
            </DropdownMenuItem>
            <DropdownMenuItem
              onClick={() =>
                dispatch(
                  setDialog({
                    entity: { ...module, courseId: props.courseId },
                    type: DialogType.UpdateModule,
                  }),
                )
              }
            >
              <span className="flex items-center gap-x-2">
                <EditIcon />
                <p>Edit</p>
              </span>
            </DropdownMenuItem>
            <DropdownMenuItem
              onClick={async () => await deleteModule({ courseId: props.courseId, moduleId: module.id }).unwrap()}
            >
              <span className="flex items-center gap-x-2">
                <TrashIcon />
                <p>Delete</p>
              </span>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
      {(!!module.description || module.files.length > 0) && (
        <CollapsibleContent className="py-4">
          <ul className="mx-3.5 flex flex-col gap-y-2 border-l px-2.5">
            {module.description && (
              <li className="p-2">
                <p>{module.description}</p>
              </li>
            )}
            {module.files.map((file) => {
              return (
                <li key={file.id} className="p-2">
                  <NavLink to={`/courses/${props.courseId}/file/${file.id}`} state={file} className="hover:underline">
                    {file.name}
                  </NavLink>
                </li>
              );
            })}
          </ul>
        </CollapsibleContent>
      )}
    </Collapsible>
  );
};
