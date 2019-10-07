using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Chat
    {
        public Perfil Perfil1 { get; set; }
        public Perfil Perfil2 { get; set; }
        public List<string> Conversacion { get; set; }

       
        public void AddMensaje(string mensaje)
        {
            Conversacion = new List<string>();
            Conversacion.Add(mensaje);
        }
        public Chat(Perfil perfil1, Perfil perfil2)
        {
            this.Perfil1 = perfil1;
            this.Perfil2 = perfil2;
        }
    }
}
