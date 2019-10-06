using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class DentroCirculoState : IState
    {
        private Perfil currentPerfil;
        private Circulo currentCirculo;

        public DentroCirculoState(Perfil perfil, Circulo circulo)
        {
            this.currentPerfil = perfil;
            this.currentCirculo = circulo;
        }

        public void Handle(StateMachine appState)
        {
            Console.Clear();
            Console.WriteLine("Has entrado al circulo: {0}", currentCirculo.Nombre);

            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Crear un post");
            byte input = byte.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Console.WriteLine("¿Cuál sera el nombre del post?");
                    string nombrePost = Console.ReadLine();
                    break;
            }
        }
    }
}
