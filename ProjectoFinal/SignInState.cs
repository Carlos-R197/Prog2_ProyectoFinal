using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class SignInState : IState
    {
        private StateMachine appState;

        public SignInState(StateMachine machine)
        {
            this.appState = machine;
        }

        public void Enter()
        {
            Console.Write("Escriba sus nombres y apellidos: ");
            string nombreCompleto = Console.ReadLine();
            Console.Write("Escriba su contraseña: ");
            string contraseña = Console.ReadLine();
            //Check if the perfil exist in the database y log in if it does.
        }
    }
}
