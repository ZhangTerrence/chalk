import type { OrganizationDTO } from "@/lib/types/organization.ts";
import type { UserDTO } from "@/lib/types/user.ts";

export type CreateCourseRequest = {
  name: string;
  description: string | null;
  code: string | null;
  public: boolean;
};

export type CourseDTO = {
  id: number;
  name: string;
  code: string | null;
};

export type CourseRoleDTO = {
  id: number;
  name: string;
  permissions: number;
};

export type CourseResponse = {
  id: number;
  name: string;
  description: string | null;
  previewImage: string | null;
  code: string | null;
  public: boolean;
  createdDate: string;
  updatedDate: string;
  organization: OrganizationDTO | null;
  users: UserDTO[];
  roles: CourseRoleDTO[];
};
