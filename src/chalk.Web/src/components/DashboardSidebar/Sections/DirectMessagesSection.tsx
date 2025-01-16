import { PlusIcon } from "lucide-react";
import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { SidebarGroup, SidebarGroupAction, SidebarGroupLabel } from "@/components/ui/sidebar.tsx";

export const DirectMessagesSection = () => {
  return (
    <SidebarGroup>
      <SidebarGroupLabel>Direct Messages</SidebarGroupLabel>
      <SidebarGroupAction>
        <Button variant="ghost" size="icon" asChild>
          <NavLink to="/dashboard" className="h-fit w-fit">
            <PlusIcon />
          </NavLink>
        </Button>
      </SidebarGroupAction>
    </SidebarGroup>
  );
};
