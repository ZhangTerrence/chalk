import baseApi from "@/redux/services/base.ts";

import type { AuthenticationResponse, RegisterRequest } from "@/lib/types.ts";

export const authenticationApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    register: builder.mutation<AuthenticationResponse, RegisterRequest>({
      query: (body) => ({
        url: "/auth/register",
        method: "POST",
        body: body,
      }),
    }),
  }),
});

export const { useRegisterMutation } = authenticationApi;
