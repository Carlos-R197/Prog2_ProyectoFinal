using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class StateMachine
    {
        private IState currentState;
        private IState firstState;

        public void GoBackToMainMenu()
        {
            ChangeState(firstState);
        }

        public void ChangeState(IState newState)
        {
            if (currentState == null)
                firstState = newState;

            currentState = newState;
            currentState.Handle(this);
        }
    }
}
