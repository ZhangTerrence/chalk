import { PlusIcon } from "lucide-react";

import NotFoundPage from "@/pages/NotFoundPage.tsx";

import { Button } from "@/components/ui/button.tsx";

import { CourseModule } from "@/components/Course/CourseModule.tsx";

import { selectCourse } from "@/redux/slices/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";

export default function CourseModulesPage() {
  const course = useTypedSelector(selectCourse);
  const dispatch = useAppDispatch();

  if (!course) {
    return <NotFoundPage />;
  }

  const modules = [...course.modules];

  return (
    <div className="h-full w-full">
      <div className="flex flex-col gap-y-4">
        {modules
          .sort((a, b) => a.relativeOrder - b.relativeOrder)
          .map((module) => {
            return <CourseModule key={module.id} courseId={course.id} data={module} />;
          })}
        <Button
          variant="outline"
          onClick={() => dispatch(setDialog({ entity: course, type: DialogType.CreateModule }))}
        >
          <span className="flex items-center gap-x-2">
            <PlusIcon />
            <p>Create module</p>
          </span>
        </Button>
      </div>
    </div>
  );
}
