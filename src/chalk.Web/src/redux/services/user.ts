import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { UpdateUserRequest, UserResponse } from "@/lib/types/user.ts";

export const userApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    updateUser: builder.mutation<ApiResponse<UserResponse>, UpdateUserRequest>({
      query: (body) => ({
        url: "/user",
        method: "PATCH",
        body: body,
      }),
    }),
  }),
});

export const { useUpdateUserMutation } = userApi;
