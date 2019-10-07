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
        private Chat currentChat;

        public CreatingChatState(Perfil currentPerfil)
        {
            this.person1 = currentPerfil;
        }
        public void Handle(StateMachine appState)
        {
            Console.WriteLine("Lista de Amigos");
            SQLManager.ObtenerAmigos(this.person1);
            Console.WriteLine();
            Console.WriteLine("Escriba el nombre de los perfiles que desea encontrar: ");
            string perfilNombre = Console.ReadLine();
            //Check if that perfil exist, if it does then add it to the chat class.
            Perfil[] perfilesEncontrados = SQLManager.EncuentraPerfilesQueContienen(perfilNombre);

           


          
        }
    }
}
