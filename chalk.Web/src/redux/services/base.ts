import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

const baseApi = createApi({
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_SERVER_URL + "/api",
    credentials: "include",
  }),
  endpoints: () => ({}),
});

export default baseApi;
