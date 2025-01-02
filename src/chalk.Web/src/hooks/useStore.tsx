import type { Dispatch } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";

import type { AppDispatch, RootState } from "../redux/store";

export const useStore = (): [TypedUseSelectorHook<RootState>, () => Dispatch] => {
  const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector;
  const useAppDispatch = () => useDispatch<AppDispatch>();

  return [useTypedSelector, useAppDispatch];
};
