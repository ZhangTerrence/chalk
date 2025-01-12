import { Label } from "@/components/ui/label.tsx";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select.tsx";
import { Switch } from "@/components/ui/switch.tsx";

import { changeColorScheme, selectTheme, toggleDarkMode } from "@/redux/slices/theme.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { ColorScheme } from "@/lib/theme.ts";

export const ThemeSection = () => {
  const theme = useTypedSelector(selectTheme);
  const dispatch = useAppDispatch();

  return (
    <div className="grow p-4 pt-20 pl-4 flex flex-col gap-y-4">
      <h3 className="text-md font-semibold">Color Scheme</h3>
      <Select
        defaultValue={theme.colorScheme}
        onValueChange={(colorScheme) => dispatch(changeColorScheme(colorScheme as ColorScheme))}
      >
        <SelectTrigger>
          <SelectValue />
        </SelectTrigger>
        <SelectContent>
          <SelectItem value="zinc">Zinc</SelectItem>
          <SelectItem value="red">Red</SelectItem>
          <SelectItem value="rose">Rose</SelectItem>
          <SelectItem value="orange">Orange</SelectItem>
          <SelectItem value="green">Green</SelectItem>
          <SelectItem value="blue">Blue</SelectItem>
          <SelectItem value="yellow">Yellow</SelectItem>
          <SelectItem value="violet">Violet</SelectItem>
        </SelectContent>
      </Select>
      <div className="flex items-center space-x-2 justify-between py-2">
        <Label htmlFor="toggle-dark-mode" className="text-md font-semibold">
          Dark Mode
        </Label>
        <Switch id="toggle-dark-mode" defaultChecked={theme.darkMode} onClick={() => dispatch(toggleDarkMode())} />
      </div>
    </div>
  );
};
