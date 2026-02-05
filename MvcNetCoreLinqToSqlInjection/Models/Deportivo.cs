namespace MvcNetCoreLinqToSqlInjection.Models
{
    public class Deportivo: ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMax { get; set; }

        public Deportivo()
        {
            this.Marca = "Ferrari";
            this.Modelo = "Rojo";
            this.Imagen = "ferrari.jpg";
            this.Velocidad = 0;
            this.VelocidadMax = 300;
        }

        public void Acelerar()
        {
            this.Velocidad += 50;
            if (this.Velocidad >= this.VelocidadMax)
            {
                this.Velocidad = this.VelocidadMax;
            }
        }

        public void Frenar()
        {
            this.Velocidad -= 25;
            if (this.Velocidad < 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
