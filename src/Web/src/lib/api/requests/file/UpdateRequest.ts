import FileFor from "../../enums/FileFor";

type UpdateRequest = {
  fileId: number;
  data: {
    for: FileFor;
    containerId: number;
    name: string;
    description?: string;
    fileChanged: boolean;
    newFile: File;
  };
};

export default UpdateRequest;
