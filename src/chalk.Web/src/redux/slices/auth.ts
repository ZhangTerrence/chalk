import { createSlice } from "@reduxjs/toolkit";

import { authApi } from "@/redux/services/auth.ts";

export type AuthState = {
  accessToken: string | null;
  refreshToken: string | null;
};

export const authSlice = createSlice({
  name: "auth",
  initialState: { accessToken: null, refreshToken: null } as AuthState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(authApi.endpoints.register.matchFulfilled, (state, { payload }) => {
      state.accessToken = payload.data.accessToken;
      state.refreshToken = payload.data.refreshToken;
    });
    builder.addMatcher(authApi.endpoints.login.matchFulfilled, (state, { payload }) => {
      state.accessToken = payload.data.accessToken;
      state.refreshToken = payload.data.refreshToken;
    });
    builder.addMatcher(authApi.endpoints.logout.matchFulfilled, (state) => {
      state.accessToken = null;
      state.refreshToken = null;
    });
    builder.addMatcher(authApi.endpoints.refresh.matchFulfilled, (state, { payload }) => {
      state.accessToken = payload.data.accessToken;
      state.refreshToken = payload.data.refreshToken;
    });
  },
});

export default authSlice.reducer;
