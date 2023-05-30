"use client";
import { useEffect, useState } from "react";

//form : use-hook-form  && validate field : yup

//

export default function Home() {
  const [loading, SetLoading] = useState(false);

  const HandleCick = (load: any) => {
    SetLoading(!load);
  };
  useEffect(() => {
    console.log(loading);
  }, [loading]);
  return (
    <main className="flex flex-col items-center justify-between min-h-screen p-24 bg-white">
      <div className="text-black">Home</div>
      <button onClick={() => HandleCick(loading)}>Click</button>
    </main>
  );
}
