type UserResponse = {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
  description: string | null;
  imageUrl: string | null;
  createdOnUtc: string;
  updatedOnUtc: string;
  directMessages: PartialChannelResponse[];
  courses: PartialCourseResponse[];
  organizations: PartialOrganizationResponse[];
};

export type PartialChannelResponse = {
  id: number;
};

export type PartialCourseResponse = {
  id: number;
  name: string;
  code: string | null;
};

export type PartialOrganizationResponse = {
  id: number;
  name: string;
};

export default UserResponse;
