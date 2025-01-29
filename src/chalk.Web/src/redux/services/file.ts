import baseApi from "@/redux/services/base.ts";

import type { Response } from "@/lib/types/_index.ts";
import type { ModuleDTO } from "@/lib/types/course.ts";
import type { CreateFileRequest } from "@/lib/types/file.ts";

export const fileApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    createFileForModule: builder.mutation<Response<ModuleDTO>, CreateFileRequest>({
      query: (body) => ({
        url: `/files/modules/${body.entityId}`,
        method: "POST",
        formData: true,
        body: body.data,
      }),
    }),
  }),
});

export const { useCreateFileForModuleMutation } = fileApi;
