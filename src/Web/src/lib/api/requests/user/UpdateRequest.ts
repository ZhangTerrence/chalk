type UpdateRequest = {
  userId: number;
  data: {
    firstName: string;
    lastName: string;
    displayName: string;
    description?: string;
    image: File;
  };
};

export default UpdateRequest;
