import { authenticationApi } from "@/redux/services/authentication.ts";
import { configureStore } from "@reduxjs/toolkit";

import authenticationReducer from "./slices/authentication.ts";

export const store = configureStore({
  reducer: {
    [authenticationApi.reducerPath]: authenticationApi.reducer,
    authentication: authenticationReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(authenticationApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
