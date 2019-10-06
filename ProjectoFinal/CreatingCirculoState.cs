using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class CreatingCirculoState : IState
    {
        public void Handle(StateMachine appState)
        {
            Console.Write("¿Qué nombre desea para el círculo? ");
            string nombreCirculo = Console.ReadLine();

            //Check if the circle alredy exists, if it doesnt, create it otherwise throw an error.
            if (!SQLManager.RevisaSiNombreExiste("circulos", nombreCirculo))
            {
                Circulo nuevoCirculo = new Circulo(nombreCirculo);
                string query1 = "INSERT INTO circulos VALUE( '" + nuevoCirculo.Nombre + "')";
                string query2 = "CREATE TABLE circulo" + nuevoCirculo.Nombre + "(nombre VARCHAR(20)  )";
                //SQLManager.AñadirCirculo(nuevoCirculo);
                SQLManager.EjecutarQuery(query1, query2);
                Console.Write("El círculo fue creado exitosamente.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Ese círculo ya existe.");
            }
        }
    }
}
