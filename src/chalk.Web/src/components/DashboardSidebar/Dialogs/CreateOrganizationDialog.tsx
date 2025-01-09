import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogContent, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Separator } from "@/components/ui/separator.tsx";

export const CreateOrganizationDialog = () => {
  return (
    <DialogContent>
      <DialogHeader>
        <DialogTitle>Create Organization</DialogTitle>
      </DialogHeader>
      <Separator />
      <DialogFooter>
        <DialogClose asChild>
          <Button variant="outline">Cancel</Button>
        </DialogClose>
        <Button>Create</Button>
      </DialogFooter>
    </DialogContent>
  );
};
