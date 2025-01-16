import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { UserResponse } from "@/lib/types/user.ts";

export const userApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    updateUser: builder.mutation<ApiResponse<UserResponse>, FormData>({
      query: (body) => ({
        url: "/user",
        method: "PUT",
        formData: true,
        body: body,
      }),
    }),
  }),
});

export const { useUpdateUserMutation } = userApi;
