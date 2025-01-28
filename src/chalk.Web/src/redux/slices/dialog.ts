import { type PayloadAction, createSlice } from "@reduxjs/toolkit";

import type { RootState } from "@/redux/store.ts";

import { DialogType } from "@/lib/dialogType.ts";

export type DialogState = {
  entity: unknown | null;
  type: DialogType;
} | null;

export const dialogSlice = createSlice({
  name: "dialog",
  initialState: null as DialogState,
  reducers: {
    setDialog: (_, action: PayloadAction<DialogState>) => action.payload,
  },
});

export default dialogSlice.reducer;

export const { setDialog } = dialogSlice.actions;

export const selectDialog = (state: RootState) => state.dialog;
