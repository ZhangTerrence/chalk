import { createSlice } from "@reduxjs/toolkit";

import { courseApi } from "@/redux/services/course.ts";
import { fileApi } from "@/redux/services/file.ts";
import type { RootState } from "@/redux/store.ts";

import type { AssignmentDTO, CourseResponse, ModuleDTO } from "@/lib/types/course.ts";
import { For } from "@/lib/types/file.ts";

export type CourseState = CourseResponse | null;

export const courseSlice = createSlice({
  name: "course",
  initialState: null as CourseState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(courseApi.endpoints.getCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.createCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.createModule.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.modules = [...state.modules, payload.data];
      }
    });
    builder.addMatcher(fileApi.endpoints.createFile.matchFulfilled, (state, { payload, meta }) => {
      if (state) {
        const args = Object.fromEntries(meta.arg.originalArgs.entries());
        if (args["for"] === For.Module.toString()) {
          const data = payload.data as ModuleDTO;
          state.modules = state.modules.map((module) => {
            if (module.id === data.id) {
              return data;
            } else {
              return module;
            }
          });
        } else if (args["for"] === For.Assignment.toString()) {
          const data = payload.data as AssignmentDTO;
          state.assignmentGroups = state.assignmentGroups.map((assignmentGroup) => {
            return {
              ...assignmentGroup,
              assignments: assignmentGroup.assignments.map((assignment) => {
                if (assignment.id === Number.parseInt(args["entityId"].toString())) {
                  return data;
                } else {
                  return assignment;
                }
              }),
            };
          });
        }
      }
    });
    builder.addMatcher(courseApi.endpoints.createAssignmentGroup.matchFulfilled, (state, { payload }) => {
      if (state) {
        state.assignmentGroups = [...state.assignmentGroups, payload.data];
      }
    });
    builder.addMatcher(courseApi.endpoints.createAssignment.matchFulfilled, (state, { payload, meta }) => {
      if (state) {
        const assignmentGroups = [...state.assignmentGroups];
        const index = assignmentGroups.map((e) => e.id).indexOf(meta.arg.originalArgs.assignmentGroupId);
        assignmentGroups[index].assignments.push(payload.data);
        state.assignmentGroups = assignmentGroups;
      }
    });
    builder.addMatcher(courseApi.endpoints.updateCourse.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.reorderModules.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(courseApi.endpoints.updateModule.matchFulfilled, (state, { payload }) => {
      if (state) {
        const modules = [...state.modules];
        const index = modules.map((e) => e.id).indexOf(payload.data.id);
        modules[index] = payload.data;
        state.modules = modules;
      }
    });
    builder.addMatcher(courseApi.endpoints.updateAssignmentGroup.matchFulfilled, (state, { payload }) => {
      if (state) {
        const assignmentGroups = [...state.assignmentGroups];
        const index = assignmentGroups.map((e) => e.id).indexOf(payload.data.id);
        assignmentGroups[index] = payload.data;
        state.assignmentGroups = assignmentGroups;
      }
    });
    builder.addMatcher(courseApi.endpoints.updateAssignment.matchFulfilled, (state, { payload }) => {
      if (state) {
        const assignmentGroups = [...state.assignmentGroups];
        for (const assignmentGroup of assignmentGroups) {
          const index = assignmentGroup.assignments.map((e) => e.id).indexOf(payload.data.id);
          if (index < 0) {
            return;
          }
          assignmentGroup.assignments[index] = payload.data;
        }
        state.assignmentGroups = assignmentGroups;
      }
    });
    builder.addMatcher(fileApi.endpoints.updateFile.matchFulfilled, (state, { payload, meta }) => {
      if (state) {
        const args = Object.fromEntries(meta.arg.originalArgs.data.entries());
        if (args["for"] === For.Module.toString()) {
          const data = payload.data as ModuleDTO;
          state.modules = state.modules.map((module) => {
            if (module.id === data.id) {
              return data;
            } else {
              return module;
            }
          });
        } else if (args["for"] === For.Assignment.toString()) {
          const data = payload.data as AssignmentDTO;
          state.assignmentGroups = state.assignmentGroups.map((assignmentGroup) => {
            return {
              ...assignmentGroup,
              assignments: assignmentGroup.assignments.map((assignment) => {
                if (assignment.id === Number.parseInt(args["entityId"].toString())) {
                  return data;
                } else {
                  return assignment;
                }
              }),
            };
          });
        }
      }
    });
    builder.addMatcher(courseApi.endpoints.deleteCourse.matchFulfilled, () => null);
    builder.addMatcher(courseApi.endpoints.deleteModule.matchFulfilled, (state, { meta }) => {
      if (state) {
        state.modules = state.modules.filter((e) => e.id !== meta.arg.originalArgs.moduleId);
      }
    });
    builder.addMatcher(fileApi.endpoints.deleteFile.matchFulfilled, (state, { meta }) => {
      if (state) {
        const args = meta.arg.originalArgs;
        if (args.for === For.Module) {
          state.modules = state.modules.map((e) => {
            if (e.id === args.entityId) {
              return {
                ...e,
                files: e.files.filter((f) => f.id !== args.fileId),
              };
            } else {
              return e;
            }
          });
        } else if (args.for === For.Assignment) {
          state.assignmentGroups = state.assignmentGroups.map((assignmentGroup) => {
            return {
              ...assignmentGroup,
              assignments: assignmentGroup.assignments.map((assignment) => {
                if (assignment.id === args.entityId) {
                  return {
                    ...assignment,
                    files: assignment.files.filter((file) => file.id !== args.fileId),
                  };
                } else {
                  return assignment;
                }
              }),
            };
          });
        }
      }
    });
    builder.addMatcher(courseApi.endpoints.deleteAssignmentGroup.matchFulfilled, (state, { meta }) => {
      if (state) {
        state.assignmentGroups = state.assignmentGroups.filter((e) => e.id !== meta.arg.originalArgs.assignmentGroupId);
      }
    });
    builder.addMatcher(courseApi.endpoints.deleteAssignment.matchFulfilled, (state, { meta }) => {
      if (state) {
        const assignmentGroups = [...state.assignmentGroups];
        for (let i = 0; i < assignmentGroups.length; i++) {
          assignmentGroups[i].assignments = assignmentGroups[i].assignments.filter(
            (e) => e.id !== meta.arg.originalArgs.assignmentId,
          );
        }
      }
    });
  },
});

export default courseSlice.reducer;

export const selectCourse = (state: RootState) => state.course;
