using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;
using System.Linq;

namespace ProjectoFinal
{
    public class CreatingChatState : IState
    {
        private Perfil person1;

        public CreatingChatState(Perfil currentPerfil)
        {
            this.person1 = currentPerfil;
        }
        public void Handle(StateMachine appState)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Lista de Amigos");
                Console.WriteLine();
                SQLManager.ObtenerAmigos(this.person1);
                Console.WriteLine();
                Console.WriteLine("1. Volver");
                Console.WriteLine("Escriba el nombre de los perfiles que desea encontrar: ");
                Console.Write("R: ");
                string amigo = Console.ReadLine();
                if (SQLManager.ValidarExistenciaAmigo(person1.Nombre, amigo) == true)
                {
                    Perfil persona2 = new Perfil(amigo, "", "", 0, 0);
                    //Check if that perfil exist, if it does then add it to the chat class.
                    Chat createchat = new Chat(person1, persona2);
                    appState.ChangeState(new ChattingState(createchat, amigo));
                }
                else if(amigo == "1")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("El Usuario no esta en su lista de amigo");
                    Console.ReadKey();
                    continue;
                }
                
            }     
        }
    }
}
