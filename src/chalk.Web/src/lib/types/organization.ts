import type { ChannelDTO } from "@/lib/types/channel.ts";
import type { CourseDTO } from "@/lib/types/course.ts";
import type { RoleDTO } from "@/lib/types/role.ts";
import type { UserDTO } from "@/lib/types/user.ts";

export type OrganizationDTO = {
  id: number;
  name: string;
  description: string | null;
  profilePicture: string | null;
  isPublic: boolean;
  createdDate: string;
};

export type OrganizationResponse = {
  id: number;
  name: string;
  description: string | null;
  profilePicture: string | null;
  isPublic: boolean;
  createdDate: string;
  owner: UserDTO;
  users: UserDTO[];
  roles: RoleDTO[];
  channels: ChannelDTO[];
  courses: CourseDTO[];
};
