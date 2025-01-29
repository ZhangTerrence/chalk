import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { Button } from "@/components/ui/button.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateCourseMutation } from "@/redux/services/course.ts";
import { selectDialog, setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import type { CourseResponse } from "@/lib/types/course.ts";
import { getImageData } from "@/lib/utils.ts";
import { UpdateCourseSchema, type UpdateCourseType } from "@/lib/validators/course.ts";

export const UpdateCourseDialog = () => {
  const dialog = useTypedSelector(selectDialog)!;
  const dispatch = useAppDispatch();
  const [updateCourse, { isLoading, isSuccess }] = useUpdateCourseMutation();

  const course = (dialog.entity as CourseResponse)!;

  const [image, setImage] = React.useState<string | undefined>(course.imageUrl ?? undefined);
  const [uploadedImage, setUploadedImage] = React.useState<File | null>();

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      dispatch(setDialog(null));
      form.reset();
      toast.success("Successfully edited course.");
    }
  }, [isLoading, isSuccess]);

  const form = useForm<UpdateCourseType>({
    resolver: zodResolver(UpdateCourseSchema),
    defaultValues: {
      name: course.name,
      code: course.code ?? undefined,
      description: course.description ?? undefined,
      isPublic: course.isPublic,
    },
  });

  const onFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { files, displayUrl } = getImageData(event);
    if (files.length !== 1) {
      toast.error("Only one file can be uploaded.");
      return;
    }

    setImage(displayUrl);
    setUploadedImage(files.item(0)!);
  };

  const onSubmit = async (data: UpdateCourseType) => {
    if (
      course.name === data.name &&
      (course.code ?? "") === data.code &&
      (course.description ?? "") === data.description &&
      course.isPublic === data.isPublic &&
      !uploadedImage
    ) {
      return;
    }

    const modules = [...course.modules];

    const formData = new FormData();

    for (let [key, value] of Object.entries(data)) {
      formData.append(key, typeof value === "boolean" || value ? (value as string) : "");
    }
    if (uploadedImage) {
      formData.append("image", uploadedImage);
    }
    formData.append(
      "modules",
      JSON.stringify(modules.sort((a, b) => a.relativeOrder - b.relativeOrder).map((e) => e.id)),
    );

    await updateCourse({ id: course.id, data: formData }).unwrap();

    dispatch(setDialog(null));
  };

  return (
    <>
      <DialogHeader>
        <DialogTitle>Edit Course</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="flex min-w-80 flex-col gap-y-4">
          <div className="flex gap-x-4">
            <div className="relative aspect-square w-36">
              <Avatar className="w-full h-full">
                <AvatarImage
                  src={image}
                  alt={course.name}
                  className="rounded-full border border-primary object-contain"
                />
                <AvatarFallback className="rounded-full border border-primary text-2xl">
                  {course.name.charAt(0).toUpperCase()}
                </AvatarFallback>
              </Avatar>
              <Input
                type="file"
                accept="image/*"
                onChange={onFileUpload}
                className="absolute top-0 h-full w-full opacity-0 hover:cursor-pointer"
              />
            </div>
            <div className="flex grow flex-col gap-y-2">
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
            </div>
          </div>
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
