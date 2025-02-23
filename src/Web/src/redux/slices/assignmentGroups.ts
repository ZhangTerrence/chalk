import { createSlice } from "@reduxjs/toolkit";

import { assignmentApi } from "@/redux/services/assignment.ts";
import { assignmentGroupApi } from "@/redux/services/assignmentGroup.ts";
import { fileApi } from "@/redux/services/file.ts";

import FileFor from "@/lib/api/enums/FileFor.ts";
import AssignmentGroupResponse from "@/lib/api/responses/AssignmentGroupResponse.ts";
import AssignmentResponse from "@/lib/api/responses/AssignmentResponse.ts";

export type AssignmentGroupsState = AssignmentGroupResponse[] | null;

export const assignmentGroupsSlice = createSlice({
  name: "assignmentGroups",
  initialState: null as AssignmentGroupsState,
  reducers: {},
  extraReducers: (builder) => {
    // Assignment group routes
    builder.addMatcher(
      assignmentGroupApi.endpoints.getCourseAssignmentGroups.matchFulfilled,
      (_, { payload }) => payload.data,
    );
    builder.addMatcher(assignmentGroupApi.endpoints.create.matchFulfilled, (state, { payload }) => {
      if (state) {
        state = [...state, payload.data];
      }
    });
    builder.addMatcher(assignmentGroupApi.endpoints.update.matchFulfilled, (state, { payload }) => {
      if (state) {
        const updatedAssignmentGroupId = payload.data.id;
        state = state.map((assignmentGroup) =>
          assignmentGroup.id === updatedAssignmentGroupId ? payload.data : assignmentGroup,
        );
      }
    });
    builder.addMatcher(assignmentGroupApi.endpoints.delete.matchFulfilled, (state, action) => {
      if (state) {
        const deletedAssignmentGroupId = action.meta.arg.originalArgs;
        state = state.filter((assignmentGroup) => assignmentGroup.id != deletedAssignmentGroupId);
      }
    });
    // Assignment routes
    builder.addMatcher(assignmentApi.endpoints.create.matchFulfilled, (state, action) => {
      if (state) {
        state = state.map((assignmentGroup) => {
          return {
            ...assignmentGroup,
            assignments:
              assignmentGroup.id === action.meta.arg.originalArgs.assignmentGroupId
                ? [...assignmentGroup.assignments, action.payload.data]
                : assignmentGroup.assignments,
          };
        });
      }
    });
    builder.addMatcher(assignmentApi.endpoints.update.matchFulfilled, (state, action) => {
      if (state) {
        state = state.map((assignmentGroup) => {
          return {
            ...assignmentGroup,
            assignments: assignmentGroup.assignments.map((assignment) =>
              assignment.id === action.meta.arg.originalArgs.assignmentId ? action.payload.data : assignment,
            ),
          };
        });
      }
    });
    builder.addMatcher(assignmentApi.endpoints.delete.matchFulfilled, (state, action) => {
      if (state) {
        state = state.map((assignmentGroup) => {
          return {
            ...assignmentGroup,
            assignments: assignmentGroup.assignments.filter(
              (assignment) => assignment.id !== action.meta.arg.originalArgs,
            ),
          };
        });
      }
    });
    // File routes
    builder.addMatcher(fileApi.endpoints.create.matchFulfilled, (state, action) => {
      const args = action.meta.arg.originalArgs;
      if (state && args.for === FileFor.Assignment) {
        const data = action.payload.data as AssignmentResponse;
        state = state.map((assignmentGroup) => {
          return {
            ...assignmentGroup,
            assignments: assignmentGroup.assignments.map((assignment) =>
              assignment.id === args.containerId ? data : assignment,
            ),
          };
        });
      }
    });
    builder.addMatcher(fileApi.endpoints.update.matchFulfilled, (state, action) => {
      const args = action.meta.arg.originalArgs.data;
      if (state && args.for === FileFor.Assignment) {
        const data = action.payload.data as AssignmentResponse;
        state = state.map((assignmentGroup) => {
          return {
            ...assignmentGroup,
            assignments: assignmentGroup.assignments.map((assignment) =>
              assignment.id === args.containerId ? data : assignment,
            ),
          };
        });
      }
    });
    builder.addMatcher(fileApi.endpoints.delete.matchFulfilled, (state, action) => {
      if (state) {
        state = state.map((assignmentGroup) => {
          return {
            ...assignmentGroup,
            assignments: assignmentGroup.assignments.filter(
              (assignment) => assignment.id !== action.meta.arg.originalArgs.containerId,
            ),
          };
        });
      }
    });
  },
});

export default assignmentGroupsSlice.reducer;
