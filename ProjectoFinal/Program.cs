﻿using System;

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
