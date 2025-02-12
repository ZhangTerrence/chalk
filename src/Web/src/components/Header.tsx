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
    <header className="fixed top-0 w-screen">
      <NavigationMenu>
        <NavigationMenuList className="flex w-screen justify-between p-4">
          <NavigationMenuItem>
            <NavigationMenuLink asChild>
              <NavLink to="/" className="ml-2 hover:text-primary/90">
                Chalk
              </NavLink>
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
