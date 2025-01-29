export enum For {
  Module,
  Assignment,
  Submission,
}

export type CreateFileRequest = FormData;

export type UpdateFileRequest = {
  id: number;
  data: FormData;
};

export type FileDTO = {
  id: number;
  name: string;
  description: string | null;
  fileUrl: string;
  createdDate: string;
  updatedDate: string;
};
