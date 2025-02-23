type UpdateRequest = {
  moduleId: number;
  data: {
    name: string;
    description?: string;
  };
};

export default UpdateRequest;
