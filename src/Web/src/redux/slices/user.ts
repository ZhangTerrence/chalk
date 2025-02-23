import { createSlice } from "@reduxjs/toolkit";

import { courseApi } from "@/redux/services/course.ts";
import { identityApi } from "@/redux/services/identity.ts";
import { userApi } from "@/redux/services/user.ts";
import type { RootState } from "@/redux/store.ts";

import CourseResponse from "@/lib/api/responses/CourseResponse.ts";
import UserResponse, { PartialCourseResponse } from "@/lib/api/responses/UserResponse.ts";

export type UserState = UserResponse | null;

function toPartialCourse(course: CourseResponse): PartialCourseResponse {
  return {
    id: course.id,
    name: course.name,
    code: course.code,
  };
}

export const userSlice = createSlice({
  name: "user",
  initialState: null as UserState,
  reducers: {},
  extraReducers: (builder) => {
    // Identity routes
    builder.addMatcher(identityApi.endpoints.login.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(identityApi.endpoints.refresh.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(identityApi.endpoints.logout.matchFulfilled, () => null);
    // User routes
    builder.addMatcher(userApi.endpoints.update.matchFulfilled, (_, { payload }) => payload.data);
    // Course routes
    builder.addMatcher(courseApi.endpoints.getCourses.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.courses = [...payload.data.map((course) => toPartialCourse(course))];
      }
    });
    builder.addMatcher(courseApi.endpoints.getCourse.matchFulfilled, (state, { payload }) => {
      if (state) {
        const courseId = payload.data.id;
        const exists = state.courses.map((course) => course.id).find((id) => id === courseId);
        if (exists) {
          state.courses = state.courses.map((course) =>
            course.id === courseId ? toPartialCourse(payload.data) : course,
          );
        } else {
          state.courses = [...state.courses, toPartialCourse(payload.data)];
        }
      }
    });
    builder.addMatcher(courseApi.endpoints.create.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.courses = [...state.courses, toPartialCourse(payload.data)];
      }
    });
    builder.addMatcher(courseApi.endpoints.update.matchFulfilled, (state, { payload }) => {
      if (state) {
        const updatedCourseId = payload.data.id;
        state.courses = state.courses.map((course) =>
          course.id === updatedCourseId ? toPartialCourse(payload.data) : course,
        );
      }
    });
    builder.addMatcher(courseApi.endpoints.delete.matchFulfilled, (state, action) => {
      if (state) {
        const deletedCourseId = action.meta.arg.originalArgs;
        state.courses = state.courses.filter((course) => course.id != deletedCourseId);
      }
    });
  },
});

export default userSlice.reducer;

export const selectUser = (state: RootState) => state.user;
export const selectUserCourses = (state: RootState) => state.user?.courses;
