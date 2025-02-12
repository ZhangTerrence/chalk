import baseApi from "@/redux/services/base.ts";

import type { Response } from "@/lib/types/_index.ts";
import type { UpdateUserRequest, UserResponse } from "@/lib/types/user.ts";

export const userApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    updateUser: builder.mutation<Response<UserResponse>, UpdateUserRequest>({
      query: (body) => ({
        url: "/users",
        method: "PUT",
        formData: true,
        body: body,
      }),
      invalidatesTags: (response) => [{ type: "user", id: response?.data.id }],
    }),
  }),
});

export const { useUpdateUserMutation } = userApi;
