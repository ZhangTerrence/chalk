import baseApi from "@/redux/services/base.ts";

import CreateRequest from "@/lib/api/requests/assignmentGroup/CreateRequest.ts";
import UpdateRequest from "@/lib/api/requests/assignmentGroup/UpdateRequest.ts";
import AssignmentGroupResponse from "@/lib/api/responses/AssignmentGroupResponse.ts";
import Response from "@/lib/api/responses/Response.ts";

export const assignmentGroupApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourseAssignmentGroups: builder.query<Response<AssignmentGroupResponse[]>, number>({
      query: (courseId) => ({
        url: `/assignment-groups/course/${courseId}`,
        method: "GET",
      }),
    }),
    create: builder.mutation<Response<AssignmentGroupResponse>, CreateRequest>({
      query: (request) => ({
        url: "/assignment-groups",
        method: "POST",
        body: request,
      }),
    }),
    update: builder.mutation<Response<AssignmentGroupResponse>, UpdateRequest>({
      query: (request) => ({
        url: `/assignment-groups/${request.assignmentGroupId}`,
        method: "PUT",
        body: request.data,
      }),
    }),
    delete: builder.mutation<null, number>({
      query: (assignmentGroupId) => ({
        url: `/assignment-groups/${assignmentGroupId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const { useGetCourseAssignmentGroupsQuery, useCreateMutation, useUpdateMutation, useDeleteMutation } =
  assignmentGroupApi;
