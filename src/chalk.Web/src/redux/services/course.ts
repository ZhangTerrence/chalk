import baseApi from "@/redux/services/base.ts";

import type { Response } from "@/lib/types/_index.ts";
import type {
  CourseResponse,
  CreateCourseModuleRequest,
  CreateCourseRequest,
  UpdateCourseModuleRequest,
  UpdateCourseRequest,
} from "@/lib/types/course.ts";

export const courseApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourse: builder.query<Response<CourseResponse>, number>({
      query: (id) => ({
        url: `/courses/${id}`,
        method: "GET",
      }),
      providesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    createCourse: builder.mutation<Response<CourseResponse>, CreateCourseRequest>({
      query: (body) => ({
        url: "/courses",
        method: "POST",
        body: body,
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    updateCourse: builder.mutation<Response<CourseResponse>, UpdateCourseRequest>({
      query: (body) => ({
        url: `/courses/${body.id}`,
        method: "PUT",
        formData: true,
        body: body.data,
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    deleteCourse: builder.mutation<null, number>({
      query: (courseId) => ({
        url: `/courses/${courseId}`,
        method: "DELETE",
      }),
    }),
    createCourseModule: builder.mutation<Response<CourseResponse>, CreateCourseModuleRequest>({
      query: (body) => ({
        url: "/courses/modules",
        method: "POST",
        body: body,
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    updateCourseModule: builder.mutation<Response<CourseResponse>, UpdateCourseModuleRequest>({
      query: (body) => ({
        url: `/courses/modules/${body.id}`,
        method: "PUT",
        body: body.data,
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
    deleteCourseModule: builder.mutation<Response<CourseResponse>, number>({
      query: (courseModuleId) => ({
        url: `/courses/modules/${courseModuleId}`,
        method: "DELETE",
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
  }),
});

export const {
  useGetCourseQuery,
  useCreateCourseMutation,
  useUpdateCourseMutation,
  useDeleteCourseMutation,
  useCreateCourseModuleMutation,
  useUpdateCourseModuleMutation,
  useDeleteCourseModuleMutation,
} = courseApi;
