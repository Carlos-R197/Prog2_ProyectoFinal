using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class ChatState : IState
    {
        StateMachine appState = new StateMachine();

        private Perfil person1;
        private Chat currentChat;

        public ChatState(StateMachine machine, Perfil currentPerfil)
        {
            this.appState = machine;
            this.person1 = currentPerfil;
        }
        public void Enter()
        {
            Console.WriteLine("Escribe el nombre de la persona que quieres invitar al chat: ");
            string perfilNombre = Console.ReadLine();
            //Check if that perfil exist, if it does then add it to the chat class.
            if (SQLManager.RevisaSiNombreExiste("perfiles_registrados", perfilNombre))
            {
                //Perfil newPerfil
            }
            else
            {
                Console.WriteLine("Ese perfil no existe");
            }
        }
    }
}
