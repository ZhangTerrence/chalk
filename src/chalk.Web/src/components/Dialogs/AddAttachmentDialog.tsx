import React from "react";

import { DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Separator } from "@/components/ui/separator.tsx";

export const AddAttachmentDialog = () => {
  return (
    <>
      <DialogHeader>
        <DialogTitle>Add Attachment</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
    </>
  );
};
