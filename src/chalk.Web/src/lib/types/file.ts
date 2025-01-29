export type CreateFileRequest = {
  entityId: number;
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
