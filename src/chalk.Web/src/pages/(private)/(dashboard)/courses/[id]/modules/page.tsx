import React from "react";

import NotFoundPage from "@/pages/notFound.tsx";

import { Button } from "@/components/ui/button.tsx";
import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";

import { CourseModule } from "@/components/Course/CourseModule.tsx";
import { AddAttachmentDialog } from "@/components/Dialogs/AddAttachmentDialog.tsx";
import { AddCourseModuleDialog } from "@/components/Dialogs/AddCourseModuleDialog.tsx";
import { UpdateCourseModuleDialog } from "@/components/Dialogs/UpdateCourseModuleDialog.tsx";

import { selectCourse } from "@/redux/slices/course.ts";
import { useTypedSelector } from "@/redux/store.ts";

export type CourseModulesDialogs = {
  open: boolean;
  id: number | null;
  type: "add-course-module" | "edit-course-module" | "add-course-module-attachment" | null;
};

export default function CourseModulesPage() {
  const course = useTypedSelector(selectCourse);

  if (!course) {
    return <NotFoundPage />;
  }

  const [dialog, setDialog] = React.useState<CourseModulesDialogs>({
    open: false,
    id: null,
    type: null,
  });

  const changeDialog = (id: number | null, type: Pick<CourseModulesDialogs, "type">["type"]) => {
    setDialog({
      open: !!type,
      id: id,
      type: type,
    });
  };

  const renderDialogContent = () => {
    switch (dialog.type) {
      case "add-course-module":
        return <AddCourseModuleDialog courseId={course.id} changeDialog={changeDialog} />;
      case "edit-course-module":
        return <UpdateCourseModuleDialog courseModuleId={dialog.id!} changeDialog={changeDialog} />;
      case "add-course-module-attachment":
        return <AddAttachmentDialog />;
      default:
        return null;
    }
  };

  const modules = [...course.modules];

  return (
    <div className="h-full w-full">
      <Dialog
        open={dialog.open}
        onOpenChange={(open) => {
          setDialog({
            open: open,
            id: dialog.id,
            type: dialog.type,
          });
        }}
      >
        <div className="flex flex-col gap-y-4">
          {modules
            .sort((a, b) => a.relativeOrder - b.relativeOrder)
            .map((module) => {
              return <CourseModule key={module.id} data={module} changeDialog={changeDialog} />;
            })}
          <Button variant="outline" onClick={() => changeDialog(null, "add-course-module")}>
            Create a new module
          </Button>
        </div>
        <DialogContent>{renderDialogContent()}</DialogContent>
      </Dialog>
    </div>
  );
}
