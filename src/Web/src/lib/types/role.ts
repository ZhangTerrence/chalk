export type RoleDTO = {
  id: number;
  name: string;
  description: string | null;
  permissions: number;
  relativeRank: number;
};
