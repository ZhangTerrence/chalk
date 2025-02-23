import baseApi from "@/redux/services/base.ts";

import CreateRequest from "@/lib/api/requests/assignment/CreateRequest.ts";
import UpdateRequest from "@/lib/api/requests/assignment/UpdateRequest.ts";
import AssignmentResponse from "@/lib/api/responses/AssignmentResponse.ts";
import Response from "@/lib/api/responses/Response.ts";

export const assignmentApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    create: builder.mutation<Response<AssignmentResponse>, CreateRequest>({
      query: (request) => ({
        url: "/assignments",
        method: "POST",
        body: request,
      }),
    }),
    update: builder.mutation<Response<AssignmentResponse>, UpdateRequest>({
      query: (request) => ({
        url: `/assignments/${request.assignmentId}`,
        method: "PUT",
        body: request.data,
      }),
    }),
    delete: builder.mutation<null, number>({
      query: (assignmentId) => ({
        url: `/assignments/${assignmentId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const { useCreateMutation, useUpdateMutation, useDeleteMutation } = assignmentApi;
