import baseApi from "@/redux/services/base.ts";

import CreateRequest from "@/lib/api/requests/course/CreateRequest.ts";
import UpdateRequest from "@/lib/api/requests/course/UpdateRequest.ts";
import CourseResponse from "@/lib/api/responses/CourseResponse.ts";
import Response from "@/lib/api/responses/Response.ts";

export const courseApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    getCourses: builder.query<Response<CourseResponse[]>, null>({
      query: () => ({
        url: "/courses",
        method: "GET",
      }),
    }),
    getCourse: builder.query<Response<CourseResponse>, number>({
      query: (courseId) => ({
        url: `/courses/${courseId}`,
        method: "GET",
      }),
    }),
    create: builder.mutation<Response<CourseResponse>, CreateRequest>({
      query: (request) => ({
        url: "/courses",
        method: "POST",
        body: request,
      }),
    }),
    update: builder.mutation<Response<CourseResponse>, UpdateRequest>({
      query: (request) => ({
        url: `/courses/${request.courseId}`,
        method: "PUT",
        formData: true,
        body: () => {
          const body = new FormData();
          body.append("name", request.data.name);
          body.append("code", request.data.code ?? "");
          body.append("description", request.data.description ?? "");
          body.append("image", request.data.image ?? "");
          body.append("isPublic", request.data.isPublic.toString());
          return body;
        },
      }),
    }),
    delete: builder.mutation<null, number>({
      query: (courseId) => ({
        url: `/courses/${courseId}`,
        method: "DELETE",
      }),
    }),
  }),
});

export const {
  useLazyGetCoursesQuery,
  useLazyGetCourseQuery,
  useCreateMutation,
  useUpdateMutation,
  useDeleteMutation,
} = courseApi;
