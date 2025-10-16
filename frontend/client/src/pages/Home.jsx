import React from "react";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";

export default function Home() {
  return (
    <div className="flex flex-col min-h-screen">
      <Navbar />
      <main className="flex-grow p-4">
        <section className="text-center bg-[#f4f4f4] py-10">
          <h1 className="text-2xl font-bold mb-2">DESCUBRE LA MODA MÁS ACTUAL</h1>
          <p>Encuentra las últimas tendencias de la temporada en nuestra tienda</p>
          <button className="mt-4 bg-black text-white px-4 py-2">¡Compra ahora!</button>
        </section>
      </main>
      <Footer />
    </div>
  );
}