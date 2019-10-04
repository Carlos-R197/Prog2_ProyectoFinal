using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    interface IComentario
    {
        List<Comentario> Comentarios { get; set; }
        void AddComentario(Comentario comentario);
    }
}
