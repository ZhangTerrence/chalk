import { Outlet } from "react-router-dom";

import { Header } from "@/components/Header.tsx";

export default function RootLayout() {
  return (
    <div className="relative flex min-h-screen w-screen items-center justify-center">
      <Header />
      <Outlet />
    </div>
  );
}
