type CreateRequest = {
  name: string;
  description?: string;
  permissions: number;
  relativeRank: number;
};

export default CreateRequest;
