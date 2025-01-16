import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { Button } from "@/components/ui/button.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateUserMutation } from "@/redux/services/user.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

import { getImageData } from "@/lib/utils.ts";
import { UpdateUserSchema, type UpdateUserType } from "@/lib/validators/updateUser.ts";

export const ProfileSection = () => {
  const user = useTypedSelector(selectUser)!;
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
    const formData = new FormData();

    for (const [key, value] of Object.entries(data)) {
      formData.append(key, value);
    }
    if (uploadedProfilePicture) {
      formData.append("profilePicture", uploadedProfilePicture);
    }

    await updateUser(formData).unwrap();
  };

  return (
    <div className="grow p-4 pt-20 pl-4 flex flex-col gap-y-4">
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col space-y-4">
          <div className="flex space-x-4 w-full">
            <div className="relative h-full aspect-square">
              <Avatar className="h-full w-full rounded-full">
                <AvatarImage
                  src={profilePicture}
                  alt={fullName}
                  className="object-center object-contain max-w-40 h-auto"
                />
                <AvatarFallback className="rounded-lg text-2xl">{fullName.charAt(0).toUpperCase()}</AvatarFallback>
              </Avatar>
              <Input
                type="file"
                accept="image/*"
                onChange={onFileUpload}
                className="absolute h-full w-full top-0 opacity-0 hover:cursor-pointer"
              />
            </div>
            <div className="grow flex flex-col space-y-2">
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
                  <Textarea {...field} className="max-h-60" />
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
    </div>
  );
};
