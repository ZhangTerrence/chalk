import type { ChannelDTO } from "@/lib/types/channel.ts";
import type { CourseDTO } from "@/lib/types/course.ts";
import type { OrganizationDTO } from "@/lib/types/organization.ts";

export type UpdateUserRequest = {
  firstName: string | null;
  lastName: string | null;
  displayName: string | null;
  description: string | null;
  profilePicture: string | null;
};

export type UserDTO = {
  id: number;
  firstName: string;
  lastName: string;
  displayName: string;
  joinedDate: string | null;
};

export type UserResponse = {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  displayName: string;
  description: string | null;
  profilePicture: string | null;
  createdDate: string;
  updatedDate: string;
  organizations: OrganizationDTO[];
  courses: CourseDTO[];
  channels: ChannelDTO[];
};
