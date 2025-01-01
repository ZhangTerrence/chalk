import { authApi } from "@/redux/services/auth.ts";
import { type Middleware, type MiddlewareAPI, configureStore, isRejectedWithValue } from "@reduxjs/toolkit";
import type { FetchBaseQueryError } from "@reduxjs/toolkit/query";

import type { ApiResponse, AuthenticationResponse } from "@/lib/types.ts";

import { toast } from "@/hooks/useToast.tsx";

import authReducer from "./slices/auth.ts";

const queryErrorLogger: Middleware = (_: MiddlewareAPI) => (next) => (action) => {
  if (isRejectedWithValue(action)) {
    // @ts-ignore
    if ("endpointName" in action.meta.arg && action.meta.arg.endpointName === "refresh") {
      return next(action);
    }

    const response = action.payload as FetchBaseQueryError;
    const errors = (response.data as ApiResponse<AuthenticationResponse>).errors;
    for (const error of errors) {
      toast({
        variant: "destructive",
        title: "Oops! Something went wrong.",
        description: error.description
      });
    }
  }

  return next(action);
};

export const store = configureStore({
  reducer: {
    [authApi.reducerPath]: authApi.reducer,
    auth: authReducer
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(queryErrorLogger).concat(authApi.middleware)
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
