import type { FileDTO } from "@/lib/types/file.ts";

export type CreateCourseRequest = {
  name: string;
  code?: string;
  description?: string;
  isPublic: boolean;
};

export type UpdateCourseRequest = {
  id: number;
  data: FormData;
};

export type CreateModuleRequest = {
  courseId: number;
  data: {
    name: string;
    description?: string;
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
  image: string | null;
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

export type CourseResponse = {
  id: number;
  name: string;
  code: string | null;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdDate: string;
  modules: ModuleDTO[];
};
