import { Outlet } from "react-router-dom";

import { SidebarInset, SidebarProvider, SidebarTrigger } from "@/components/ui/sidebar.tsx";

import { DashboardSidebar } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

export default function DashboardLayout() {
  return (
    <div className="min-h-screen w-screen">
      <SidebarProvider>
        <DashboardSidebar />
        <SidebarInset className="p-4">
          <SidebarTrigger />
          <Outlet />
        </SidebarInset>
      </SidebarProvider>
    </div>
  );
}
