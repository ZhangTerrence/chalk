import { useLocation } from "react-router-dom";

import type { FileDTO } from "@/lib/types/file.ts";

export default function FilePage() {
  const { state } = useLocation();

  const file = state as FileDTO;

  return (
    <div className="flex h-full w-full flex-col gap-y-2">
      <h1 className="text-xl">
        <strong>{file.name}</strong>
      </h1>
      <p>{file.description}</p>
      <iframe src={file.fileUrl} className="h-screen"></iframe>
    </div>
  );
}
