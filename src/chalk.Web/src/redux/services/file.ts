import baseApi from "@/redux/services/base.ts";

import type { Response } from "@/lib/types/_index.ts";
import type { ModuleDTO } from "@/lib/types/course.ts";
import { type CreateFileRequest, For, type UpdateFileRequest } from "@/lib/types/file.ts";

export const fileApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createFile: builder.mutation<Response<ModuleDTO | {}>, CreateFileRequest>({
      query: (body) => ({
        url: "/files",
        method: "POST",
        formData: true,
        body: body,
      }),
    }),
    updateFile: builder.mutation<Response<ModuleDTO | {}>, UpdateFileRequest>({
      query: (body) => ({
        url: `/files/${body.id}`,
        method: "PUT",
        formData: true,
        body: body.data,
      }),
    }),
    deleteFile: builder.mutation<null, { for: For; entityId: number; fileId: number }>({
      query: (body) => ({
        url: `/files/${body.fileId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const { useCreateFileMutation, useUpdateFileMutation, useDeleteFileMutation } = fileApi;
