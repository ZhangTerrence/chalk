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

import { useCreateFileMutation } from "@/redux/services/file.ts";
import { selectDialog, setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import type { ModuleDTO } from "@/lib/types/course.ts";
import { For } from "@/lib/types/file.ts";
import { getImageData } from "@/lib/utils.ts";
import { CreateFileSchema, type CreateFileType } from "@/lib/validators/file.ts";

export const CreateFileDialog = () => {
  const dialog = useTypedSelector(selectDialog)!;
  const dispatch = useAppDispatch();
  const [createFile, { isLoading, isSuccess }] = useCreateFileMutation();

  const module = dialog.entity as ModuleDTO;

  const [uploadedFile, setUploadedFile] = React.useState<File | null>();

  const form = useForm<CreateFileType>({
    resolver: zodResolver(CreateFileSchema),
    defaultValues: {
      name: "",
      description: "",
    },
  });

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      dispatch(setDialog(null));
      form.reset();
      toast.success("Successfully created file for module.");
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

  const onSubmit = async (data: CreateFileType) => {
    if (!uploadedFile) {
      return;
    }

    const formData = new FormData();

    for (let [key, value] of Object.entries(data)) {
      formData.append(key, value);
    }
    formData.append("for", For.Module.toString());
    formData.append("entityId", module.id.toString());
    formData.append("file", uploadedFile);

    await createFile(formData).unwrap();

    dispatch(setDialog(null));
  };

  return (
    <>
      <DialogHeader>
        <DialogTitle>Create File</DialogTitle>
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
          <div className="flex flex-col gap-y-2">
            <Label htmlFor="file">File</Label>
            <Input id="file" type="file" onChange={onFileUpload} className="hover:cursor-pointer pt-1.5" />
          </div>
          <DialogFooter className="max-md:flex max-md:w-full max-md:flex-row max-md:justify-end max-md:gap-x-4">
            <DialogClose asChild>
              <Button variant="outline">Cancel</Button>
            </DialogClose>
            <Button>Create</Button>
          </DialogFooter>
        </form>
      </Form>
    </>
  );
};
