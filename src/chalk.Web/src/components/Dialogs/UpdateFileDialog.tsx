import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Label } from "@/components/ui/label.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateFileMutation } from "@/redux/services/file.ts";
import { selectDialog, setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { type FileDTO, For } from "@/lib/types/file.ts";
import { getImageData } from "@/lib/utils.ts";
import { UpdateFileSchema, type UpdateFileType } from "@/lib/validators/file.ts";

export const UpdateFileDialog = () => {
  const dialog = useTypedSelector(selectDialog)!;
  const dispatch = useAppDispatch();
  const [updateFile, { isLoading, isSuccess }] = useUpdateFileMutation();

  const file = (dialog.entity as FileDTO & { for: For; entityId: number })!;

  const [uploadedFile, setUploadedFile] = React.useState<File | null>();

  const form = useForm<UpdateFileType>({
    resolver: zodResolver(UpdateFileSchema),
    defaultValues: {
      name: file.name,
      description: file.description ?? undefined,
    },
  });

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      dispatch(setDialog(null));
      form.reset();
      toast.success("Successfully edited file.");
    }
  }, [isLoading, isSuccess]);

  const onFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { files } = getImageData(event);
    if (files.length !== 1) {
      toast.error("Only one file can be uploaded.");
      return;
    }

    setUploadedFile(files.item(0)!);
  };

  const onSubmit = async (data: UpdateFileType) => {
    if (file.name === data.name && (file.description ?? undefined) === data.description && !uploadedFile) {
      dispatch(setDialog(null));
      return;
    }

    const formData = new FormData();

    for (let [key, value] of Object.entries(data)) {
      formData.append(key, value ?? "");
    }
    formData.append("for", file.for.toString());
    formData.append("entityId", file.entityId.toString());
    formData.append("fileChanged", (!!uploadedFile).toString());
    if (uploadedFile) {
      formData.append("newFile", uploadedFile);
    }

    await updateFile({
      id: file.id,
      data: formData,
    }).unwrap();

    dispatch(setDialog(null));
  };

  return (
    <>
      <DialogHeader>
        <DialogTitle>Edit File</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="flex min-w-80 flex-col gap-y-4">
          <FormField
            control={form.control}
            name="name"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Name</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="description"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Description</FormLabel>
                <FormControl>
                  <Textarea {...field} className="max-h-40" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <div className="flex flex-col gap-y-2 grow">
            <Label htmlFor="resource">File</Label>
            <Input id="resource" type="file" onChange={onFileUpload} className="hover:cursor-pointer pt-1.5" />
          </div>
          <DialogFooter className="max-md:flex max-md:w-full max-md:flex-row max-md:justify-end max-md:gap-x-4">
            <DialogClose asChild>
              <Button variant="outline">Cancel</Button>
            </DialogClose>
            <Button>Edit</Button>
          </DialogFooter>
        </form>
      </Form>
    </>
  );
};
