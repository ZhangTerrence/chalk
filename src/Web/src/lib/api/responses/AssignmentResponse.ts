import FileResponse from "@/lib/api/responses/FileResponse.ts";

type AssignmentResponse = {
  id: number;
  name: string;
  description: string | null;
  dueOnUtc: string | null;
  createdOnUtc: string;
  updatedOnUtc: string;
  files: FileResponse[];
};

export default AssignmentResponse;
