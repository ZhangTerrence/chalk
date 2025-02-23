import baseApi from "@/redux/services/base.ts";

import FileFor from "@/lib/api/enums/FileFor.ts";
import CreateRequest from "@/lib/api/requests/file/CreateRequest.ts";
import UpdateRequest from "@/lib/api/requests/file/UpdateRequest.ts";
import AssignmentResponse from "@/lib/api/responses/AssignmentResponse.ts";
import ModuleResponse from "@/lib/api/responses/ModuleResponse.ts";
import Response from "@/lib/api/responses/Response.ts";

export const fileApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    create: builder.mutation<Response<ModuleResponse | AssignmentResponse>, CreateRequest>({
      query: (request) => ({
        url: "/files",
        method: "POST",
        formData: true,
        body: () => {
          const body = new FormData();
          body.append("for", request.for.toString());
          body.append("containerId", request.containerId.toString());
          body.append("name", request.name);
          body.append("description", request.description ?? "");
          body.append("file", request.file);
          return body;
        },
      }),
    }),
    update: builder.mutation<Response<ModuleResponse | AssignmentResponse>, UpdateRequest>({
      query: (request) => ({
        url: `/files/${request.fileId}`,
        method: "PUT",
        formData: true,
        body: () => {
          const body = new FormData();
          body.append("for", request.data.for.toString());
          body.append("containerId", request.data.containerId.toString());
          body.append("name", request.data.name);
          body.append("description", request.data.description ?? "");
          body.append("fileChanged", request.data.fileChanged.toString());
          body.append("newFile", request.data.newFile);
          return body;
        },
      }),
    }),
    delete: builder.mutation<null, { for: FileFor; containerId: number; fileId: number }>({
      query: (request) => ({
        url: `/files/${request.fileId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const { useCreateMutation, useUpdateMutation, useDeleteMutation } = fileApi;
