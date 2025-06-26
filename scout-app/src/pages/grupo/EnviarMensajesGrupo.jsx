// src/pages/grupo/EnviarMensajesGrupo.jsx
import React from "react";
import MenuFijoGrupo from "../../components/MenuFijoGrupo";

export default function EnviarMensajesGrupo() {
  return (
    <div className="p-4 mt-16">
      <h2 className="text-2xl font-bold mb-4">Enviar Mensajes del Grupo</h2>
      <p className="text-gray-700">Esta sección permitirá enviar mensajes a scouts, dirigentes o todos los miembros del grupo.</p>
      <MenuFijoGrupo />
    </div>
  );
}
