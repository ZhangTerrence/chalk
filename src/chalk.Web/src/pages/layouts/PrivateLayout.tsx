import { Outlet } from "react-router-dom";

import { Dialog, DialogContent } from "@/components/ui/dialog.tsx";

import { AuthenticationGuard } from "@/components/AuthenticationGuard.tsx";
import { CreateAssignmentDialog } from "@/components/Dialogs/CreateAssignmentDialog.tsx";
import { CreateAssignmentGroupDialog } from "@/components/Dialogs/CreateAssignmentGroupDialog.tsx";
import { CreateCourseDialog } from "@/components/Dialogs/CreateCourseDialog.tsx";
import { CreateFileDialog } from "@/components/Dialogs/CreateFileDialog.tsx";
import { CreateModuleDialog } from "@/components/Dialogs/CreateModuleDialog.tsx";
import { CreateOrganizationDialog } from "@/components/Dialogs/CreateOrganizationDialog.tsx";
import { UpdateAccountDialog } from "@/components/Dialogs/UpdateAccountDialog.tsx";
import { UpdateAppearanceDialog } from "@/components/Dialogs/UpdateAppearanceDialog.tsx";
import { UpdateCourseDialog } from "@/components/Dialogs/UpdateCourseDialog.tsx";
import { UpdateFileDialog } from "@/components/Dialogs/UpdateFileDialog.tsx";
import { UpdateModuleDialog } from "@/components/Dialogs/UpdateModuleDialog.tsx";
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
        case DialogType.UpdateCourse:
          return <UpdateCourseDialog />;
        case DialogType.CreateModule:
          return <CreateModuleDialog />;
        case DialogType.UpdateModule:
          return <UpdateModuleDialog />;
        case DialogType.CreateAssignmentGroup:
          return <CreateAssignmentGroupDialog />;
        case DialogType.CreateAssignment:
          return <CreateAssignmentDialog />;
        case DialogType.CreateOrganization:
          return <CreateOrganizationDialog />;
        case DialogType.CreateFile:
          return <CreateFileDialog />;
        case DialogType.UpdateFile:
          return <UpdateFileDialog />;
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
