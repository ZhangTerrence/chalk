export type RoleDTO = {
  id: number;
  name: string;
  description?: string;
  permissions: number;
  relativeRank: number;
};
