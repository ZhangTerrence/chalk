import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";

import { Button } from "@/components/ui/button.tsx";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
} from "@/components/ui/dropdown-menu.tsx";
import { SidebarGroup, SidebarGroupContent, SidebarHeader } from "@/components/ui/sidebar.tsx";

type DashboardSidebarHeaderProps = {
  contextList: { [key: string]: { id: number } };
  context: string;
  changeContext: (context: string) => void;
};

export const DashboardSidebarHeader = (props: DashboardSidebarHeaderProps) => {
  return (
    <SidebarHeader>
      <SidebarGroup>
        <SidebarGroupContent>
          <DropdownMenu>
            <DropdownMenuTrigger className="w-full border-2 rounded-lg p-2">{props.context}</DropdownMenuTrigger>
            <DropdownMenuContent className="w-[--radix-popper-anchor-width]" sideOffset={10}>
              {Object.entries(props.contextList).map(([k, v]) => (
                <DropdownMenuItem key={v.id} className="flex justify-center">
                  {k}
                </DropdownMenuItem>
              ))}
              <DropdownMenuSeparator />
              {Object.keys(props.contextList).length > 1 && (
                <>
                  <DropdownMenuLabel className="font-semibold">Organizations</DropdownMenuLabel>
                  <DropdownMenuSeparator />
                </>
              )}
              <div className="p-2 flex flex-col space-y-4">
                <Button className="w-full">Finds Organizations</Button>
                <Button className="w-full">Create Organization</Button>
              </div>
            </DropdownMenuContent>
          </DropdownMenu>
        </SidebarGroupContent>
      </SidebarGroup>
    </SidebarHeader>
  );
};
