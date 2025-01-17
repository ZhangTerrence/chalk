import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { LogOutIcon, SettingsIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarFooter, SidebarMenuButton, SidebarMenuItem, useSidebar } from "@/components/ui/sidebar.tsx";

import { useLogoutMutation } from "@/redux/services/account.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const DashboardSidebarFooter = () => {
  const user = useTypedSelector(selectUser)!;
  const [logout] = useLogoutMutation();
  const { isMobile } = useSidebar();
  const navigate = useNavigate();

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
                <AvatarImage src={user.profilePicture} alt={fullName} className="object-contain" />
                <AvatarFallback className="rounded-lg">{fullName.charAt(0).toUpperCase()}</AvatarFallback>
              </Avatar>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <span className="truncate font-semibold">{fullName}</span>
                <span className="truncate text-xs">{user.email}</span>
              </div>
            </SidebarMenuButton>
          </DropdownMenuTrigger>
          <DropdownMenuContent
            side={isMobile ? "bottom" : "right"}
            align="end"
            sideOffset={isMobile ? 5 : 20}
            className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
          >
            <DropdownMenuItem onClick={() => navigate("/settings")} className="py-3">
              <SettingsIcon />
              Settings
            </DropdownMenuItem>
            <DropdownMenuItem onClick={() => logout(null)} className="py-3">
              <LogOutIcon />
              Logout
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </SidebarMenuItem>
    </SidebarFooter>
  );
};
