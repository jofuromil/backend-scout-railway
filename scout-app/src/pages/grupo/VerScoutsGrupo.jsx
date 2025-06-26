import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import MenuFijo from "../../components/MenuFijoGrupo";

export default function VerScoutsGrupo() {
  const [scouts, setScouts] = useState([]);
  const navigate = useNavigate();

  const token = localStorage.getItem("token");
  const usuarioId = localStorage.getItem("usuarioId");

  useEffect(() => {
    if (!token) return navigate("/login");

    axios
      .get(`/api/gruposcout/ver-scouts/${usuarioId}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then((res) => setScouts(res.data))
      .catch(() => alert("Error al obtener scouts del grupo"));
  }, []);

  return (
    <div className="p-4 pb-20 lg:pb-4">
      <h2 className="text-2xl font-bold mb-4 text-center">Scouts del Grupo</h2>
      {scouts.length === 0 ? (
        <p className="text-center">No hay scouts registrados en este grupo.</p>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {scouts.map((scout) => (
            <div key={scout.id} className="border rounded-xl shadow p-4">
              <p><strong>Nombre:</strong> {scout.nombreCompleto}</p>
              <p><strong>Correo:</strong> {scout.correo}</p>
              <p><strong>Rama:</strong> {scout.rama}</p>
              <p><strong>Unidad:</strong> {scout.unidadNombre}</p>
            </div>
          ))}
        </div>
      )}
      <MenuFijo tipo="Grupo" />
    </div>
  );
}
