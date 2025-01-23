import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { BrushIcon, LogOutIcon, SettingsIcon, UserIcon } from "lucide-react";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarFooter, SidebarMenuButton, SidebarMenuItem, useSidebar } from "@/components/ui/sidebar.tsx";

import type { DashboardDialogs } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

import { useLogoutMutation } from "@/redux/services/account.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

type DashboardSidebarFooterProps = {
  changeDialog: (type: Pick<DashboardDialogs, "type">["type"]) => void;
};

export const DashboardSidebarFooter = (props: DashboardSidebarFooterProps) => {
  const user = useTypedSelector(selectUser)!;
  const [logout] = useLogoutMutation();
  const { isMobile } = useSidebar();

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
                <AvatarImage
                  src={user.profilePicture ?? undefined}
                  alt={fullName}
                  className="rounded-full border border-primary object-contain"
                />
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
            <DropdownMenuItem onClick={() => props.changeDialog("update-account")} className="py-3">
              <SettingsIcon />
              Account
            </DropdownMenuItem>
            <DropdownMenuItem onClick={() => props.changeDialog("update-profile")} className="py-3">
              <UserIcon />
              Profile
            </DropdownMenuItem>
            <DropdownMenuItem onClick={() => props.changeDialog("update-appearance")} className="py-3">
              <BrushIcon />
              Appearance
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
