type UpdateRequest = {
  assignmentId: number;
  data: {
    name: string;
    description?: string;
    dueOnUtc?: Date;
  };
};

export default UpdateRequest;
