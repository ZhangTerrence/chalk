import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useCreateCourseMutation } from "@/redux/services/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch } from "@/redux/store.ts";

import { CreateCourseSchema, type CreateCourseType } from "@/lib/validators/course.ts";

export const CreateCourseDialog = () => {
  const dispatch = useAppDispatch();
  const [createCourse, { isLoading, isSuccess }] = useCreateCourseMutation();

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      dispatch(setDialog(null));
      form.reset();
      toast.success("Successfully created course.");
    }
  }, [isLoading, isSuccess]);

  const form = useForm<CreateCourseType>({
    resolver: zodResolver(CreateCourseSchema),
    defaultValues: {
      name: "",
      code: "",
      description: "",
      isPublic: false,
    },
  });

  return (
    <>
      <DialogHeader>
        <DialogTitle>Create Course</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(async (data) => await createCourse(data).unwrap())}
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
          <FormField
            control={form.control}
            name="code"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Code</FormLabel>
                <FormControl>
                  <Input {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="isPublic"
            render={({ field }) => (
              <FormItem className="w-full">
                <FormLabel>Type</FormLabel>
                <Select
                  defaultValue={field.value.toString()}
                  onValueChange={(value) => field.onChange(value === "true")}
                >
                  <FormControl>
                    <SelectTrigger className="w-full">
                      <SelectValue />
                    </SelectTrigger>
                  </FormControl>
                  <SelectContent>
                    <SelectItem value="true">Public</SelectItem>
                    <SelectItem value="false">Private</SelectItem>
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />
          <DialogFooter className="max-md:flex max-md:w-full max-md:flex-row max-md:justify-end max-md:space-x-4">
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
