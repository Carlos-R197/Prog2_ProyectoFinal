using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class CreatingCirculoState : IState
    {
        private StateMachine appState;

        public CreatingCirculoState(StateMachine appState)
        {
            this.appState = appState;
        }

        public void Enter()
        {
            Console.WriteLine("¿Qué nombre desea para el círculo? ");
            string nombreCirculo = Console.ReadLine();

            //Check if the circle alredy exists, if it doesnt, create it otherwise throw an error.
            if (!SQLManager.RevisaSiNombreExiste("circulos", nombreCirculo))
            {
                Circulo nuevoCirculo = new Circulo(nombreCirculo);
                SQLManager.AñadirCirculo(nuevoCirculo);
            }
            else
            {
                Console.WriteLine("Ese círculo ya existe.");
            }
        }
    }
}
