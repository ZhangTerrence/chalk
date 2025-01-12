import React, { useState } from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar.tsx";
import { Button } from "@/components/ui/button.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateUserMutation } from "@/redux/services/user.ts";
import { selectUser } from "@/redux/slices/user.ts";
import { useTypedSelector } from "@/redux/store.ts";

import { UpdateUserSchema, type UpdateUserType } from "@/lib/validators/updateUser.ts";

function getImageData(event: React.ChangeEvent<HTMLInputElement>) {
  // FileList is immutable, so we need to create a new one
  const dataTransfer = new DataTransfer();

  // Add newly uploaded images
  Array.from(event.target.files!).forEach((image) => dataTransfer.items.add(image));

  const files = dataTransfer.files;
  const displayUrl = URL.createObjectURL(event.target.files![0]);

  return { files, displayUrl };
}

export const ProfileSection = () => {
  const user = useTypedSelector(selectUser)!;
  const [image, setImage] = useState<string | null>(user.profilePicture);

  const form = useForm<UpdateUserType>({
    resolver: zodResolver(UpdateUserSchema),
    defaultValues: {
      firstName: user.firstName,
      lastName: user.lastName,
      displayName: user.displayName,
      description: user.description ?? undefined,
      profilePicture: user.profilePicture ?? undefined,
    },
  });

  const fullName = `${user.firstName} ${user.lastName}`;

  const [updateUser] = useUpdateUserMutation();

  return (
    <div className="grow p-4 pt-20 pl-4 flex flex-col gap-y-4">
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(
            async (data) =>
              await updateUser({
                firstName: data.firstName ?? null,
                lastName: data.lastName ?? null,
                displayName: data.displayName ?? null,
                description: data.description ?? null,
                profilePicture: image,
              }).unwrap(),
          )}
          className="flex flex-col space-y-4"
        >
          <div className="flex space-x-4 w-full">
            <FormField
              control={form.control}
              name="profilePicture"
              render={({ field: { onChange, value, ...rest } }) => (
                <FormItem className="aspect-square h-full">
                  <FormControl>
                    <div className="relative h-full w-full">
                      <Avatar className="h-full w-full rounded-full">
                        <AvatarImage
                          src={image ?? undefined}
                          alt={fullName}
                          className="object-center object-contain max-w-40 h-auto"
                        />
                        <AvatarFallback className="rounded-lg text-2xl">
                          {fullName.charAt(0).toUpperCase()}
                        </AvatarFallback>
                      </Avatar>
                      <Input
                        type="file"
                        {...rest}
                        onChange={(event) => {
                          const { displayUrl } = getImageData(event);
                          setImage(displayUrl);
                          onChange(displayUrl);
                        }}
                        className="absolute h-full w-full top-0 opacity-0 hover:cursor-pointer"
                      />
                    </div>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
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
