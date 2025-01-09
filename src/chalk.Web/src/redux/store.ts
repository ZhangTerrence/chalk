import { type Middleware, type MiddlewareAPI, configureStore, isRejectedWithValue } from "@reduxjs/toolkit";
import type { FetchBaseQueryError } from "@reduxjs/toolkit/query";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { toast } from "sonner";

import { authApi } from "@/redux/services/auth.ts";

import type { ApiResponse } from "@/lib/types/_index.ts";
import type { AuthenticationResponse } from "@/lib/types/authentication.ts";

import authReducer from "./slices/auth.ts";
import themeReducer from "./slices/theme.ts";
import userReducer from "./slices/user.ts";

const queryErrorLogger: Middleware = (_: MiddlewareAPI) => (next) => (action) => {
  if (isRejectedWithValue(action)) {
    // @ts-ignore
    if ("endpointName" in action.meta.arg && action.meta.arg.endpointName === "refresh") {
      return next(action);
    }

    const response = action.payload as FetchBaseQueryError;
    const errors = (response.data as ApiResponse<AuthenticationResponse>).errors;
    for (const error of errors) {
      toast.error("Oops! Something went wrong.", {
        description: error.description,
      });
    }
  }

  return next(action);
};

export const store = configureStore({
  reducer: {
    [authApi.reducerPath]: authApi.reducer,
    auth: authReducer,
    user: userReducer,
    theme: themeReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(queryErrorLogger).concat(authApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector;
export const useAppDispatch = () => useDispatch<AppDispatch>();
