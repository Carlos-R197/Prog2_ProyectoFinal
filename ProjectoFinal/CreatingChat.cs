using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;
using System.Linq;

namespace ProjectoFinal
{
    public class CreatingChatState : IState
    {
        StateMachine appState = new StateMachine();

        private Perfil person1;
        private Chat currentChat;

        public CreatingChatState(StateMachine machine, Perfil currentPerfil)
        {
            this.appState = machine;
            this.person1 = currentPerfil;
        }
        public void Enter()
        {
            Console.WriteLine("Escriba el nombre de los perfiles que desea encontrar: ");
            string perfilNombre = Console.ReadLine();
            //Check if that perfil exist, if it does then add it to the chat class.
            Perfil[] perfilesEncontrados = SQLManager.EncuentraPerfilesQueContienen(perfilNombre);
            
            if (perfilesEncontrados.Length > 0)
            {
                foreach (Perfil perfil in perfilesEncontrados)
                {
                    perfil.ImprimirNombre();
                }

                Console.Write("Escriba el nombre del perfil que desea añadir al chat: ");
                string input = Console.ReadLine();

                if (SQLManager.RevisaSiNombreExiste("perfiles_registrados", input))
                {
                    Perfil person2 = new Perfil();

                    for (int i = 0; i < perfilesEncontrados.Length; i++)
                    {
                        if (perfilesEncontrados[i].Nombre == input)
                            person2 = perfilesEncontrados[i];
                    }

                    currentChat = new Chat(person1, person2);
                }
                else
                {
                    Console.WriteLine("Lo escrito no coincide con ningun perfil");
                }
            }
            else
            {
                Console.WriteLine("No se encontro ningun perfil.");
            }
        }
    }
}
