namespace MirJan
{
    namespace Helpers
    {
        abstract public class FiniteStateMachineState
        {
            #region Variables
            protected FiniteStateMachine fSM;
            #endregion

            #region Constructor
            public FiniteStateMachineState(FiniteStateMachine fsm) => fSM = fsm;
            #endregion

            #region Public Virtual Methods
            public virtual void Enter() { }
            public virtual void Exit() { }
            public virtual void Update() { }
            public virtual void FixedUpdate() { }
            public virtual void LateUpdate() { }
            #endregion
        }
    }
}
