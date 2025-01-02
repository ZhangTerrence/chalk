export type ErrorDTO = {
  description: string;
};

export type ApiResponse<T> = {
  errors: ErrorDTO[];
  data: T;
};
