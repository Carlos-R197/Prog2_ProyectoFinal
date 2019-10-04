using System;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            StateMachine appState = new StateMachine();

            appState.ChangeState(new InicioState(appState));
        }
    }
}
