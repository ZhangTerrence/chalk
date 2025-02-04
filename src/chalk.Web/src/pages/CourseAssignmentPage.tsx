import { PlusIcon } from "lucide-react";

import NotFoundPage from "@/pages/NotFoundPage.tsx";

import { Button } from "@/components/ui/button.tsx";

import { AssignmentGroup } from "@/components/Course/AssignmentGroup.tsx";

import { selectCourse } from "@/redux/slices/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";

export default function CourseAssignmentPage() {
  const course = useTypedSelector(selectCourse);
  const dispatch = useAppDispatch();

  if (!course) {
    return <NotFoundPage />;
  }

  const assignmentGroups = [...course.assignmentGroups];

  return (
    <div className="flex h-full w-full flex-col gap-y-4 justify-between">
      <div className="flex flex-col gap-y-4">
        {assignmentGroups
          .sort((a, b) => b.weight - a.weight)
          .map((assignmentGroup) => {
            return <AssignmentGroup key={assignmentGroup.id} courseId={course.id} data={assignmentGroup} />;
          })}
      </div>
      <Button
        variant="outline"
        onClick={() => dispatch(setDialog({ entity: course, type: DialogType.CreateAssignmentGroup }))}
      >
        <span className="flex items-center gap-x-2">
          <PlusIcon />
          <p>Create assignment group</p>
        </span>
      </Button>
    </div>
  );
}
