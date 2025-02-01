import type { FileDTO } from "@/lib/types/file.ts";

export type CreateCourseRequest = {
  name: string;
  code?: string;
  description?: string;
  isPublic: boolean;
};

export type CreateModuleRequest = {
  courseId: number;
  data: {
    name: string;
    description?: string;
  };
};

export type CreateAssignmentGroupRequest = {
  courseId: number;
  data: {
    name: string;
    description?: string;
    weight: number;
  };
};

export type CreateAssignmentRequest = {
  courseId: number;
  assignmentGroupId: number;
  data: {
    name: string;
    description?: string;
    isOpen: boolean;
    dueDate?: Date;
    allowedAttempts?: number;
  };
};

export type UpdateCourseRequest = {
  id: number;
  data: FormData;
};

export type ReorderModulesRequest = {
  courseId: number;
  data: {
    modules: number[];
  };
};

export type UpdateModuleRequest = {
  courseId: number;
  moduleId: number;
  data: {
    name: string;
    description?: string;
  };
};

export type CourseDTO = {
  id: number;
  name: string;
  code: string | null;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdDate: string;
};

export type ModuleDTO = {
  id: number;
  name: string;
  description: string | null;
  relativeOrder: number;
  createdDate: string;
  files: FileDTO[];
};

export type AssignmentGroupDTO = {
  id: number;
  name: string;
  description: string | null;
  assignments: AssignmentDTO[];
};

export type AssignmentDTO = {
  id: number;
  name: string;
  description: string | null;
  isOpen: boolean;
  dueDate: string | null;
  allowedAttempts: number | null;
  createdDate: string;
  updatedDate: string;
};

export type CourseResponse = {
  id: number;
  name: string;
  code: string | null;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdDate: string;
  modules: ModuleDTO[];
  assignmentGroups: AssignmentGroupDTO[];
};
