import React from "react";

import { useOutletContext } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";

import { AddCourseModuleDialog } from "@/components/Dialogs/AddCourseModuleDialog.tsx";

import type { CourseResponse } from "@/lib/types/course.ts";

export type CourseModulesDialogs = {
  open: boolean;
  type: "add-course-module" | null;
};

export default function CourseModulesPage() {
  const course: CourseResponse = useOutletContext();

  const [dialog, setDialog] = React.useState<CourseModulesDialogs>({
    open: false,
    type: null,
  });

  const changeDialog = (section: Pick<CourseModulesDialogs, "type">["type"]) => {
    setDialog({
      open: !!section,
      type: section,
    });
  };

  const renderDialogContent = () => {
    switch (dialog.type) {
      case "add-course-module":
        return <AddCourseModuleDialog courseId={course.id} changeDialog={changeDialog} />;
      default:
        return null;
    }
  };

  return (
    <div className="h-full w-full">
      <Dialog
        open={dialog.open}
        onOpenChange={(open) => {
          setDialog({
            open: open,
            type: dialog.type,
          });
        }}
      >
        {course.modules.length > 0 ? (
          <div className="flex w-fit flex-col gap-y-4">
            {course.modules.map((module) => {
              return <div key={module.id}>{module.name}</div>;
            })}
          </div>
        ) : (
          <div className="flex w-fit flex-col gap-y-4">
            <span>No modules.</span>
            <Button onClick={() => changeDialog("add-course-module")}>Create a new module</Button>
          </div>
        )}
        <DialogContent>{renderDialogContent()}</DialogContent>
      </Dialog>
    </div>
  );
}
