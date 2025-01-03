import { type PayloadAction, createSlice } from "@reduxjs/toolkit";

import type { RootState } from "@/redux/store.ts";

import { ColorScheme } from "@/lib/theme.ts";

export type ThemeState = {
  colorScheme: ColorScheme;
  darkMode: boolean;
};

const defaultColorScheme = (localStorage.getItem("color-scheme") as ColorScheme) ?? ColorScheme.Zinc;
const defaultDarkMode = localStorage.getItem("dark-mode")
  ? localStorage.getItem("dark-mode") === "true"
  : window.matchMedia("(prefers-color-scheme: dark)").matches;

const root = window.document.documentElement;
root.setAttribute("data-theme", defaultColorScheme);
root.classList.remove("light", "dark");
root.classList.add(defaultDarkMode ? "dark" : "light");

export const themeSlice = createSlice({
  name: "theme",
  initialState: {
    colorScheme: defaultColorScheme,
    darkMode: defaultDarkMode,
  } as ThemeState,
  reducers: {
    changeColorScheme: (state, action: PayloadAction<ColorScheme>) => {
      state.colorScheme = action.payload;

      root.setAttribute("data-theme", state.colorScheme);
      localStorage.setItem("color-scheme", state.colorScheme);
    },
    toggleDarkMode: (state) => {
      state.darkMode = !state.darkMode;

      root.classList.remove("light", "dark");
      root.classList.add(state.darkMode ? "dark" : "light");
      localStorage.setItem("dark-mode", state.darkMode.toString());
    },
  },
});

export default themeSlice.reducer;

export const { changeColorScheme, toggleDarkMode } = themeSlice.actions;

export const selectTheme = (state: RootState) => state.theme;
