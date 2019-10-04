using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    class Chat
    {
        public Perfil Perfil1 { get; set; }
        public Perfil Perfil2 { get; set; }
        public List<Mensaje> Conversacion { get; set; }

        public void AddMensaje(Mensaje mensaje)
        {
            Conversacion.Add(mensaje);
        }
        public Chat(Perfil perfil1, Perfil perfil2)
        {
            this.Perfil1 = perfil1;
            this.Perfil2 = perfil2;
        }
    }
}
