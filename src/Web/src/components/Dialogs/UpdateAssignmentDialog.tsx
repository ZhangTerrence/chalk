import React from "react";

import { zodResolver } from "@hookform/resolvers/zod";
import { format } from "date-fns";
import { CalendarIcon } from "lucide-react";
import { useForm } from "react-hook-form";
import { toast } from "sonner";

import { Button } from "@/components/ui/button.tsx";
import { Calendar } from "@/components/ui/calendar.tsx";
import { DialogClose, DialogFooter, DialogHeader, DialogTitle } from "@/components/ui/dialog.tsx";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form.tsx";
import { Input } from "@/components/ui/input.tsx";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover.tsx";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select.tsx";
import { Separator } from "@/components/ui/separator.tsx";
import { Textarea } from "@/components/ui/textarea.tsx";

import { useUpdateAssignmentMutation } from "@/redux/services/course.ts";
import { selectDialog, setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import type { AssignmentDTO } from "@/lib/types/course.ts";
import { cn } from "@/lib/utils.ts";
import { UpdateAssignmentSchema, type UpdateAssignmentType } from "@/lib/validators/course.ts";

export const UpdateAssignmentDialog = () => {
  const dialog = useTypedSelector(selectDialog)!;
  const dispatch = useAppDispatch();
  const [updateAssignment, { isLoading, isSuccess }] = useUpdateAssignmentMutation();

  const assignment = dialog.entity as AssignmentDTO & { courseId: number; assignmentGroupId: number };

  const form = useForm<UpdateAssignmentType>({
    resolver: zodResolver(UpdateAssignmentSchema),
    defaultValues: {
      name: assignment.name,
      description: assignment.description ?? undefined,
      isOpen: assignment.isOpen,
      dueDate: assignment.dueDate ? new Date(assignment.dueDate) : undefined,
      allowedAttempts: assignment.allowedAttempts?.toString(),
    },
  });

  React.useEffect(() => {
    if (!isLoading && isSuccess) {
      dispatch(setDialog(null));
      form.reset();
      toast.success("Successfully edited assignment.");
    }
  }, [isLoading, isSuccess]);

  const onSubmit = async (data: UpdateAssignmentType) => {
    if (
      assignment.name === data.name &&
      (assignment.description ?? undefined) === data.description &&
      assignment.isOpen === data.isOpen &&
      (assignment.dueDate ? new Date(assignment.dueDate).getTime() : undefined) === data.dueDate?.getTime() &&
      assignment.allowedAttempts === (data.allowedAttempts ? Number.parseInt(data.allowedAttempts) : null)
    ) {
      dispatch(setDialog(null));
      return;
    }

    await updateAssignment({
      courseId: assignment.courseId,
      assignmentGroupId: assignment.assignmentGroupId,
      assignmentId: assignment.id,
      data: {
        ...data,
        description: !!data.description ? data.description : undefined,
        allowedAttempts:
          !!data.allowedAttempts && !Number.isNaN(Number.parseInt(data.allowedAttempts))
            ? Number.parseInt(data.allowedAttempts)
            : undefined,
      },
    }).unwrap();
  };

  return (
    <>
      <DialogHeader>
        <DialogTitle>Edit Assignment</DialogTitle>
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
          <FormField
            control={form.control}
            name="isOpen"
            render={({ field }) => (
              <FormItem className="w-full">
                <FormLabel>Status</FormLabel>
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
                    <SelectItem value="true">Open</SelectItem>
                    <SelectItem value="false">Closed</SelectItem>
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="dueDate"
            render={({ field }) => (
              <FormItem className="flex flex-col gap-y-2">
                <FormLabel>Due Date</FormLabel>
                <Popover>
                  <PopoverTrigger asChild>
                    <FormControl>
                      <Button
                        variant={"outline"}
                        className={cn("pl-3 text-left font-normal w-full", !field.value && "text-muted-foreground")}
                      >
                        {field.value ? format(field.value, "PPP") : <span>Pick a date</span>}
                        <CalendarIcon className="ml-auto h-4 w-4 opacity-50" />
                      </Button>
                    </FormControl>
                  </PopoverTrigger>
                  <PopoverContent className="w-auto p-0" align="start">
                    <Calendar
                      mode="single"
                      selected={field.value}
                      onSelect={field.onChange}
                      disabled={(date) => date < new Date()}
                    />
                  </PopoverContent>
                </Popover>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="allowedAttempts"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Allowed Attempts</FormLabel>
                <FormControl>
                  <Input
                    type="number"
                    min={1}
                    value={field.value}
                    onChange={(e) => field.onChange(e.target.value)}
                    className="max-h-40"
                  />
                </FormControl>
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
