using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class ChattingState : IState
    {
        private readonly Chat currentChat;
        public ChattingState(Chat chat)
        {
            this.currentChat = chat;
        }

        public void Handle(StateMachine appState)
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
                    appState.ChangeState(new MainMenuState(currentChat.Perfil1));
            }
        }
    }
}
