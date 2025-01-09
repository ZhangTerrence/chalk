import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { PlusIcon, UsersIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Dialog, DialogTrigger } from "@/components/ui/dialog.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarGroup, SidebarGroupAction, SidebarGroupLabel, useSidebar } from "@/components/ui/sidebar.tsx";

import { CreateOrganizationDialog } from "@/components/DashboardSidebar/Dialogs/CreateOrganizationDialog.tsx";

export const OrganizationsSection = () => {
  const { isMobile } = useSidebar();
  const navigate = useNavigate();

  return (
    <SidebarGroup>
      <Dialog>
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
              <div className="flex space-x-2 items-center">
                <UsersIcon />
                <span>Find Organizations</span>
              </div>
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3">
              <DialogTrigger className="flex w-full">
                <div className="flex space-x-2 items-center">
                  <PlusIcon />
                  <span>Create New Organization</span>
                </div>
              </DialogTrigger>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
        <CreateOrganizationDialog />
      </Dialog>
    </SidebarGroup>
  );
};
