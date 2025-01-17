import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const protocol = import.meta.env.DEV ? "http://" : "https://";
const host = import.meta.env.VITE_API_HOST;
const port = import.meta.env.VITE_API_PORT;

const baseApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: `${protocol}${host}:${port}/api`,
    credentials: "include",
  }),
  endpoints: () => ({}),
});

export default baseApi;
