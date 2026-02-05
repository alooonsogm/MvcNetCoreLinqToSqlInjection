namespace MvcNetCoreLinqToSqlInjection.Models
{
    public class Coche: ICoche
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Imagen { get; set; }
        public int Velocidad { get; set; }
        public int VelocidadMax { get; set; }

        public Coche()
        {
            this.Marca = "Cochecito";
            this.Modelo = "Normal";
            this.Imagen = "bugatti.jpg";
            this.Velocidad = 0;
            this.VelocidadMax = 120;
        }

        public void Acelerar()
        {
            this.Velocidad += 20;
            if(this.Velocidad >= this.VelocidadMax)
            {
                this.Velocidad = this.VelocidadMax;
            }
        }

        public void Frenar()
        {
            this.Velocidad -= 10;
            if(this.Velocidad < 0)
            {
                this.Velocidad = 0;
            }
        }
    }
}
