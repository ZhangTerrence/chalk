import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { CourseResponse, CreateCourseModuleRequest, CreateCourseRequest } from "@/lib/types/course.ts";

export const courseApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourse: builder.query<ApiResponse<CourseResponse>, number>({
      query: (id) => ({
        url: `/courses/${id}`,
        method: "GET",
      }),
      providesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    createCourse: builder.mutation<ApiResponse<CourseResponse>, CreateCourseRequest>({
      query: (body) => ({
        url: "/courses",
        method: "POST",
        body: body,
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    addCourseModule: builder.mutation<ApiResponse<CourseResponse>, CreateCourseModuleRequest>({
      query: (body) => ({
        url: `/courses/modules`,
        method: "POST",
        body: body,
      }),
    }),
  }),
});

export const { useGetCourseQuery, useCreateCourseMutation, useAddCourseModuleMutation } = courseApi;
