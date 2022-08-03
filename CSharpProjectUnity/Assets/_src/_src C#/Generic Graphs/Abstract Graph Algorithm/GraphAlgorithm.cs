namespace MirJan
{
    namespace GenericGraphs
    {
        using MirJan.Helpers;

        abstract public class GraphAlgorithm<Node> where Node : class
        {
            #region Enumeration Class
            public class AlgorithmStatus : Enumeration
            {
                public static readonly AlgorithmStatus NOT_INITIALIZED = new AlgorithmStatus(nameof(NOT_INITIALIZED));
                public static readonly AlgorithmStatus SUCCEEDED = new AlgorithmStatus(nameof(SUCCEEDED));
                public static readonly AlgorithmStatus RUNNING = new AlgorithmStatus(nameof(RUNNING));

                protected AlgorithmStatus(string name) : base(name) { }
            }
            #endregion

            #region Delegates
            public delegate void DelegateOnStatus();
            public DelegateOnStatus OnStart { get; set; }
            public DelegateOnStatus OnSuccess { get; set; }
            public DelegateOnStatus OnRunning { get; set; }
            #endregion

            #region Properties
            public Node SourceNode { get; protected set; }
            public AlgorithmStatus Status { get; protected set; }

            public bool IsNotInitialized { get { return Status.Equals(AlgorithmStatus.NOT_INITIALIZED); } }
            public bool IsRunning { get { return Status.Equals(AlgorithmStatus.RUNNING); } }
            public bool IsSucceeded { get { return Status.Equals(AlgorithmStatus.SUCCEEDED); } }
            #endregion

            #region Constructor
            public GraphAlgorithm()
            {
                Status = AlgorithmStatus.NOT_INITIALIZED;

                SourceNode = default;
            }
            #endregion

            #region Public Methods
            public AlgorithmStatus Step()
            {
                if (!IsRunning) return Status;

                return StepMethod();
            }

            public void ForceReset()
            {
                Reset();
            }
            #endregion

            #region Protected & Private Methods
            protected abstract void InitializeMethod();
            protected bool InitializeStart()
            {
                if (IsRunning) return false;

                Reset();

                return true;
            }

            protected bool InitializeEnd()
            {
                OnStart?.Invoke();

                Status = AlgorithmStatus.RUNNING;

                return true;
            }

            protected virtual AlgorithmStatus StepMethod()
            {
                Status = AlgorithmStatus.RUNNING;

                OnRunning?.Invoke();

                return Status;
            }

            protected virtual void Reset()
            {
                Status = AlgorithmStatus.NOT_INITIALIZED;

                SourceNode = null;
            }
            #endregion
        }
    }
}

