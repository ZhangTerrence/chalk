type UpdateRequest = {
  assignmentGroupId: number;
  data: {
    name: string;
    description?: string;
    weight: number;
  };
};

export default UpdateRequest;
