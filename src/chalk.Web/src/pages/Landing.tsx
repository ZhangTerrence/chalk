import { Helmet } from "react-helmet-async";

export default function Landing() {
  return (
    <>
      <Helmet>
        <title>Chalk - Landing</title>
      </Helmet>
      <main className="flex flex-col gap-y-2 items-center">
        <h1 className="text-4xl">
          <strong>Chalk</strong>
        </h1>
        <p>A learning management system.</p>
      </main>
    </>
  );
}
