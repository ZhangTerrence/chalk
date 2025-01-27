import NotFoundPage from "@/pages/notFound.tsx";

import { selectCourse } from "@/redux/slices/course.ts";
import { useTypedSelector } from "@/redux/store.ts";

export default function CoursePage() {
  const course = useTypedSelector(selectCourse);

  if (!course) {
    return <NotFoundPage />;
  }

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
