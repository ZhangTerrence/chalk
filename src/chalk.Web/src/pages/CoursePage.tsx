import { EditIcon, TrashIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

import NotFoundPage from "@/pages/NotFoundPage.tsx";

import { Button } from "@/components/ui/button.tsx";

import { useDeleteCourseMutation } from "@/redux/services/course.ts";
import { selectCourse } from "@/redux/slices/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";

export default function CoursePage() {
  const course = useTypedSelector(selectCourse);
  const dispatch = useAppDispatch();
  const [deleteCourse] = useDeleteCourseMutation();
  const navigate = useNavigate();

  if (!course) {
    return <NotFoundPage />;
  }

  return (
    <div className="flex h-full w-full items-center justify-center relative">
      <Button
        onClick={() => dispatch(setDialog({ entity: course, type: DialogType.UpdateCourse }))}
        className="absolute top-0 right-0"
      >
        <EditIcon />
        <p>Edit</p>
      </Button>
      <Button
        variant="destructive"
        onClick={async () => {
          await deleteCourse(course.id).unwrap();
          toast.success("Successfully deleted course.");
          navigate("/dashboard");
        }}
        className="absolute bottom-0 right-0"
      >
        <TrashIcon />
        <p>Delete</p>
      </Button>
      <div className="flex flex-col items-center gap-y-4">
        <h1 className="text-2xl">
          <strong>
            {course.code ? course.code + " - " : ""} {course.name}
          </strong>
        </h1>
        <p>{course.description}</p>
      </div>
    </div>
  );
}
