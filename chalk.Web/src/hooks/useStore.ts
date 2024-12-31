import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";

import type { AppDispatch, RootState } from "../redux/store";

export const useStore = () => {
  const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector;
  const useAppDispatch = () => useDispatch<AppDispatch>();

  return [useTypedSelector, useAppDispatch];
};
