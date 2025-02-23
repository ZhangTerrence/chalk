import baseApi from "@/redux/services/base.ts";

import CreateRequest from "@/lib/api/requests/module/CreateRequest.ts";
import ReorderRequest from "@/lib/api/requests/module/ReorderRequest.ts";
import UpdateRequest from "@/lib/api/requests/module/UpdateRequest.ts";
import ModuleResponse from "@/lib/api/responses/ModuleResponse.ts";
import Response from "@/lib/api/responses/Response.ts";

export const moduleApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourseModules: builder.query<Response<ModuleResponse[]>, null>({
      query: () => ({
        url: "/modules",
        method: "GET",
      }),
    }),
    create: builder.mutation<Response<ModuleResponse>, CreateRequest>({
      query: (request) => ({
        url: "/modules",
        method: "POST",
        body: request,
      }),
    }),
    reorder: builder.mutation<Response<ModuleResponse[]>, ReorderRequest>({
      query: (request) => ({
        url: "/modules/reorder",
        method: "PATCH",
        body: request,
      }),
    }),
    update: builder.mutation<Response<ModuleResponse>, UpdateRequest>({
      query: (request) => ({
        url: `/modules/${request.moduleId}`,
        method: "PUT",
        body: request.data,
      }),
    }),
    delete: builder.mutation<null, number>({
      query: (moduleId) => ({
        url: `/modules/${moduleId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const {
  useLazyGetCourseModulesQuery,
  useCreateMutation,
  useReorderMutation,
  useUpdateMutation,
  useDeleteMutation,
} = moduleApi;
