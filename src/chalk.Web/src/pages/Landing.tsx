import { Helmet } from "react-helmet-async";

import { Header } from "@/components/Header.tsx";

export default function Landing() {
  return (
    <div className="min-h-screen w-screen flex items-center justify-center">
      <Helmet>
        <title>Chalk - Landing</title>
      </Helmet>
      <Header />
      <main className="flex flex-col gap-y-2 items-center">
        <h1 className="text-4xl">
          <strong>Chalk</strong>
        </h1>
        <p>A learning management system.</p>
      </main>
    </div>
  );
}
