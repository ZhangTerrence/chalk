import baseApi from "@/redux/services/base.ts";

import type { ApiResponse, AuthenticationResponse, LoginRequest, RegisterRequest } from "@/lib/types.ts";

export const authApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    register: builder.mutation<ApiResponse<AuthenticationResponse>, RegisterRequest>({
      query: (body) => ({
        url: "/auth/register",
        method: "POST",
        body: body
      })
    }),
    login: builder.mutation<ApiResponse<AuthenticationResponse>, LoginRequest>({
      query: (body) => ({
        url: "/auth/login",
        method: "POST",
        body: body
      })
    })
  })
});

export const { useRegisterMutation, useLoginMutation } = authApi;