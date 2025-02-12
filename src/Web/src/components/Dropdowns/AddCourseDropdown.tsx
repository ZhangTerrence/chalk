import { DropdownMenuTrigger } from "@radix-ui/react-dropdown-menu";
import { PlusIcon, SearchIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem } from "@/components/ui/dropdown-menu.tsx";
import { SidebarMenuItem, useSidebar } from "@/components/ui/sidebar.tsx";

import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";

export const AddCourseDropdown = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { isMobile } = useSidebar();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger className="focus:outline-none" asChild>
        <SidebarMenuItem>
          <Button variant="outline" className="w-full">
            Add
          </Button>
        </SidebarMenuItem>
      </DropdownMenuTrigger>
      <DropdownMenuContent
        side={isMobile ? "bottom" : "right"}
        align="start"
        sideOffset={isMobile ? 5 : 20}
        className="w-[--radix-dropdown-menu-trigger-width] min-w-56 rounded-lg"
      >
        <DropdownMenuItem onClick={() => navigate("/courses")} className="py-3">
          <span className="flex items-center gap-x-2">
            <SearchIcon />
            <p>Find Courses</p>
          </span>
        </DropdownMenuItem>
        <DropdownMenuItem
          onClick={() => dispatch(setDialog({ entity: null, type: DialogType.CreateCourse }))}
          className="py-3"
        >
          <span className="flex items-center gap-x-2">
            <PlusIcon />
            <p>Create Course</p>
          </span>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};
