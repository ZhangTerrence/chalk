export type ErrorDTO = {
  description: string;
};

export type Response<T> = {
  errors: ErrorDTO[];
  data: T;
};
