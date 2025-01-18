import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { PlusIcon, UsersIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarGroup, SidebarGroupAction, SidebarGroupLabel, useSidebar } from "@/components/ui/sidebar.tsx";

import type { DashboardDialog } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

type OrganizationsSectionProps = {
  changeDialog: (section: Pick<DashboardDialog, "section">["section"]) => void;
};

export const OrganizationsSection = (props: OrganizationsSectionProps) => {
  const { isMobile } = useSidebar();
  const navigate = useNavigate();

  return (
    <SidebarGroup>
      <DropdownMenu>
        <DropdownMenuTrigger>
          <SidebarGroupLabel>Organizations</SidebarGroupLabel>
          <SidebarGroupAction asChild>
            <PlusIcon />
          </SidebarGroupAction>
        </DropdownMenuTrigger>
        <DropdownMenuContent
          side={isMobile ? "bottom" : "right"}
          align="start"
          sideOffset={isMobile ? 5 : 20}
          className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
        >
          <DropdownMenuItem onClick={() => navigate("/organizations")} className="py-3">
            <div className="flex space-x-2 items-center">
              <UsersIcon />
              <span>Find Organizations</span>
            </div>
          </DropdownMenuItem>
          <DropdownMenuItem onClick={() => props.changeDialog("create-organization")} className="py-3">
            <div className="flex space-x-2 items-center">
              <PlusIcon />
              <span>Create New Organization</span>
            </div>
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </SidebarGroup>
  );
};
