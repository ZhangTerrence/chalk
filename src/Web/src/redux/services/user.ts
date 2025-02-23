import baseApi from "@/redux/services/base.ts";

import UpdateRequest from "@/lib/api/requests/user/UpdateRequest.ts";
import Response from "@/lib/api/responses/Response.ts";
import UserResponse from "@/lib/api/responses/UserResponse.ts";

export const userApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getUsers: builder.query<Response<UserResponse[]>, null>({
      query: () => ({
        url: "/users",
        method: "GET",
      }),
    }),
    getUser: builder.query<Response<UserResponse>, number>({
      query: (courseId) => ({
        url: `/users/${courseId}`,
        method: "GET",
      }),
    }),
    update: builder.mutation<Response<UserResponse>, UpdateRequest>({
      query: (request) => ({
        url: `/users/${request.userId}`,
        method: "PUT",
        formData: true,
        body: () => {
          const body = new FormData();
          body.append("firstName", request.data.firstName);
          body.append("lastName", request.data.lastName);
          body.append("displayName", request.data.displayName);
          body.append("description", request.data.description ?? "");
          body.append("image", request.data.image ?? "");
          return body;
        },
      }),
    }),
  }),
});

export const { useUpdateMutation } = userApi;
