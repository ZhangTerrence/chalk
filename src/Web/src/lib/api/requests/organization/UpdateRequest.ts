type UpdateRequest = {
  name: string;
  description?: string;
  image: File;
  isPublic: boolean;
};

export default UpdateRequest;
