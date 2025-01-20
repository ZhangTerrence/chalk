import { ChevronDownIcon } from "lucide-react";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible.tsx";
import {
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuItem,
} from "@/components/ui/sidebar.tsx";

export const DirectMessagesSection = () => {
  return (
    <Collapsible defaultOpen className="group/collapsible">
      <SidebarGroup className="gap-y-2">
        <SidebarGroupLabel className="underline" asChild>
          <CollapsibleTrigger className="flex justify-between">
            Direct Messages
            <ChevronDownIcon className="ml-auto transition-transform group-data-[state=open]/collapsible:rotate-180" />
          </CollapsibleTrigger>
        </SidebarGroupLabel>
        <CollapsibleContent>
          <SidebarGroupContent>
            <SidebarMenu className="gap-y-2">
              <SidebarMenuItem>
                <Button variant="outline" className="w-full">
                  Add
                </Button>
              </SidebarMenuItem>
            </SidebarMenu>
          </SidebarGroupContent>
        </CollapsibleContent>
      </SidebarGroup>
    </Collapsible>
  );
};
