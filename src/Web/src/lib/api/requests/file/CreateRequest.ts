import FileFor from "@/lib/api/enums/FileFor.ts";

type CreateRequest = {
  for: FileFor;
  containerId: number;
  name: string;
  description?: string;
  file: File;
};

export default CreateRequest;
