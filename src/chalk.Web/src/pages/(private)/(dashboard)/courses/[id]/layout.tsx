import { LoaderIcon } from "lucide-react";
import { Helmet } from "react-helmet-async";
import { Outlet, useParams } from "react-router-dom";

import ErrorPage from "@/pages/error.tsx";
import NotFoundPage from "@/pages/notFound.tsx";

import { SidebarTrigger, useSidebar } from "@/components/ui/sidebar.tsx";

import { useGetCourseQuery } from "@/redux/services/course.ts";

export default function CourseLayout() {
  const { id } = useParams();
  const { isMobile } = useSidebar();

  if (!id || Number.isNaN(id)) {
    return <NotFoundPage />;
  }

  const { data, isLoading } = useGetCourseQuery(Number.parseInt(id));

  if (isLoading) {
    return <LoaderIcon className="absolute inset-0 m-auto animate-spin" />;
  }

  if (!data || data.errors) {
    return <ErrorPage />;
  }

  const course = data.data;

  return (
    <>
      <Helmet>
        <title>
          {course.code && course.code + " - "} {course.name}
        </title>
      </Helmet>
      {isMobile && <SidebarTrigger className="absolute left-0 top-0 m-4" />}
      <Outlet context={course} />
    </>
  );
}
