import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { PlusIcon, UsersIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarGroup, SidebarGroupAction, SidebarGroupLabel, useSidebar } from "@/components/ui/sidebar.tsx";

export const OrganizationsSection = () => {
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
          className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
          side={isMobile ? "bottom" : "right"}
          align="start"
          sideOffset={isMobile ? 5 : 20}
        >
          <DropdownMenuItem className="py-3" onClick={() => navigate("/organizations")}>
            <UsersIcon />
            Join Existing Organization
          </DropdownMenuItem>
          <DropdownMenuItem className="py-3" onClick={() => navigate("/organizations/create")}>
            <PlusIcon />
            Create New Organization
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </SidebarGroup>
  );
};
