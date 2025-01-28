import type { ChannelDTO } from "@/lib/types/channel.ts";
import type { CourseDTO } from "@/lib/types/course.ts";
import type { OrganizationDTO } from "@/lib/types/organization.ts";

export type UpdateUserRequest = FormData;

export type UserDTO = {
  id: number;
  firstName: string;
  lastName: string;
  displayName: string;
  description: string | null;
  profilePicture: string | null;
  createdDate: string;
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
  directMessages: ChannelDTO[];
  courses: CourseDTO[];
  organizations: OrganizationDTO[];
};
