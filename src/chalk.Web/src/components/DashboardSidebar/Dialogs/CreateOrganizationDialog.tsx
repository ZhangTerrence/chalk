import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Separator } from "@/components/ui/separator.tsx";

import type { DashboardDialog } from "@/components/DashboardSidebar/DashboardSidebar.tsx";

type CreateOrganizationDialogProps = {
  changeDialog: (section: Pick<DashboardDialog, "section">["section"]) => void;
};

export const CreateOrganizationDialog = (props: CreateOrganizationDialogProps) => {
  return (
    <>
      <DialogHeader>
        <DialogTitle>Create Organization</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <DialogFooter className="max-md:flex max-md:flex-row max-md:space-x-4 max-md:justify-end max-md:w-full">
        <DialogClose asChild>
          <Button variant="outline">Cancel</Button>
        </DialogClose>
        <Button>Create</Button>
      </DialogFooter>
    </>
  );
};
