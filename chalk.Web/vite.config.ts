import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";
import * as path from "node:path";

// https://vite.dev/config/
export default defineConfig({
  base: "/",
  plugins: [react()],
  server: {
    port: 3000,
    strictPort: true,
    host: true,
    origin: "http://0.0.0.0:8080",
  },
  preview: {
    port: 3000,
    strictPort: true,
  },
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
});
