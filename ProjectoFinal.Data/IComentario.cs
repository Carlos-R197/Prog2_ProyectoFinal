using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    interface IComentario
    {
        List<Comentario> Comentarios { get; set; }
        void AddComentario(Comentario comentario);
    }
}
