import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import type { CourseModulesDialogs } from "@/pages/(private)/(dashboard)/courses/[id]/modules/page.tsx";

import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateCourseModuleMutation } from "@/redux/services/course.ts";

import { AddCourseModuleSchema, type AddCourseModuleType } from "@/lib/validators/addCourseModule.ts";

type UpdateCourseModuleDialogProps = {
  courseModuleId: number;
  changeDialog: (id: number | null, type: Pick<CourseModulesDialogs, "type">["type"]) => void;
};

export const UpdateCourseModuleDialog = (props: UpdateCourseModuleDialogProps) => {
  const [updateCourseModule, { isLoading, isSuccess }] = useUpdateCourseModuleMutation();

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      props.changeDialog(null, null);
      form.reset();
      toast.success("Successfully add course module.");
    }
  }, [isLoading, isSuccess]);

  const form = useForm<AddCourseModuleType>({
    resolver: zodResolver(AddCourseModuleSchema),
    defaultValues: {
      name: "",
      description: "",
    },
  });

  return (
    <>
      <DialogHeader>
        <DialogTitle>Edit Course Module</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(
            async (data) =>
              await updateCourseModule({
                ...data,
                id: props.courseModuleId,
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
          <DialogFooter className="max-md:flex max-md:w-full max-md:flex-row max-md:justify-end max-md:space-x-4">
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
