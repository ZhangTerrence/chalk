export type ErrorDTO = {
  title: string;
  description: string[];
};

type Response<T> = {
  errors: ErrorDTO[];
  data: T;
};

export default Response;
