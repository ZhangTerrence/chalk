import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { LogOutIcon, SettingsIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarFooter, SidebarMenuButton, SidebarMenuItem, useSidebar } from "@/components/ui/sidebar.tsx";

import { useLogoutMutation } from "@/redux/services/base.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const DashboardSidebarFooter = () => {
  const user = useTypedSelector(selectUser)!;
  const navigate = useNavigate();
  const { isMobile } = useSidebar();
  const [logout] = useLogoutMutation();

  const fullName = `${user.firstName} ${user.lastName}`;

  return (
    <SidebarFooter>
      <SidebarMenuItem>
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <SidebarMenuButton
              size="lg"
              className="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
            >
              <Avatar className="h-8 w-8 rounded-lg">
                <AvatarImage src={user.profilePicture ?? undefined} alt={fullName} className="object-contain" />
                <AvatarFallback className="rounded-lg">{fullName.charAt(0).toUpperCase()}</AvatarFallback>
              </Avatar>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <span className="truncate font-semibold">{fullName}</span>
                <span className="truncate text-xs">{user.email}</span>
              </div>
            </SidebarMenuButton>
          </DropdownMenuTrigger>
          <DropdownMenuContent
            className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
            side={isMobile ? "bottom" : "right"}
            align="end"
            sideOffset={isMobile ? 5 : 20}
          >
            <DropdownMenuItem className="py-3" onClick={() => navigate("/settings")}>
              <SettingsIcon />
              Settings
            </DropdownMenuItem>
            <DropdownMenuItem className="py-3" onClick={() => logout(null)}>
              <LogOutIcon />
              Logout
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </SidebarMenuItem>
    </SidebarFooter>
  );
};
