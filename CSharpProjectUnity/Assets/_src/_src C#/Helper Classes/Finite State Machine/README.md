  # Finite State Machine & State
  
  # Example Code
  ```cs
    using MirJan.Helpers;

    public class Example
    {
        public FiniteStateMachine FSM = new FiniteStateMachine();

        void ExampleMethod()
        {
            FSM.Add(0, new ExampleState(FSM));
        }
    }

    public class ExampleState : FiniteStateMachineState
    {
        public ExampleState(FiniteStateMachine fsm) : base(fsm) { }

        public ovveride void Enter() { }
        public ovveride void Exit() { }
        public ovveride void Update() { }
        public ovveride void FixedUpdate() { }
        public ovveride void LateUpdate() { }
    }
   ```
