import baseApi from "@/redux/services/base.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { AuthenticationResponse, LoginRequest, RegisterRequest } from "@/lib/types/authentication.ts";

export const authApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    register: builder.mutation<ApiResponse<AuthenticationResponse>, RegisterRequest>({
      query: (body) => ({
        url: "/auth/register",
        method: "POST",
        body: body,
      }),
    }),
    login: builder.mutation<ApiResponse<AuthenticationResponse>, LoginRequest>({
      query: (body) => ({
        url: "/auth/login",
        method: "POST",
        body: body,
      }),
    }),
    logout: builder.mutation<null, null>({
      query: () => ({
        url: "/auth/logout",
        method: "DELETE",
      }),
    }),
    refresh: builder.mutation<ApiResponse<AuthenticationResponse>, null>({
      query: () => ({
        url: "/auth/refresh",
        method: "PATCH",
      }),
    }),
  }),
});

export const { useRegisterMutation, useLoginMutation, useLogoutMutation, useRefreshMutation } = authApi;
