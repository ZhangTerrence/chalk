import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { UserResponse } from "@/lib/types/user.ts";

export const userApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    updateUser: builder.mutation<ApiResponse<UserResponse>, FormData>({
      query: (body) => ({
        url: "/users",
        method: "PUT",
        formData: true,
        body: body,
      }),
      invalidatesTags: (response) => [{ type: "course", id: response?.data.id }],
    }),
  }),
});

export const { useUpdateUserMutation } = userApi;
