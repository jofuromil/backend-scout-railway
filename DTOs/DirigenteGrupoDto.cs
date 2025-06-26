namespace BackendScout.Dtos.Grupo
{
    public class DirigenteGrupoDto
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Rama { get; set; }
        public string Unidad { get; set; }
        public bool EsAdminGrupoScout { get; set; }
    }
}
