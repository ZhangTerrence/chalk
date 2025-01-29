import { createSlice } from "@reduxjs/toolkit";

import { courseApi } from "@/redux/services/course.ts";
import { fileApi } from "@/redux/services/file.ts";
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
    builder.addMatcher(courseApi.endpoints.createModule.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.modules = [...state.modules, payload.data];
      }
    });
    builder.addMatcher(courseApi.endpoints.updateModule.matchFulfilled, (state, { payload }) => {
      if (state) {
        const modules = [...state.modules];
        const index = modules.map((e) => e.id).indexOf(payload.data.id);
        modules[index] = payload.data;
        state.modules = modules;
      }
    });
    builder.addMatcher(courseApi.endpoints.deleteModule.matchFulfilled, (state, { meta }) => {
      if (state) {
        state.modules = state.modules.filter((e) => e.id !== meta.arg.originalArgs.moduleId);
      }
    });
    builder.addMatcher(fileApi.endpoints.createFileForModule.matchFulfilled, (state, { payload }) => {
      if (state) {
        const modules = [...state.modules];
        const index = modules.map((e) => e.id).indexOf(payload.data.id);
        modules[index] = payload.data;
        state.modules = modules;
      }
    });
  },
});

export default courseSlice.reducer;

export const selectCourse = (state: RootState) => state.course;
