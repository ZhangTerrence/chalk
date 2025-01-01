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

export type OrganizationDTO = {
  id: string;
  name: string;
};

export type CourseDTO = {
  id: string;
  name: string;
  code?: string;
};

export type ChannelDTO = {
  id: string;
  name?: string;
};

export type ApiResponse<T> = {
  errors: string[];
  data: T;
};

export type UserResponse = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
  profilePicture?: string;
  createdDate: string;
  updatedDate: string;
  organizations: OrganizationDTO[];
  courses: CourseDTO[];
  channels: ChannelDTO[];
};

export type AuthenticationResponse = {
  user: UserResponse;
  accessToken: string;
  refreshToken: string;
};
