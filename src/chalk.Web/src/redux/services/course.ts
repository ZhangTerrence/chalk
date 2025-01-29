import baseApi from "@/redux/services/base.ts";

import type { Response } from "@/lib/types/_index.ts";
import type {
  CourseResponse,
  CreateCourseRequest,
  CreateModuleRequest,
  ModuleDTO,
  UpdateCourseRequest,
  UpdateModuleRequest,
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
    createModule: builder.mutation<Response<ModuleDTO>, CreateModuleRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/modules`,
        method: "POST",
        body: body.data,
      }),
    }),
    updateModule: builder.mutation<Response<ModuleDTO>, UpdateModuleRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/modules/${body.moduleId}`,
        method: "PUT",
        body: body.data,
      }),
    }),
    deleteModule: builder.mutation<null, { courseId: number; moduleId: number }>({
      query: (body) => ({
        url: `/courses/${body.courseId}/modules/${body.moduleId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const {
  useGetCourseQuery,
  useCreateCourseMutation,
  useUpdateCourseMutation,
  useDeleteCourseMutation,
  useCreateModuleMutation,
  useUpdateModuleMutation,
  useDeleteModuleMutation,
} = courseApi;
