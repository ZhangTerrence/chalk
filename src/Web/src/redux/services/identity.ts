import baseApi from "@/redux/services/base.ts";

import LoginRequest from "@/lib/api/requests/identity/LoginRequest";
import RegisterRequest from "@/lib/api/requests/identity/RegisterRequest";
import Response from "@/lib/api/responses/Response.ts";
import UserResponse from "@/lib/api/responses/UserResponse.ts";

export const identityApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    register: builder.mutation<null, RegisterRequest>({
      query: (request) => ({
        url: "/register",
        method: "POST",
        body: request,
      }),
    }),
    login: builder.mutation<Response<UserResponse>, LoginRequest>({
      query: (request) => ({
        url: "/login",
        method: "POST",
        body: request,
      }),
      invalidatesTags: (response) => [{ type: "user", id: response?.data.id }],
    }),
    refresh: builder.query<Response<UserResponse>, null>({
      query: () => ({
        url: "/refresh",
      }),
      providesTags: (response) => [{ type: "user", id: response?.data.id }],
    }),
    logout: builder.mutation<null, null>({
      query: () => ({
        url: "/logout",
        method: "DELETE",
      }),
    }),
  }),
});

export const { useRegisterMutation, useLoginMutation, useLogoutMutation, useLazyRefreshQuery } = identityApi;
