﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class SignUpState : IState
    {
        public void Handle(StateMachine appState)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Escriba sus nombres y apellidos: ");
                string nombreCompleto = Console.ReadLine();
                Console.Write("Escriba su contraseña: ");
                string contraseña = Console.ReadLine();
                Console.Write("Escriba su correo: ");
                string correo = Console.ReadLine(); //Make it check if the email is valid.Si
                if(!(correo.Contains("@") && correo.Contains(".com")))
                {
                    Console.WriteLine("Correo invalido");
                    Console.ReadLine();
                    continue;
                }
                Console.Write("Escriba su edad: ");
                byte edad;
                
                if (byte.TryParse(Console.ReadLine(), out edad))
                {
                    Perfil newPerfil = new Perfil(nombreCompleto, correo, contraseña, edad, 0);
                    string query = "INSERT perfiles_registrados (nombre,contrasena,correo,edad) VALUE ('" + newPerfil.Nombre + "','" + newPerfil.Contraseña + "','" + newPerfil.Correo + "','" + newPerfil.Edad + "')";
                    SQLManager.EjecutarQuery(query);
                    Console.WriteLine("Usuario registrado");
                    appState.ChangeState(new InicioState());
                }
                else
                    continue; //Make the user write everything again.
            }

            //Create a instace of perfil, save the data to the database and change the state.
        }
    }
}
