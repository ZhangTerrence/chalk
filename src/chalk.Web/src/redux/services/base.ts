import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { AuthResponse, LoginRequest, RegisterRequest } from "@/lib/types/auth.ts";

const protocol = import.meta.env.DEV ? "http://" : "https://";
const host = import.meta.env.VITE_API_HOST;
const port = import.meta.env.VITE_API_PORT;

const baseApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: `${protocol}${host}:${port}/api`,
    credentials: "include",
  }),
  endpoints: (builder) => ({
    register: builder.mutation<ApiResponse<AuthResponse>, RegisterRequest>({
      query: (body) => ({
        url: "/register",
        method: "POST",
        body: body,
      }),
    }),
    login: builder.mutation<ApiResponse<AuthResponse>, LoginRequest>({
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
    refresh: builder.mutation<ApiResponse<AuthResponse>, null>({
      query: () => ({
        url: "/refresh",
        method: "PATCH",
      }),
    }),
  }),
});

export default baseApi;

export const { useRegisterMutation, useLoginMutation, useLogoutMutation, useRefreshMutation } = baseApi;
