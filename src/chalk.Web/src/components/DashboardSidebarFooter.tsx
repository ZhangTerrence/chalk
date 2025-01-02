import { useLogoutMutation } from "@/redux/services/auth.ts";
import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { LogOutIcon, SettingsIcon, UserIcon } from "lucide-react";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { Button } from "@/components/ui/button.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarFooter, SidebarMenuButton, SidebarMenuItem, useSidebar } from "@/components/ui/sidebar.tsx";

import { useAuth } from "@/hooks/useAuth.tsx";

export const DashboardSidebarFooter = () => {
  const user = useAuth().user!;
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
                <AvatarImage src={user.profilePicture} alt={fullName} />
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
            sideOffset={4}
          >
            <DropdownMenuItem>
              <Button className="grow justify-normal p-0" variant="ghost" onClick={() => {}}>
                <UserIcon />
                Profile
              </Button>
            </DropdownMenuItem>
            <DropdownMenuItem>
              <Button className="grow justify-normal p-0" variant="ghost" onClick={() => {}}>
                <SettingsIcon />
                Settings
              </Button>
            </DropdownMenuItem>
            <DropdownMenuItem>
              <Button className="grow justify-normal p-0" variant="ghost" onClick={() => logout(null)}>
                <LogOutIcon />
                Logout
              </Button>
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </SidebarMenuItem>
    </SidebarFooter>
  );
};
