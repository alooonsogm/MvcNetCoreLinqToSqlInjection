namespace MvcNetCoreLinqToSqlInjection.Models
{
    public interface ICoche
    {
        //La interface no tiene ambito de metodo, no llleva ni public, private, solo la declaracion
        string Marca { get; set; }
        string Modelo { get; set; }
        string Imagen { get; set; }
        int Velocidad { get; set; }
        int VelocidadMax { get; set; }

        void Acelerar();
        void Frenar();
    }
}
