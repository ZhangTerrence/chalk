import { Outlet } from "react-router-dom";

import { AuthenticationGuard } from "@/components/AuthenticationGuard.tsx";

export default function PrivateLayout() {
  return (
    <div className="relative flex min-h-screen w-screen items-center justify-center">
      <AuthenticationGuard>
        <Outlet />
      </AuthenticationGuard>
    </div>
  );
}
