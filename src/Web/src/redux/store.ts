import { type Middleware, type MiddlewareAPI, configureStore, isRejectedWithValue } from "@reduxjs/toolkit";
import type { FetchBaseQueryError } from "@reduxjs/toolkit/query";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { toast } from "sonner";

import baseApi from "@/redux/services/base.ts";

import Response from "@/lib/api/responses/Response.ts";

import assignmentGroupsReducer from "./slices/assignmentGroups.ts";
import courseReducer from "./slices/course.ts";
import dialogReducer from "./slices/dialog.ts";
import fileReducer from "./slices/file.ts";
import modulesReducer from "./slices/modules.ts";
import themeReducer from "./slices/theme.ts";
import userReducer from "./slices/user.ts";

const queryErrorLogger: Middleware = (_: MiddlewareAPI) => (next) => (action) => {
  if (isRejectedWithValue(action)) {
    // @ts-ignore
    if ("endpointName" in action.meta.arg && action.meta.arg.endpointName === "refresh") {
      return next(action);
    }

    const response = action.payload as FetchBaseQueryError;
    const errors = (response.data as Response<unknown>).errors;
    for (const error of errors) {
      toast.error(error.title, {
        description: error.description,
      });
    }
  }

  return next(action);
};

export const store = configureStore({
  reducer: {
    [baseApi.reducerPath]: baseApi.reducer,
    user: userReducer,
    course: courseReducer,
    modules: modulesReducer,
    assignmentGroups: assignmentGroupsReducer,
    file: fileReducer,
    dialog: dialogReducer,
    theme: themeReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(queryErrorLogger).concat(baseApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector;
export const useAppDispatch = () => useDispatch<AppDispatch>();
