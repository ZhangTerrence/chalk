import { DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Label } from "@/components/ui/label.tsx";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Switch } from "@/components/ui/switch.tsx";

import { changeColorScheme, selectTheme, toggleDarkMode } from "@/redux/slices/theme.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { ColorScheme } from "@/lib/colorScheme.ts";

export const UpdateAppearanceDialog = () => {
  const theme = useTypedSelector(selectTheme);
  const dispatch = useAppDispatch();

  return (
    <>
      <DialogHeader>
        <DialogTitle>Appearance</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <div className="flex flex-col gap-y-4">
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
      </div>
      <div className="flex items-center justify-between gap-x-2 py-2">
        <Label htmlFor="toggle-dark-mode" className="text-md font-semibold">
          Dark Mode
        </Label>
        <Switch id="toggle-dark-mode" defaultChecked={theme.darkMode} onClick={() => dispatch(toggleDarkMode())} />
      </div>
    </>
  );
};
