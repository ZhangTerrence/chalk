type FileResponse = {
  id: number;
  name: string;
  description: string | null;
  fileUrl: string;
  createdOnUtc: string;
  updatedOnUtc: string;
};

export default FileResponse;
