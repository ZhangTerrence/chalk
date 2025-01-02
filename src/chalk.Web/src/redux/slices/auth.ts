import { createSlice } from "@reduxjs/toolkit";

import { authApi } from "@/redux/services/auth.ts";
import type { RootState } from "@/redux/store.ts";

import type { UserResponse } from "@/lib/types/user.ts";

export type AuthState = {
  user: UserResponse | null;
  accessToken: string | null;
  refreshToken: string | null;
};

export const authSlice = createSlice({
  name: "auth",
  initialState: { user: null, accessToken: null, refreshToken: null } as AuthState,
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
    builder.addMatcher(authApi.endpoints.logout.matchFulfilled, (state) => {
      state.user = null;
      state.accessToken = null;
      state.refreshToken = null;
    });
    builder.addMatcher(authApi.endpoints.refresh.matchFulfilled, (state, { payload }) => {
      state.user = payload.data.user;
      state.accessToken = payload.data.accessToken;
      state.refreshToken = payload.data.refreshToken;
    });
  },
});

export default authSlice.reducer;

export const selectAuthenticatedUser = (state: RootState) => state.auth.user;
