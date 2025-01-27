import { type PayloadAction, createSlice } from "@reduxjs/toolkit";

import { courseApi } from "@/redux/services/course.ts";
import type { RootState } from "@/redux/store.ts";

import type { CourseResponse } from "@/lib/types/course.ts";

export type CourseState = CourseResponse | null;

export const courseSlice = createSlice({
  name: "course",
  initialState: null as CourseState,
  reducers: {
    setCourse: (_, action: PayloadAction<CourseResponse>) => action.payload,
  },
  extraReducers: (builder) => {
    builder.addMatcher(courseApi.endpoints.getCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.addCourseModule.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.updateCourseModule.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.deleteCourseModule.matchFulfilled, (_, { payload }) => payload.data);
  },
});

export default courseSlice.reducer;

export const { setCourse } = courseSlice.actions;

export const selectCourse = (state: RootState) => state.course;
export const selectCourseModules = (state: RootState) => state.course?.modules;
