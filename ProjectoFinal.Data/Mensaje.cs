using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Mensaje
    {
        public DateTime HoraEnvio { get; set; }
        public Perfil Perfil { get; set; }
        public string SMS { get; set; }

        public Mensaje() { }

        public Mensaje(DateTime horaEnvio, Perfil perfil, string sms)
        {
            this.HoraEnvio = horaEnvio;
            this.Perfil = perfil;
            this.SMS = sms;
        }
    }
}
