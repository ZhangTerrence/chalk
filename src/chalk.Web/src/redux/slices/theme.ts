import { type PayloadAction, createSlice } from "@reduxjs/toolkit";

import type { RootState } from "@/redux/store.ts";

import { ColorScheme } from "@/lib/theme.ts";

export type ThemeState = {
  colorScheme: ColorScheme;
  darkMode: boolean;
};

const root = window.document.documentElement;
root.classList.remove("light", "dark");
const defaultDarkMode = localStorage.getItem("darkMode")
  ? localStorage.getItem("darkMode") === "true"
  : window.matchMedia("(prefers-color-scheme: dark)").matches;
root.classList.add(defaultDarkMode ? "dark" : "light");

export const themeSlice = createSlice({
  name: "theme",
  initialState: {
    colorScheme: ColorScheme.Zinc,
    darkMode: defaultDarkMode,
  } as ThemeState,
  reducers: {
    changeColorScheme: (state, action: PayloadAction<ColorScheme>) => {
      state.colorScheme = action.payload;
    },
    toggleDarkMode: (state) => {
      root.classList.remove("light", "dark");
      state.darkMode = !state.darkMode;
      root.classList.add(state.darkMode ? "dark" : "light");
      localStorage.setItem("darkMode", state.darkMode.toString());
    },
  },
});

export default themeSlice.reducer;

export const { changeColorScheme, toggleDarkMode } = themeSlice.actions;

export const selectTheme = (state: RootState) => state.theme;
