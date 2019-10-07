using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class ChattingState : IState
    {
        private readonly Chat currentChat;
        private string perfil;
        public ChattingState(Chat chat,string perfil)
        {
            this.currentChat = chat;
            this.perfil = perfil;
        }

        public void Handle(StateMachine appState)
        {
            
            Console.Clear();
            Console.WriteLine("Chat con: " + perfil);
            Console.WriteLine("Presione 1 para salir");
            Console.WriteLine();
            SQLManager.VerMensajes(currentChat);
            while (true)
            { 
                string message = Console.ReadLine();
                if (message != "1")
                {
                    Mensaje mensaje = new Mensaje(DateTime.Now, currentChat.Perfil1, message);
                    currentChat.AddMensaje(message);
                    //currentChat.Conversacion.Add(mensaje);
                    SQLManager.GuardarChat(currentChat);                    
                }
                else
                    appState.ChangeState(MainMenuState.Instance);
            }
        }
    }
}
