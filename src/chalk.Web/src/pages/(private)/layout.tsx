import { Outlet } from "react-router-dom";

import { AuthenticationGuard } from "@/components/AuthenticationGuard.tsx";

export default function PrivateLayout() {
  return (
    <div className="min-h-screen w-screen flex items-center justify-center relative">
      <AuthenticationGuard>
        <Outlet />
      </AuthenticationGuard>
    </div>
  );
}
