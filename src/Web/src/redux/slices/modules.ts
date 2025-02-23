import { createSlice } from "@reduxjs/toolkit";

import { fileApi } from "@/redux/services/file.ts";
import { moduleApi } from "@/redux/services/module.ts";
import { RootState } from "@/redux/store.ts";

import FileFor from "@/lib/api/enums/FileFor.ts";
import ModuleResponse from "@/lib/api/responses/ModuleResponse.ts";

export type ModulesState = ModuleResponse[] | null;

export const modulesSlice = createSlice({
  name: "modules",
  initialState: null as ModulesState,
  reducers: {},
  extraReducers: (builder) => {
    // Module routes
    builder.addMatcher(moduleApi.endpoints.getCourseModules.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(moduleApi.endpoints.create.matchFulfilled, (state, { payload }) => {
      if (state) {
        state = [...state, payload.data];
      }
    });
    builder.addMatcher(moduleApi.endpoints.reorder.matchFulfilled, (_, { payload }) => payload.data);
    builder.addMatcher(moduleApi.endpoints.update.matchFulfilled, (state, { payload }) => {
      if (state) {
        const updatedModuleId = payload.data.id;
        state = state.map((module) => (module.id === updatedModuleId ? payload.data : module));
      }
    });
    builder.addMatcher(moduleApi.endpoints.delete.matchFulfilled, (state, action) => {
      if (state) {
        const deletedModuleId = action.meta.arg.originalArgs;
        state = state.filter((module) => module.id != deletedModuleId);
      }
    });
    // File routes
    builder.addMatcher(fileApi.endpoints.create.matchFulfilled, (state, action) => {
      const args = action.meta.arg.originalArgs;
      if (state && args.for === FileFor.Module) {
        const data = action.payload.data as ModuleResponse;
        state = state.map((module) => (module.id === args.containerId ? data : module));
      }
    });
    builder.addMatcher(fileApi.endpoints.update.matchFulfilled, (state, action) => {
      const args = action.meta.arg.originalArgs.data;
      if (state && args.for === FileFor.Module) {
        const data = action.payload.data as ModuleResponse;
        state = state.map((module) => (module.id === args.containerId ? data : module));
      }
    });
    builder.addMatcher(fileApi.endpoints.delete.matchFulfilled, (state, action) => {
      if (state) {
        state = state.filter((module) => module.id === action.meta.arg.originalArgs.containerId);
      }
    });
  },
});

export default modulesSlice.reducer;

export const selectModules = (state: RootState) => state.modules;
