using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class ChattingState : IState
    {
        private readonly Chat currentChat;
        private readonly StateMachine appState;
        public ChattingState(StateMachine machine, Chat chat)
        {
            this.currentChat = chat;
            this.appState = machine;
        }

        public void Enter()
        {
            Console.Clear();
            Console.WriteLine("Presione 1 para salir");

            while (true)
            { 
                string message = Console.ReadLine();
                if (message != "1")
                {
                    Mensaje mensaje = new Mensaje(DateTime.Now, currentChat.Perfil1, message);
                    currentChat.AddMensaje(mensaje);
                }
                else
                    appState.ChangeState(new MainMenuState(appState, currentChat.Perfil1));
            }
        }
    }
}
