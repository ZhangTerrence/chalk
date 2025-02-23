import { PayloadAction, createSlice } from "@reduxjs/toolkit";

import { RootState } from "@/redux/store.ts";

import FileResponse from "@/lib/api/responses/FileResponse.ts";

export type FileState = FileResponse | null;

export const fileSlice = createSlice({
  name: "file",
  initialState: null as FileState,
  reducers: {
    setFile: (_, action: PayloadAction<FileState>) => action.payload,
  },
});

export default fileSlice.reducer;

export const { setFile } = fileSlice.actions;

export const selectFile = (state: RootState) => state.file;
