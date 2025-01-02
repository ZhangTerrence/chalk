import { Button } from "@/components/ui/button.tsx";
import { DialogContent, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Label } from "@/components/ui/label.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Switch } from "@/components/ui/switch.tsx";

import { selectTheme, toggleDarkMode } from "@/redux/slices/theme.ts";

import { useStore } from "@/hooks/useStore.tsx";

type SettingsProps = {
  setDialog: (dialog: null | "profile" | "settings") => void;
};

export const SettingsDialog = (props: SettingsProps) => {
  const [useTypedSelector, useAppDispatch] = useStore();
  const theme = useTypedSelector(selectTheme);
  const dispatch = useAppDispatch();

  return (
    <DialogContent>
      <DialogHeader>
        <DialogTitle>Settings</DialogTitle>
      </DialogHeader>
      <Separator />
      <div className="flex items-center space-x-2">
        <Switch id="toggle-dark-mode" defaultChecked={theme.darkMode} onClick={() => dispatch(toggleDarkMode())} />
        <Label htmlFor="toggle-dark-">Dark Mode</Label>
      </div>
      <DialogFooter>
        <Button onClick={() => props.setDialog(null)}>Confirm</Button>
      </DialogFooter>
    </DialogContent>
  );
};
