type CreateRequest = {
  assignmentGroupId: number;
  name: string;
  description?: string;
  dueOnUtc?: Date;
};

export default CreateRequest;
