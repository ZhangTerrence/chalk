import { EditIcon, PlusIcon, TrashIcon } from "lucide-react";
import { NavLink, useParams } from "react-router-dom";

import NotFoundPage from "@/pages/NotFoundPage.tsx";

import { Button } from "@/components/ui/button.tsx";

import { useDeleteFileMutation } from "@/redux/services/file.ts";
import { selectCourse } from "@/redux/slices/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";
import { For } from "@/lib/types/file.ts";

export default function CourseAssignmentPage() {
  const course = useTypedSelector(selectCourse)!;
  const dispatch = useAppDispatch();
  const { courseId, assignmentId } = useParams();
  const [deleteFile] = useDeleteFileMutation();

  const assignment = course.assignmentGroups
    .flatMap((e) => e.assignments)
    .find((e) => e.id === Number.parseInt(assignmentId ?? ""));

  if (!assignment) {
    return <NotFoundPage />;
  }

  return (
    <div className="flex h-full w-full flex-col gap-y-2 relative">
      <div className="flex justify-between">
        <h1 className="text-xl">
          <strong>{assignment.name}</strong>
        </h1>
        <Button>New Attempt</Button>
      </div>
      <div className="flex space-x-2">
        {assignment.dueDate && (
          <div>
            <strong>Due Date:</strong> {assignment.dueDate}
          </div>
        )}
        {assignment.allowedAttempts && (
          <div>
            <strong>Allowed Attempts:</strong> {assignment.allowedAttempts}
          </div>
        )}
      </div>
      <p>{assignment.description}</p>
      <div>
        {assignment.files.map((file) => {
          return (
            <li key={file.id} className="p-2 flex justify-between items-center">
              <NavLink to={`/courses/${courseId}/file/${file.id}`} state={file} className="hover:underline">
                {file.name}
              </NavLink>
              <div className="flex gap-x-2">
                <Button
                  size="icon"
                  onClick={() =>
                    dispatch(
                      setDialog({
                        entity: { ...file, for: For.Assignment, entityId: assignment.id },
                        type: DialogType.UpdateFile,
                      }),
                    )
                  }
                >
                  <EditIcon />
                </Button>
                <Button
                  variant="destructive"
                  size="icon"
                  onClick={async () =>
                    await deleteFile({
                      for: For.Assignment,
                      entityId: assignment.id,
                      fileId: file.id,
                    }).unwrap()
                  }
                >
                  <TrashIcon />
                </Button>
              </div>
            </li>
          );
        })}
      </div>
      <Button
        size="icon"
        onClick={() =>
          dispatch(
            setDialog({
              entity: { ...assignment, for: For.Assignment },
              type: DialogType.CreateFile,
            }),
          )
        }
        className="absolute bottom-0 right-0"
      >
        <PlusIcon />
      </Button>
    </div>
  );
}
