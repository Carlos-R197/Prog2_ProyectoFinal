using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class MainMenuState : IState
    {
        public void Enter()
        {
            while (true)
            {
                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Ver propio perfil");
                Console.WriteLine("2. Buscar un perfil existente");
                Console.WriteLine("3. Crear un chat privado");
                Console.WriteLine("4. Ver Lista de circulos existente");
                Console.WriteLine("5. Salir de la aplicación");
                Console.Write("R: ");
                byte input;

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:

                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        default:
                            continue;
                    }
                }
            }
        }
    }
}
