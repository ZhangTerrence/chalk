import { createSlice } from "@reduxjs/toolkit";

import { courseApi } from "@/redux/services/course.ts";
import type { RootState } from "@/redux/store.ts";

import CourseResponse from "@/lib/api/responses/CourseResponse.ts";

export type CourseState = CourseResponse | null;

export const courseSlice = createSlice({
  name: "course",
  initialState: null as CourseState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(courseApi.endpoints.getCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.create.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.update.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.delete.matchFulfilled, () => null);
  },
});

export default courseSlice.reducer;

export const selectCourse = (state: RootState) => state.course;
