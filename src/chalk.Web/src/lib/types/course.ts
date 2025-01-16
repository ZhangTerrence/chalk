import type { OrganizationDTO } from "@/lib/types/organization.ts";
import type { RoleDTO } from "@/lib/types/role.ts";
import type { UserDTO } from "@/lib/types/user.ts";

export type CreateCourseRequest = {
  name: string;
  code?: string;
  description?: string;
  isPublic: boolean;
};

export type CourseDTO = {
  id: number;
  name: string;
  code?: string;
  description?: string;
  image?: string;
  createdDate: string;
};

export type CourseResponse = {
  id: number;
  name: string;
  code?: string;
  description?: string;
  image?: string;
  isPublic: boolean;
  createdDate: string;
  organization?: OrganizationDTO;
  users: UserDTO[];
  roles: RoleDTO[];
};
