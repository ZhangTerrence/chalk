import { authenticationApi } from "@/redux/services/authentication.ts";
import type { RootState } from "@/redux/store.ts";
import { createSlice } from "@reduxjs/toolkit";

import type { UserResponse } from "@/lib/types.ts";

export type AuthenticationState = {
  user: UserResponse | null;
  accessToken: string | null;
  refreshToken: string | null;
};

export const authenticationSlice = createSlice({
  name: "authentication",
  initialState: { user: null, accessToken: null, refreshToken: null } as AuthenticationState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addMatcher(authenticationApi.endpoints.register.matchFulfilled, (state, { payload }) => {
      state.user = payload.user;
      state.accessToken = payload.accessToken;
      state.refreshToken = payload.refreshToken;
    });
  },
});

export default authenticationSlice.reducer;

export const selectAuthenticatedUser = (state: RootState) => state.authentication.user;
