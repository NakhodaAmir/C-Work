namespace MirJan
{
    namespace Helpers
    {
        public class FiniteStateMachine
        {
            #region Variables
            protected Dictionary<int, FiniteStateMachineState> states;
            protected FiniteStateMachineState currentState;
            protected FiniteStateMachineState previousState;
            #endregion

            #region Constructor
            public FiniteStateMachine()
            {
                states = new Dictionary<int, FiniteStateMachineState>();
                currentState = null;
                previousState = null;
            }
            #endregion

            #region Public Methods
            public void Add(int key, FiniteStateMachineState state)
            {
                states.Add(key, state);
            }

            public FiniteStateMachineState GetState(int key)
            {
                return states[key];
            }

            public FiniteStateMachineState GetPreviousState()
            {
                return previousState;
            }

            public void SetCurrentState(int key)
            {
                SetCurrentState(GetState(key));
            }

            public void SetCurrentState(FiniteStateMachineState state)
            {
                if (currentState != null) currentState.Exit();

                previousState = currentState;

                currentState = state;

                if (currentState != null) currentState.Enter();
            }

            public void Update()
            {
                if(currentState != null) currentState.Update();
            }

            public void FixedUpdate()
            {
                if (currentState != null) currentState.FixedUpdate();
            }

            public void LateUpdate()
            {
                if (currentState != null) currentState.LateUpdate();
            }
            #endregion
        }
    }
}

