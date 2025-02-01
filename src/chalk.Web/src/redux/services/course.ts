import baseApi from "@/redux/services/base.ts";

import type { Response } from "@/lib/types/_index.ts";
import type {
  AssignmentDTO,
  AssignmentGroupDTO,
  CourseResponse,
  CreateAssignmentGroupRequest,
  CreateAssignmentRequest,
  CreateCourseRequest,
  CreateModuleRequest,
  ModuleDTO,
  ReorderModulesRequest,
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
    createModule: builder.mutation<Response<ModuleDTO>, CreateModuleRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/modules`,
        method: "POST",
        body: body.data,
      }),
    }),
    createAssignmentGroup: builder.mutation<Response<AssignmentGroupDTO>, CreateAssignmentGroupRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups`,
        method: "POST",
        body: body.data,
      }),
    }),
    createAssignment: builder.mutation<Response<AssignmentDTO>, CreateAssignmentRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups/${body.assignmentGroupId}`,
        method: "POST",
        body: body.data,
      }),
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
    reorderModules: builder.mutation<Response<CourseResponse>, ReorderModulesRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/modules`,
        method: "PUT",
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
    deleteCourse: builder.mutation<null, number>({
      query: (courseId) => ({
        url: `/courses/${courseId}`,
        method: "DELETE",
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
  useCreateModuleMutation,
  useCreateAssignmentGroupMutation,
  useCreateAssignmentMutation,
  useUpdateCourseMutation,
  useReorderModulesMutation,
  useUpdateModuleMutation,
  useDeleteCourseMutation,
  useDeleteModuleMutation,
} = courseApi;
