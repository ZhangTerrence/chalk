import { Outlet } from "react-router-dom";

import { Header } from "@/components/Header.tsx";

export default function Layout() {
  return (
    <div className="min-h-screen w-screen flex items-center justify-center relative">
      <Header />
      <Outlet />
    </div>
  );
}
