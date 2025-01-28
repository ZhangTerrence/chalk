import { ChevronDownIcon } from "lucide-react";
import { useNavigate } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import { Collapsible, CollapsibleContent, CollapsibleTrigger } from "@/components/ui/collapsible.tsx";
import {
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarMenuSub,
  SidebarMenuSubItem,
} from "@/components/ui/sidebar.tsx";

import { AddCourseDropdown } from "@/components/Dropdowns/AddCourseDropdown.tsx";

import { selectUserCourses } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const CoursesSection = () => {
  const courses = useTypedSelector(selectUserCourses) ?? [];
  const navigate = useNavigate();

  return (
    <Collapsible defaultOpen className="group/collapsible [&[data-state=open]>div>button>svg:first-child]:rotate-180">
      <SidebarGroup className="gap-y-2">
        <SidebarGroupLabel className="underline" asChild>
          <CollapsibleTrigger className="flex justify-between">
            Courses
            <ChevronDownIcon className="ml-auto transition-transform" />
          </CollapsibleTrigger>
        </SidebarGroupLabel>
        <CollapsibleContent>
          <SidebarGroupContent>
            <SidebarMenu className="gap-y-2">
              <AddCourseDropdown />
              {courses.map((course) => (
                <Collapsible
                  key={course.name}
                  defaultOpen={false}
                  className="group/collapsible [&[data-state=open]>li>button>svg:last-child]:rotate-180"
                >
                  <SidebarMenuItem>
                    <SidebarMenuButton className="px-2" asChild>
                      <CollapsibleTrigger>
                        <span className="w-[90%] overflow-x-clip text-ellipsis text-nowrap">
                          {course.code && course.code + " - "}
                          {course.name}
                        </span>
                        <ChevronDownIcon className={`ml-auto transition-transform`} />
                      </CollapsibleTrigger>
                    </SidebarMenuButton>
                    <CollapsibleContent>
                      <SidebarMenuSub>
                        <SidebarMenuSubItem onClick={() => navigate(`/courses/${course.id}`)}>
                          <Button variant="ghost" className="w-full justify-normal pl-2">
                            Home
                          </Button>
                        </SidebarMenuSubItem>
                        <SidebarMenuSubItem onClick={() => navigate(`/courses/${course.id}/modules`)}>
                          <Button variant="ghost" className="w-full justify-normal pl-2">
                            Modules
                          </Button>
                        </SidebarMenuSubItem>
                        <SidebarMenuSubItem onClick={() => navigate(`/courses/${course.id}/settings`)}>
                          <Button variant="ghost" className="w-full justify-normal pl-2">
                            Settings
                          </Button>
                        </SidebarMenuSubItem>
                      </SidebarMenuSub>
                    </CollapsibleContent>
                  </SidebarMenuItem>
                </Collapsible>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </CollapsibleContent>
      </SidebarGroup>
    </Collapsible>
  );
};
