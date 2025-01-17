import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const protocol = import.meta.env.DEV ? "http://" : "https://";
const host = import.meta.env.VITE_SERVER_HOST;
const port = import.meta.env.VITE_SERVER_PORT;

const baseApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: `${protocol}${host}:${port}/api`,
    credentials: "include",
  }),
  endpoints: () => ({}),
});

export default baseApi;
