import type { AttachmentDTO } from "@/lib/types/attachment.ts";
import type { OrganizationDTO } from "@/lib/types/organization.ts";
import type { RoleDTO } from "@/lib/types/role.ts";
import type { UserDTO } from "@/lib/types/user.ts";

export type CreateCourseRequest = {
  name: string;
  code?: string;
  description?: string;
  isPublic: boolean;
};

export type CreateCourseModuleRequest = {
  name: string;
  description?: string;
  courseId: number;
};

export type UpdateCourseModuleRequest = {
  id: number;
  name: string;
  description?: string;
};

export type CourseDTO = {
  id: number;
  name: string;
  code: string | null;
  description: string | null;
  image: string | null;
  isPublic: boolean;
  createdDate: string;
};

export type CourseModuleDTO = {
  id: number;
  name: string;
  description: string | null;
  relativeOrder: number;
  createdDate: string;
  attachments: AttachmentDTO[];
};

export type CourseResponse = {
  id: number;
  name: string;
  code: string | null;
  description: string | null;
  image: string | null;
  isPublic: boolean;
  createdDate: string;
  organization?: OrganizationDTO;
  users: UserDTO[];
  roles: RoleDTO[];
  modules: CourseModuleDTO[];
};
