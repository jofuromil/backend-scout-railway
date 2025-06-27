import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate, Link } from "react-router-dom";
import MenuFijo from "../../components/MenuFijoGrupo";

const GestionGrupoPage = () => {
  const [dirigentes, setDirigentes] = useState([]);
  const [scouts, setScouts] = useState([]);
  const [agrupadosDirigentes, setAgrupadosDirigentes] = useState({});
  const [agrupadosScouts, setAgrupadosScouts] = useState({});

  const navigate = useNavigate();
  const token = localStorage.getItem("token");
  const usuarioId = localStorage.getItem("usuarioId");

  useEffect(() => {
    if (!token) return navigate("/login");

    const fetchData = async () => {
      try {
        const config = {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        };

        const [resDirigentes, resScouts] = await Promise.all([
          axios.get(`/api/gruposcout/dirigentes`, config),
          axios.get(`/api/gruposcout/ver-scouts/${usuarioId}`, config),
        ]);

        setDirigentes(resDirigentes.data);
        setScouts(resScouts.data);

        // Agrupar por unidad
        const dPorUnidad = {};
        resDirigentes.data.forEach((d) => {
          const unidad = d.unidad || "Sin unidad";
          if (!dPorUnidad[unidad]) dPorUnidad[unidad] = [];
          dPorUnidad[unidad].push(d);
        });
        setAgrupadosDirigentes(dPorUnidad);

        const sPorUnidad = {};
        resScouts.data.forEach((s) => {
          const unidad = s.unidad || "Sin unidad";
          if (!sPorUnidad[unidad]) sPorUnidad[unidad] = [];
          sPorUnidad[unidad].push(s);
        });
        setAgrupadosScouts(sPorUnidad);
      } catch (error) {
        console.error("Error al cargar miembros del grupo:", error);
      }
    };

    fetchData();
  }, [token, usuarioId, navigate]);

  return (
    <div className="p-4 mt-20">
      <MenuFijo />
      <h2 className="text-2xl font-bold mb-6">Gestión del Grupo Scout</h2>

      {/* DIRIGENTES */}
      <h3 className="text-xl font-semibold mt-8 mb-2">Dirigentes por Unidad</h3>
      {Object.keys(agrupadosDirigentes).map((unidad) => (
        <div key={unidad} className="mb-6">
          <h4 className="font-bold text-lg mb-2">{unidad}</h4>
          <div className="overflow-x-auto">
            <table className="table-auto border w-full text-sm">
              <thead className="bg-gray-100">
                <tr>
                  <th className="border px-2 py-1">CI</th>
                  <th className="border px-2 py-1">Nombre</th>
                  <th className="border px-2 py-1">Fecha Nac.</th>
                  <th className="border px-2 py-1">Género</th>
                  <th className="border px-2 py-1">Rama</th>
                  <th className="border px-2 py-1">Profesión</th>
                  <th className="border px-2 py-1">Ocupación</th>
                </tr>
              </thead>
              <tbody>
                {agrupadosDirigentes[unidad].map((d) => (
                  <tr key={d.id}>
                    <td className="border px-2 py-1">{d.ci || "-"}</td>
                    <td className="border px-2 py-1">{d.nombreCompleto}</td>
                    <td className="border px-2 py-1">{d.fechaNacimiento?.split("T")[0]}</td>
                    <td className="border px-2 py-1">{d.genero}</td>
                    <td className="border px-2 py-1">{d.rama}</td>
                    <td className="border px-2 py-1">{d.profesion || "-"}</td>
                    <td className="border px-2 py-1">{d.ocupacion || "-"}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      ))}

      {/* SCOUTS */}
      <h3 className="text-xl font-semibold mt-10 mb-2">Scouts por Unidad</h3>
      {Object.keys(agrupadosScouts).map((unidad) => (
        <div key={unidad} className="mb-6">
          <h4 className="font-bold text-lg mb-2">{unidad}</h4>
          <div className="overflow-x-auto">
            <table className="table-auto border w-full text-sm">
              <thead className="bg-gray-100">
                <tr>
                  <th className="border px-2 py-1">CI</th>
                  <th className="border px-2 py-1">Nombre</th>
                  <th className="border px-2 py-1">Fecha Nac.</th>
                  <th className="border px-2 py-1">Género</th>
                  <th className="border px-2 py-1">Rama</th>
                  <th className="border px-2 py-1">Colegio</th>
                  <th className="border px-2 py-1">Curso</th>
                </tr>
              </thead>
              <tbody>
                {agrupadosScouts[unidad].map((s) => (
                  <tr key={s.id}>
                    <td className="border px-2 py-1">{s.ci || "-"}</td>
                    <td className="border px-2 py-1">{s.nombreCompleto}</td>
                    <td className="border px-2 py-1">{s.fechaNacimiento?.split("T")[0]}</td>
                    <td className="border px-2 py-1">{s.genero}</td>
                    <td className="border px-2 py-1">{s.rama}</td>
                    <td className="border px-2 py-1">{s.institucionEducativa || "-"}</td>
                    <td className="border px-2 py-1">{s.nivelEstudios || "-"}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      ))}

      <div className="mt-10">
        <Link to="/grupo" className="text-blue-600 underline">
          ← Volver al Panel del Grupo
        </Link>
      </div>
    </div>
  );
};

export default GestionGrupoPage;
