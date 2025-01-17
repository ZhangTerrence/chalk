import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { LoginRequest, RegisterRequest } from "@/lib/types/account.ts";
import type { UserResponse } from "@/lib/types/user.ts";

export const accountApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    register: builder.mutation<null, RegisterRequest>({
      query: (body) => ({
        url: "/account/register",
        method: "POST",
        body: body,
      }),
    }),
    login: builder.mutation<ApiResponse<UserResponse>, LoginRequest>({
      query: (body) => ({
        url: "/account/login",
        method: "POST",
        body: body,
      }),
    }),
    refresh: builder.query<ApiResponse<UserResponse>, null>({
      query: () => ({
        url: "/account/refresh",
      }),
    }),
    logout: builder.mutation<null, null>({
      query: () => ({
        url: "/account/logout",
        method: "DELETE",
      }),
    }),
  }),
});

export const { useRegisterMutation, useLoginMutation, useLogoutMutation, useLazyRefreshQuery } = accountApi;
