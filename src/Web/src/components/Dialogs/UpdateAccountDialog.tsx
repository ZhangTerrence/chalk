import { DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Separator } from "@/components/ui/separator.tsx";

import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

export const UpdateAccountDialog = () => {
  const user = useTypedSelector(selectUser)!;

  return (
    <>
      <DialogHeader>
        <DialogTitle>Account</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <div>
        <span>{user.email}</span>
      </div>
    </>
  );
};
