type OrganizationResponse = {
  id: number;
  name: string;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdOnUtc: string;
  updatedOnUtc: string;
};

export default OrganizationResponse;
