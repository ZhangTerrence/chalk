import type { UserResponse } from "@/lib/types/user.ts";

export type RegisterRequest = {
  firstName: string;
  lastName: string;
  displayName: string;
  email: string;
  password: string;
};

export type LoginRequest = {
  email: string;
  password: string;
};

export type AuthenticationResponse = {
  user: UserResponse;
  accessToken: string;
  refreshToken: string;
};
