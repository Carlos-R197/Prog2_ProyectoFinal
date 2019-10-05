using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Perfil
    {
        public string Nombre { get; private set; }
        public string Correo { get; private set; }
        public string Contraseña { get; private set; }
        public byte Edad { get; private set; }
        public short RatingGeneral { get; private set; }

        public Perfil() { }

        public Perfil(string nombre, string correo, string contraseña, byte edad)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Contraseña = contraseña;
            this.Edad = edad;
            this.RatingGeneral = 0;
        }

        public void ImprimirInformacion()
        {
            Console.WriteLine("Nombre: {0}", this.Nombre);
            Console.WriteLine("Correo: {0}", this.Correo);
            Console.WriteLine("Edad: {0}", this.Edad);
            Console.WriteLine("Rating general: {0}", this.RatingGeneral);
        }

        public void ImprimirNombre()
        {
            Console.WriteLine("Nombre: {0}", this.Nombre);
        }

        public void SubirRating()
        {
            RatingGeneral++;
        }
        public void BajarRating()
        {
            RatingGeneral--;
        }
    }
}