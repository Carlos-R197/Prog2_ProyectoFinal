﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class SignUpState : IState
    {
        private StateMachine appState;

        public SignUpState(StateMachine machine)
        {
            this.appState = machine;
        }

        public void Enter()
        {
            Console.Write("Escriba sus nombres y apellidos: ");
            string nombreCompleto = Console.ReadLine();
            Console.Write("Escriba su contraseña: ");
            string contraseña = Console.ReadLine();
            Console.Write("Escriba su correo: ");
            string correo = Console.ReadLine(); //Make it check if the email is valid.

            //Create a instace of perfil, save the data to the database and change the state.
        }
    }
}