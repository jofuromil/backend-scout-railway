namespace BackendScout.Helpers
{
    public static class GruposPorDistrito
    {
        // Diccionario de distritos y sus grupos permitidos (en mayúsculas)
        public static readonly Dictionary<string, List<string>> Grupos = new()
        {
            ["PANDO"] = new List<string> { "COBIJA", "PUERTO" },
            ["BENI"] = new List<string> { "TROPA AVALON" },
            ["CHUQUISACA"] = new List<string> { "JUNIN", "SAGRADO CORAZON", "DON BOSCO SUCRE", "LA SALLE", "ALEMÁN SUCRE", "HEIMDALL", "APACHE" },
            ["COCHABAMBA"] = new List<string>
            {
                "BROWNSEA", "TUNARI", "SAINT ANDREW´S", "MURRAY DICKSON", "LOYOLA", "ANGLO AMERICANO",
                "ESPAÑA", "LA SALLE", "ALEMÁN", "MAFEKING", "PANDA", "INTIDRAC", "INCAS", "KAIROS",
                "CEIBO", "AMÉRICA", "IMPEESA", "PRIMAVERA", "FORTALEZA", "BOLIVIA"
            },
            ["LA PAZ"] = new List<string>
            {
                "LOYOLA SAN CALIXTO", "AMERINST 301", "LOS ROBLES", "LOYOLA SAN IGNACIO",
                "NAVAL CRUX-UENHP", "HANS PHILIPPSBERG SAINT ANDREW'S SCHOOL", "BOLIVIANO ISRAELITA",
                "LOS PINOS", "IMPEESA", "NAVAL ALMTE MIGUEL GRAU S."
            },
            ["ORURO"] = new List<string>
            {
                "VIKING´S", "DRAGONES", "CAP. USTARIZ", "SAN FRANCISCO", "LA SALLE \"HNO. JUAN FROMENTAL\"",
                "CAMACHO", "MEJILLONES"
            },
            ["POTOSI"] = new List<string> { "MAFEKING", "TORRE FUERTE", "ALEMAN SAN PEDRO" },
            ["SANTA CRUZ"] = new List<string>
            {
                "AMBORÓ", "MARISTA", "ARGENTINO BOLIVIANO", "JUAN PABLO II", "LA SANTA CRUZ", "SANTO TOMÁS",
                "DON BOSCO", "SAN ANDRES", "SANTA ANA", "GASTÓN GUILLAUX"
            },
            ["TARIJA"] = new List<string> { "SAN MARTIN DE PORRES", "LA SALLE" }
        };

        public static bool DistritoExiste(string distrito)
        {
            return Grupos.ContainsKey(distrito.Trim().ToUpper());
        }

        public static bool GrupoValido(string distrito, string grupo)
        {
            var distritoKey = distrito.Trim().ToUpper();
            var grupoKey = grupo.Trim().ToUpper();

            return DistritoExiste(distritoKey) && Grupos[distritoKey].Contains(grupoKey);
        }
    }
}
