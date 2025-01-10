import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { AuthenticationResponse, LoginRequest, RegisterRequest } from "@/lib/types/authentication.ts";

const baseApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_SERVER_URL + "/api",
    credentials: "include",
  }),
  endpoints: (builder) => ({
    register: builder.mutation<ApiResponse<AuthenticationResponse>, RegisterRequest>({
      query: (body) => ({
        url: "/register",
        method: "POST",
        body: body,
      }),
    }),
    login: builder.mutation<ApiResponse<AuthenticationResponse>, LoginRequest>({
      query: (body) => ({
        url: "/login",
        method: "POST",
        body: body,
      }),
    }),
    logout: builder.mutation<null, null>({
      query: () => ({
        url: "/logout",
        method: "DELETE",
      }),
    }),
    refresh: builder.mutation<ApiResponse<AuthenticationResponse>, null>({
      query: () => ({
        url: "/refresh",
        method: "PATCH",
      }),
    }),
  }),
});

export default baseApi;

export const { useRegisterMutation, useLoginMutation, useLogoutMutation, useRefreshMutation } = baseApi;
