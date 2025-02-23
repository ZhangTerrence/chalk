import FileResponse from "@/lib/api/responses/FileResponse.ts";

type ModuleResponse = {
  id: number;
  name: string;
  description: string | null;
  relativeOrder: number;
  createdOnUtc: string;
  updatedOnUtc: string;
  files: FileResponse[];
};

export default ModuleResponse;
