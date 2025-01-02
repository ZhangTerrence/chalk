import { PlusIcon } from "lucide-react";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupAction,
  SidebarGroupLabel,
  SidebarHeader,
  SidebarRail,
} from "@/components/ui/sidebar.tsx";

import { DashboardSidebarFooter } from "@/components/DashboardSidebarFooter.tsx";

export const DashboardSidebar = () => {
  return (
    <Sidebar>
      <SidebarHeader />
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel>Organizations</SidebarGroupLabel>
          <SidebarGroupAction>
            <Button variant="ghost" size="icon" asChild>
              <NavLink className="h-fit w-fit" to="/organization">
                <PlusIcon />
              </NavLink>
            </Button>
          </SidebarGroupAction>
        </SidebarGroup>
        <SidebarGroup>
          <SidebarGroupLabel>Courses</SidebarGroupLabel>
        </SidebarGroup>
      </SidebarContent>
      <DashboardSidebarFooter />
      <SidebarRail />
    </Sidebar>
  );
};
