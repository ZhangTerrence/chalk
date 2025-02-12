export type OrganizationDTO = {
  id: number;
  name: string;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdDate: string;
};

export type OrganizationResponse = {
  id: number;
  name: string;
  description: string | null;
  imageUrl: string | null;
  isPublic: boolean;
  createdDate: string;
};
