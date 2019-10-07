using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class StateMachine
    {
        private IState currentState;
        private IState firstState;
        private IState previusState;

        public void GoBackToFirst()
        {
            ChangeState(firstState);
        }

        public void GoBackToPrevious()
        {
            ChangeState(previusState);
        }

        public void ChangeState(IState newState)
        {
            if (currentState == null)
                firstState = newState;

            previusState = currentState;
            currentState = newState;
            currentState.Handle(this);
        }
    }
}
