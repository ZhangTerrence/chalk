import { createSlice } from "@reduxjs/toolkit";

import { courseApi } from "@/redux/services/course.ts";
import type { RootState } from "@/redux/store.ts";

import type { CourseResponse } from "@/lib/types/course.ts";

export type CourseState = CourseResponse | null;

export const courseSlice = createSlice({
  name: "course",
  initialState: null as CourseState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(courseApi.endpoints.getCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.createCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.updateCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.deleteCourse.matchFulfilled, () => null);
    builder.addMatcher(courseApi.endpoints.createCourseModule.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.updateCourseModule.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.deleteCourseModule.matchFulfilled, (_, { payload }) => payload.data);
  },
});

export default courseSlice.reducer;

export const selectCourse = (state: RootState) => state.course;
