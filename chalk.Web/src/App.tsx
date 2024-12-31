import Landing from "@/pages/Landing.tsx";
import Login from "@/pages/Login.tsx";
import Register from "@/pages/Register.tsx";
import { store } from "@/redux/store.ts";
import { Provider } from "react-redux";
import { BrowserRouter, Route, Routes } from "react-router";

export default function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Landing />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}
