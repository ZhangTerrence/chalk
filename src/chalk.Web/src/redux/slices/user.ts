import { createSlice } from "@reduxjs/toolkit";

import { accountApi } from "@/redux/services/account.ts";
import { courseApi } from "@/redux/services/course.ts";
import { userApi } from "@/redux/services/user.ts";
import type { RootState } from "@/redux/store.ts";

import type { UserResponse } from "@/lib/types/user.ts";

export type UserState = UserResponse | null;

export const userSlice = createSlice({
  name: "user",
  initialState: null as UserState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(accountApi.endpoints.refresh.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(accountApi.endpoints.login.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(accountApi.endpoints.logout.matchFulfilled, () => null);
    builder.addMatcher(userApi.endpoints.updateUser.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.createCourse.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.courses.push({
          id: payload.data.id,
          name: payload.data.name,
          code: payload.data.code,
          description: payload.data.description,
          image: payload.data.image,
          isPublic: payload.data.isPublic,
          createdDate: payload.data.createdDate,
        });
      }
    });
  },
});

export default userSlice.reducer;

export const selectUser = (state: RootState) => state.user;
export const selectUserCourses = (state: RootState) => state.user?.courses;
export const selectUserOrganizations = (state: RootState) => state.user?.organizations;
