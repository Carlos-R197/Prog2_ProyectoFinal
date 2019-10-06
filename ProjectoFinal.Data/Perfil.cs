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
        public int RatingGeneral { get; private set; }

        public Perfil() { }

        public Perfil(string nombre, string correo, string contraseña, byte edad, int rating)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Contraseña = contraseña;
            this.Edad = edad;
            this.RatingGeneral = rating;
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
        public void ModificarInfo()
        {
            Console.Clear();
            Console.WriteLine("Que dato desea modificar?");
            Console.WriteLine("1. Nombre y apellido");
            Console.WriteLine("2. Contraseña");
            Console.WriteLine("3. Edad");
            byte desicion;
            if (byte.TryParse(Console.ReadLine(), out desicion))
            {
                switch (desicion)
                {
                    case 1:
                        Console.Write("Escriba su nuevo Nombre: ");
                        this.Nombre = Console.ReadLine();
                        return;
                    case 2:
                        Console.Write("Escriba su nueva Constraseña: ");
                        this.Contraseña = Console.ReadLine();
                        return;
                    case 3:
                        Console.WriteLine("Modifique su Edad: ");
                        this.Edad = byte.Parse(Console.ReadLine());
                        return;
                }
            }
        }
    }
}