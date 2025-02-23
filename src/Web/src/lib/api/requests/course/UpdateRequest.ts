type UpdateRequest = {
  courseId: number;
  data: {
    name: string;
    code?: string;
    description?: string;
    image: File;
    isPublic: boolean;
  };
};

export default UpdateRequest;
