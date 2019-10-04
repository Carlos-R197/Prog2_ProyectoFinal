using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class Perfil
    {
        public string Nombre { get; private set; }
        public string Correo { get; private set; }
        public string Contraseña { get; private set; }


        public void ImprimirInformacion()
        {
            Console.WriteLine("Nombre: {0}", this.Nombre);
            Console.WriteLine("Correo: {0}", this.Correo);
        }
    }
}
