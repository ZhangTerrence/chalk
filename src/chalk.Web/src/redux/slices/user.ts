import { createSlice } from "@reduxjs/toolkit";

import baseApi from "@/redux/services/base.ts";
import { courseApi } from "@/redux/services/course.ts";
import type { RootState } from "@/redux/store.ts";

import type { CourseDTO } from "@/lib/types/course.ts";
import type { UserResponse } from "@/lib/types/user.ts";

export type UserState =
  | (UserResponse & {
      courses: CourseDTO[];
    })
  | null;

export const userSlice = createSlice({
  name: "user",
  initialState: null as UserState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(baseApi.endpoints.register.matchFulfilled, (_, { payload }) => payload.data.user);
    builder.addMatcher(baseApi.endpoints.login.matchFulfilled, (_, { payload }) => payload.data.user);
    builder.addMatcher(baseApi.endpoints.logout.matchFulfilled, () => null);
    builder.addMatcher(baseApi.endpoints.refresh.matchFulfilled, (_, { payload }) => payload.data.user);
    builder.addMatcher(courseApi.endpoints.createCourse.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.courses.push({
          id: payload.data.id,
          name: payload.data.name,
          code: payload.data.code,
        });
      }
    });
  },
});

export default userSlice.reducer;

export const selectUser = (state: RootState) => state.user;
export const selectUserCourses = (state: RootState) => state.user?.courses;
