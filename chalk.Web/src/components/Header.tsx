import { NavLink } from "react-router-dom";

import { Button } from "@/components/ui/button.tsx";
import {
  NavigationMenu,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
} from "@/components/ui/navigation-menu";

export const Header = () => {
  return (
    <header className="w-screen fixed top-0">
      <NavigationMenu>
        <NavigationMenuList className="w-screen p-4 flex justify-between">
          <NavigationMenuItem>
            <NavigationMenuLink asChild>
              <NavLink to="/">Chalk</NavLink>
            </NavigationMenuLink>
          </NavigationMenuItem>
          <div className="flex gap-x-2">
            <NavigationMenuItem>
              <NavigationMenuLink asChild>
                <NavLink to="/login">
                  <Button>Login</Button>
                </NavLink>
              </NavigationMenuLink>
            </NavigationMenuItem>
            <NavigationMenuItem>
              <NavigationMenuLink asChild>
                <NavLink to="/register">
                  <Button>Register</Button>
                </NavLink>
              </NavigationMenuLink>
            </NavigationMenuItem>
          </div>
        </NavigationMenuList>
      </NavigationMenu>
    </header>
  );
};
