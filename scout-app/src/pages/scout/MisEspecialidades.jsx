import { useEffect, useState } from "react";
import MenuFijo from "@/components/MenuFijo";

export default function MisEspecialidades() {
  const [especialidades, setEspecialidades] = useState([]);
  const token = localStorage.getItem("token");

  useEffect(() => {
    fetch("/api/Especialidades/mis-avances", {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al obtener avances");
        return res.json();
      })
      .then((data) => setEspecialidades(data))
      .catch((err) => console.error(err));
  }, [token]);

  return (
    <div className="min-h-screen bg-white pb-20">
      {/* Menú fijo superior en pantallas grandes */}
      <div className="hidden lg:block fixed top-0 left-0 right-0 z-50">
        <MenuFijo />
      </div>

      <div className="max-w-3xl mx-auto pt-6 px-4">
        <h2 className="text-2xl font-bold mb-4">⭐ Mis Especialidades</h2>

        {especialidades.length === 0 ? (
          <p>No has iniciado ninguna especialidad aún.</p>
        ) : (
          <ul className="grid gap-3">
            {especialidades.map((esp) => (
              <li
                key={esp.especialidadId}
                className="p-4 border rounded bg-gray-50 shadow"
              >
                <div className="mb-2">
                  <div className="font-semibold text-lg">{esp.nombre}</div>
                  <div className="text-sm text-gray-700">
                    Requisitos seleccionados: <strong>{esp.seleccionados}</strong> / Aprobados: <strong>{esp.aprobados}</strong>
                  </div>
                </div>
                <div className="text-sm">
                  {esp.cumplida ? (
                    <div className="text-green-700 font-semibold">
                      ✔ Cumplida
                      {esp.fechaCumplida && (
                        <span className="block text-xs text-gray-600">Fecha: {new Date(esp.fechaCumplida).toLocaleDateString()}</span>
                      )}
                    </div>
                  ) : (
                    <span className="text-yellow-700 font-medium">⏳ En progreso</span>
                  )}
                </div>
              </li>
            ))}
          </ul>
        )}
      </div>

      {/* Menú fijo inferior en móviles */}
      <div className="lg:hidden fixed bottom-0 left-0 right-0 z-50">
        <MenuFijo />
      </div>
    </div>
  );
}
