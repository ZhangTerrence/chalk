import { Outlet } from "react-router-dom";

import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";

import { AuthenticationGuard } from "@/components/AuthenticationGuard.tsx";
import { CreateAttachmentDialog } from "@/components/Dialogs/CreateAttachmentDialog.tsx";
import { CreateCourseDialog } from "@/components/Dialogs/CreateCourseDialog.tsx";
import { CreateCourseModuleDialog } from "@/components/Dialogs/CreateCourseModuleDialog.tsx";
import { CreateOrganizationDialog } from "@/components/Dialogs/CreateOrganizationDialog.tsx";
import { UpdateAccountDialog } from "@/components/Dialogs/UpdateAccountDialog.tsx";
import { UpdateAppearanceDialog } from "@/components/Dialogs/UpdateAppearanceDialog.tsx";
import { UpdateCourseModuleDialog } from "@/components/Dialogs/UpdateCourseModuleDialog.tsx";
import { UpdateProfileDialog } from "@/components/Dialogs/UpdateProfileDialog.tsx";

import { selectDialog, setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";

export default function PrivateLayout() {
  const dialog = useTypedSelector(selectDialog);
  const dispatch = useAppDispatch();

  const renderDialogContent = () => {
    if (dialog) {
      switch (dialog.type) {
        case DialogType.UpdateAccount:
          return <UpdateAccountDialog />;
        case DialogType.UpdateProfile:
          return <UpdateProfileDialog />;
        case DialogType.UpdateAppearance:
          return <UpdateAppearanceDialog />;
        case DialogType.CreateCourse:
          return <CreateCourseDialog />;
        case DialogType.CreateCourseModule:
          return <CreateCourseModuleDialog />;
        case DialogType.UpdateCourseModule:
          return <UpdateCourseModuleDialog />;
        case DialogType.CreateOrganization:
          return <CreateOrganizationDialog />;
        case DialogType.CreateAttachment:
          return <CreateAttachmentDialog />;
        default:
          return null;
      }
    }
  };

  return (
    <div className="relative flex min-h-screen w-screen items-center justify-center">
      <AuthenticationGuard>
        <Dialog open={!!dialog} onOpenChange={(open) => dispatch(setDialog(open ? dialog : null))}>
          <Outlet />
          {dialog && <DialogContent>{renderDialogContent()}</DialogContent>}
        </Dialog>
      </AuthenticationGuard>
    </div>
  );
}
