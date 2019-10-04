using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    interface IComentario
    {
        short RatingPost { get; set; }
        List<Comentario> Comentarios { get; set; }
        void AddComentario(Comentario comentario);
        void SubirRating();
        void BajarRating();
    }
}
