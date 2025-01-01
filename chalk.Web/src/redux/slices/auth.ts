import { authApi } from "@/redux/services/auth.ts";
import type { RootState } from "@/redux/store.ts";
import { createSlice } from "@reduxjs/toolkit";

import type { UserResponse } from "@/lib/types.ts";

export type authState = {
  user: UserResponse | null;
  accessToken: string | null;
  refreshToken: string | null;
};

export const authSlice = createSlice({
  name: "auth",
  initialState: { user: null, accessToken: null, refreshToken: null } as authState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(authApi.endpoints.register.matchFulfilled, (state, { payload }) => {
      state.user = payload.data.user;
      state.accessToken = payload.data.accessToken;
      state.refreshToken = payload.data.refreshToken;
    });
    builder.addMatcher(authApi.endpoints.login.matchFulfilled, (state, { payload }) => {
      state.user = payload.data.user;
      state.accessToken = payload.data.accessToken;
      state.refreshToken = payload.data.refreshToken;
    });
  },
});

export default authSlice.reducer;

export const selectAuthenticatedUser = (state: RootState) => state.auth.user;
