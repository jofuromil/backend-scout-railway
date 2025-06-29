import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import MenuFijoGrupo from "../../components/MenuFijoGrupo";

export default function VerDirigentesGrupo() {
  const [dirigentes, setDirigentes] = useState([]);
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get("/api/gruposcout/dirigentes", {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((res) => setDirigentes(res.data))
      .catch(() => alert("No se pudo cargar la lista de dirigentes"));
  }, []);

  const cambiarRol = (id, nuevoEstado) => {
    const url = nuevoEstado
      ? `/api/gruposcout/asignar-admingrupo/${id}`
      : `/api/gruposcout/quitar-admingrupo/${id}`;

    axios
      .put(url, {}, { headers: { Authorization: `Bearer ${token}` } })
      .then(() => {
        alert("Cambio realizado");
        setDirigentes((prev) =>
          prev.map((d) =>
            d.id === id ? { ...d, esAdminGrupo: nuevoEstado } : d
          )
        );
      })
      .catch(() => alert("Error al cambiar rol de administrador"));
  };

  return (
    <div className="min-h-screen pt-20 p-4 pb-24 bg-white">
      <h1 className="text-2xl font-bold mb-4 text-center">
        Dirigentes del Grupo Scout
      </h1>

      <div className="bg-white p-4 rounded-2xl shadow-md">
        {dirigentes.length === 0 ? (
          <p className="text-gray-600 text-center">
            No hay dirigentes registrados en este grupo.
          </p>
        ) : (
          <table className="w-full table-auto border border-gray-300">
            <thead className="bg-violet-200">
              <tr>
                <th className="p-2 border">Nombre</th>
                <th className="p-2 border">Unidad</th>
                <th className="p-2 border">Rama</th>
                <th className="p-2 border">Acciones</th>
              </tr>
            </thead>
            <tbody>
              {dirigentes.map((d) => (
                <tr key={d.id} className="text-center">
                  <td className="border p-2">{d.nombreCompleto}</td>
                  <td className="border p-2">{d.unidadNombre}</td>
                  <td className="border p-2">{d.rama}</td>
                  <td className="border p-2 space-x-2">
                    <button
                      className={`px-2 py-1 rounded ${
                        d.esAdminGrupo
                          ? "bg-gray-400 cursor-not-allowed"
                          : "bg-green-600 hover:bg-green-700 text-white"
                      }`}
                      onClick={() => cambiarRol(d.id, true)}
                      disabled={d.esAdminGrupo}
                    >
                      Hacer Admin
                    </button>
                    <button
                      className={`px-2 py-1 rounded ${
                        !d.esAdminGrupo
                          ? "bg-gray-400 cursor-not-allowed"
                          : "bg-red-600 hover:bg-red-700 text-white"
                      }`}
                      onClick={() => cambiarRol(d.id, false)}
                      disabled={!d.esAdminGrupo}
                    >
                      Quitar Admin
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>

      <div className="mt-6 flex justify-center">
        <button
          className="bg-blue-600 text-white px-4 py-2 rounded-xl shadow hover:bg-blue-700 transition"
          onClick={() => navigate("/grupo")}
        >
          Volver al Panel
        </button>
      </div>

      <MenuFijoGrupo />
    </div>
  );
}
