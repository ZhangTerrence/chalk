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
  UpdateAssignmentGroupRequest,
  UpdateAssignmentRequest,
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
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
    }),
    createAssignmentGroup: builder.mutation<Response<AssignmentGroupDTO>, CreateAssignmentGroupRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups`,
        method: "POST",
        body: body.data,
      }),
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
    }),
    createAssignment: builder.mutation<Response<AssignmentDTO>, CreateAssignmentRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups/${body.assignmentGroupId}`,
        method: "POST",
        body: body.data,
      }),
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
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
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
    }),
    updateModule: builder.mutation<Response<ModuleDTO>, UpdateModuleRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/modules/${body.moduleId}`,
        method: "PUT",
        body: body.data,
      }),
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
    }),
    updateAssignmentGroup: builder.mutation<Response<AssignmentGroupDTO>, UpdateAssignmentGroupRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups/${body.assignmentGroupId}`,
        method: "PUT",
        body: body.data,
      }),
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
    }),
    updateAssignment: builder.mutation<Response<AssignmentDTO>, UpdateAssignmentRequest>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups/${body.assignmentGroupId}/${body.assignmentId}`,
        method: "PUT",
        body: body.data,
      }),
      invalidatesTags: (_, __, requestBody) => [{ type: "course", id: requestBody.courseId }],
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
    deleteAssignmentGroup: builder.mutation<null, { courseId: number; assignmentGroupId: number }>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups/${body.assignmentGroupId}`,
        method: "DELETE",
      }),
    }),
    deleteAssignment: builder.mutation<null, { courseId: number; assignmentGroupId: number; assignmentId: number }>({
      query: (body) => ({
        url: `/courses/${body.courseId}/assignment-groups/${body.assignmentGroupId}/${body.assignmentId}`,
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
  useUpdateAssignmentGroupMutation,
  useUpdateAssignmentMutation,
  useDeleteCourseMutation,
  useDeleteModuleMutation,
  useDeleteAssignmentGroupMutation,
  useDeleteAssignmentMutation,
} = courseApi;
