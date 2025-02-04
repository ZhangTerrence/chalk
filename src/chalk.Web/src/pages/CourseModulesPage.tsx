import React from "react";

import { DragDropContext, Draggable, type DropResult, Droppable } from "@hello-pangea/dnd";
import { PlusIcon } from "lucide-react";

import NotFoundPage from "@/pages/NotFoundPage.tsx";

import { Button } from "@/components/ui/button.tsx";

import { Module } from "@/components/Course/Module.tsx";

import { useReorderModulesMutation } from "@/redux/services/course.ts";
import { selectCourse } from "@/redux/slices/course.ts";
import { setDialog } from "@/redux/slices/dialog.ts";
import { useAppDispatch, useTypedSelector } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";
import { reorder } from "@/lib/utils.ts";

export default function CourseModulesPage() {
  const course = useTypedSelector(selectCourse);
  const dispatch = useAppDispatch();
  const [reorderModules] = useReorderModulesMutation();

  if (!course) {
    return <NotFoundPage />;
  }

  const [modules, setModules] = React.useState([...course.modules].sort((a, b) => a.relativeOrder - b.relativeOrder));

  const onDragEnd = (result: DropResult) => {
    if (result.combine) {
      const newModules = [...modules];
      newModules.splice(result.source.index, 1);
      setModules(newModules);
      return;
    }

    if (!result.destination) {
      return;
    }

    if (result.destination.index === result.source.index) {
      return;
    }

    const newModules = reorder(modules, result.source.index, result.destination.index);
    setModules(newModules);
    reorderModules({
      courseId: course.id,
      data: {
        modules: newModules.map((e) => e.id),
      },
    });
  };

  return (
    <div className="flex h-full w-full flex-col gap-y-4 justify-between">
      <DragDropContext onDragEnd={onDragEnd}>
        <Droppable droppableId="droppable">
          {(provided) => (
            <div {...provided.droppableProps} ref={provided.innerRef} className="flex flex-col gap-y-4">
              {modules.map((module, i) => {
                return (
                  <Draggable key={module.id} draggableId={module.id.toString()} index={i}>
                    {(provided) => (
                      <div ref={provided.innerRef} {...provided.draggableProps} {...provided.dragHandleProps}>
                        <Module courseId={course.id} data={module} />
                      </div>
                    )}
                  </Draggable>
                );
              })}
            </div>
          )}
        </Droppable>
      </DragDropContext>
      <Button variant="outline" onClick={() => dispatch(setDialog({ entity: course, type: DialogType.CreateModule }))}>
        <span className="flex items-center gap-x-2">
          <PlusIcon />
          <p>Create module</p>
        </span>
      </Button>
    </div>
  );
}
