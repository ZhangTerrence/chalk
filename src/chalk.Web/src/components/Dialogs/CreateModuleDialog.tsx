import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useCreateModuleMutation } from "@/redux/services/course.ts";
import { selectDialog, setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import type { CourseDTO } from "@/lib/types/course.ts";
import { CreateModuleSchema, type CreateModuleType } from "@/lib/validators/module.ts";

export const CreateModuleDialog = () => {
  const dialog = useTypedSelector(selectDialog)!;
  const dispatch = useAppDispatch();
  const [createModule, { isLoading, isSuccess }] = useCreateModuleMutation();

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      dispatch(setDialog(null));
      form.reset();
      toast.success("Successfully created module.");
    }
  }, [isLoading, isSuccess]);

  const form = useForm<CreateModuleType>({
    resolver: zodResolver(CreateModuleSchema),
    defaultValues: {
      name: "",
      description: "",
    },
  });

  return (
    <>
      <DialogHeader>
        <DialogTitle>Create Module</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(
            async (data) =>
              await createModule({
                courseId: (dialog.entity as CourseDTO)!.id,
                data: data,
              }).unwrap(),
          )}
          className="flex min-w-80 flex-col gap-y-4"
        >
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
