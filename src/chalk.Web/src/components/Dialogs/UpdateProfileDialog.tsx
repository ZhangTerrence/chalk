import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { Button } from "@/components/ui/button.tsx";
import { DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateUserMutation } from "@/redux/services/user.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { getImageData } from "@/lib/utils.ts";
import { UpdateUserSchema, type UpdateUserType } from "@/lib/validators/updateUser.ts";

export const UpdateProfileDialog = () => {
  const user = useTypedSelector(selectUser)!;
  const dispatch = useAppDispatch();
  const [updateUser, { isLoading, isSuccess }] = useUpdateUserMutation();

  const [profilePicture, setProfilePicture] = React.useState<string | undefined>(user.profilePicture ?? undefined);
  const [uploadedProfilePicture, setUploadedProfilePicture] = React.useState<File | null>();

  const fullName = `${user.firstName} ${user.lastName}`;

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      toast.success("Successfully updated profile.");
    }
  }, [isLoading, isSuccess]);

  const form = useForm<UpdateUserType>({
    resolver: zodResolver(UpdateUserSchema),
    defaultValues: {
      firstName: user.firstName,
      lastName: user.lastName,
      displayName: user.displayName,
      description: user.description ?? "",
    },
  });

  const onFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { files, displayUrl } = getImageData(event);
    if (files.length !== 1) {
      toast.error("Only one file can be uploaded.");
      return;
    }

    setProfilePicture(displayUrl);
    setUploadedProfilePicture(files.item(0)!);
  };

  const onSubmit = async (data: UpdateUserType) => {
    if (
      user.firstName === data.firstName &&
      user.lastName === data.lastName &&
      user.displayName === data.displayName &&
      (user.description ?? "") === data.description &&
      !uploadedProfilePicture
    ) {
      return;
    }

    const formData = new FormData();

    for (let [key, value] of Object.entries(data)) {
      formData.append(key, value);
    }
    if (uploadedProfilePicture) {
      formData.append("profilePicture", uploadedProfilePicture);
    }

    await updateUser(formData).unwrap();

    dispatch(setDialog(null));
  };

  return (
    <>
      <DialogHeader>
        <DialogTitle>Profile</DialogTitle>
      </DialogHeader>
      <Separator orientation="horizontal" />
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col space-y-4">
          <div className="flex space-x-4">
            <div className="relative aspect-square h-auto rounded-full border border-primary">
              <Avatar className="aspect-square h-full w-full">
                <AvatarImage src={profilePicture} alt={fullName} className="object-contain object-center" />
                <AvatarFallback className="text-2xl">{fullName.charAt(0).toUpperCase()}</AvatarFallback>
              </Avatar>
              <Input
                type="file"
                accept="image/*"
                onChange={onFileUpload}
                className="absolute top-0 h-full w-full opacity-0 hover:cursor-pointer"
              />
            </div>
            <div className="flex grow flex-col space-y-2">
              <div className="flex space-x-2">
                <FormField
                  control={form.control}
                  name="firstName"
                  render={({ field }) => (
                    <FormItem className="grow">
                      <FormLabel className="text-nowrap">First Name</FormLabel>
                      <FormControl>
                        <Input {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
                <FormField
                  control={form.control}
                  name="lastName"
                  render={({ field }) => (
                    <FormItem className="grow">
                      <FormLabel className="text-nowrap">Last Name</FormLabel>
                      <FormControl>
                        <Input {...field} />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <FormField
                control={form.control}
                name="displayName"
                render={({ field }) => (
                  <FormItem className="">
                    <FormLabel className="text-nowrap">Display Name</FormLabel>
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
              <FormItem className="grow">
                <FormLabel>Description</FormLabel>
                <FormControl>
                  <Textarea {...field} className="h-40 resize-none" />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type="submit" className="self-end">
            Save
          </Button>
        </form>
      </Form>
    </>
  );
};
