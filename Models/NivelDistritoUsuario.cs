using System;

namespace BackendScout.Models
{
    public class NivelDistritoUsuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Usuario asociado (normalmente un dirigente)
        public Guid UsuarioId { get; set; }
        public User Usuario { get; set; }

        // Distrito asignado
        public int NivelDistritoId { get; set; }
        public NivelDistrito NivelDistrito { get; set; }

        // ¿Es administrador del distrito?
        public bool EsAdminDistrito { get; set; } = false;

        // ¿Fue invitado como organizador a un evento específico?
        public bool EsInvitadoEvento { get; set; } = false;
    }
}
