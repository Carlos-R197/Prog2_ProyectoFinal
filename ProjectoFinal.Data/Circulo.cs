using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Circulo
    {
        public string Nombre { get; private set; }
        public List<Perfil> PerfilesSuscritos { get; set; }
        public List<Post> PostsPublicados { get; set; }

        public Circulo()
        {

        }

        public Circulo(string nombre)
        {
            this.Nombre = nombre;
        }

        public Circulo(string nombre, List<Perfil> perfilesSuscritos, List<Post> postsPublicados)
        {
            this.Nombre = nombre;
            this.PerfilesSuscritos = perfilesSuscritos;
            this.PostsPublicados = postsPublicados;
        }
    }
}
