using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public interface IState
    {
        void Handle(StateMachine appState);
    }
}
