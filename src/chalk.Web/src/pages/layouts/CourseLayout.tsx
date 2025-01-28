import { LoaderIcon } from "lucide-react";
import { Helmet } from "react-helmet-async";
import { Outlet, useParams } from "react-router-dom";

import ErrorPage from "@/pages/ErrorPage.tsx";

import { SidebarTrigger, useSidebar } from "@/components/ui/sidebar.tsx";

import { useGetCourseQuery } from "@/redux/services/course.ts";

export default function CourseLayout() {
  const { id } = useParams();
  const { isMobile } = useSidebar();

  const { data, isFetching } = useGetCourseQuery(Number.parseInt(id ?? ""), {
    refetchOnMountOrArgChange: true,
  });

  if (isFetching) {
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
          {course.code ? course.code + " - " : ""} {course.name}
        </title>
      </Helmet>
      {isMobile && <SidebarTrigger className="mb-4" />}
      <Outlet />
    </>
  );
}
