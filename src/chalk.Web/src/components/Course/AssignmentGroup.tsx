import React from "react";

import { ChevronDownIcon, EllipsisVerticalIcon, PlusIcon } from "lucide-react";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent } from "@/components/ui/collapsible.tsx";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";

import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";
import type { AssignmentGroupDTO } from "@/lib/types/course.ts";

type AssignmentGroupProps = {
  courseId: number;
  data: AssignmentGroupDTO;
};

export const AssignmentGroup = (props: AssignmentGroupProps) => {
  const dispatch = useAppDispatch();

  const assignmentGroup = props.data;

  const [open, setOpen] = React.useState(true);

  return (
    <Collapsible open={open} onOpenChange={setOpen} className="group/collapsible">
      <div className="flex w-full items-center justify-between gap-x-2 border-b p-2">
        <div onClick={() => setOpen(!open)} className="flex grow items-center justify-center hover:cursor-pointer">
          <span className="text-lg">{assignmentGroup.name}</span>
          <ChevronDownIcon className={`ml-auto transition-transform group-data-[state=open]/collapsible:rotate-180`} />
        </div>
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="outline" size="icon">
              <EllipsisVerticalIcon />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent className="mr-4 mt-1">
            <DropdownMenuItem
              onClick={() =>
                dispatch(
                  setDialog({
                    entity: { ...assignmentGroup, courseId: props.courseId },
                    type: DialogType.CreateAssignment,
                  }),
                )
              }
            >
              <span className="flex items-center gap-x-2">
                <PlusIcon size={20} />
                <p>Add</p>
              </span>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
      {(!!assignmentGroup.description || assignmentGroup.assignments.length > 0) && (
        <CollapsibleContent className="py-4">
          <ul className="mx-3.5 flex flex-col gap-y-2 border-l px-2.5">
            {assignmentGroup.description && (
              <li className="p-2">
                <p>{assignmentGroup.description}</p>
              </li>
            )}
            {assignmentGroup.assignments.map((assignment) => {
              return (
                <li key={assignment.id} className="p-2 flex justify-between items-center">
                  <NavLink to={`/courses/${props.courseId}/assignment/${assignment.id}`} className="hover:underline">
                    {assignment.name}
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
