import { useOutletContext } from "react-router-dom";

import type { CourseResponse } from "@/lib/types/course.ts";

export default function CoursePage() {
  const course: CourseResponse = useOutletContext();

  return (
    <div className="flex h-full w-full items-center justify-center">
      <div className="flex flex-col items-center gap-y-4">
        <h1 className="text-2xl">
          <strong>
            {course.code && course.code + " - "}
            {course.name}
          </strong>
        </h1>
        <p>{course.description}</p>
      </div>
    </div>
  );
}
