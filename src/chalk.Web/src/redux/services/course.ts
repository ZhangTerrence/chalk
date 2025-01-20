import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { CourseResponse, CreateCourseRequest } from "@/lib/types/course.ts";

export const courseApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createCourse: builder.mutation<ApiResponse<CourseResponse>, CreateCourseRequest>({
      query: (body) => ({
        url: "/courses",
        method: "POST",
        body: body,
      }),
    }),
  }),
});

export const { useCreateCourseMutation } = courseApi;
