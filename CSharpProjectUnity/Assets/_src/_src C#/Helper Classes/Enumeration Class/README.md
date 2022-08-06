 ```cs
  using MirJan.Helpers;
    
    public class ExampleEnumeration : Enumeration
    {
        public static readonly ExampleEnumeration EXAMPLE_ONE = new ExampleEnumeration(nameof(EXAMPLE_ONE));
        public static readonly ExampleEnumeration EXAMPLE_TWO = new ExampleEnumeration(nameof(EXAMPLE_TWO));
        
        private ExampleEnumeration(string name) : base(name) { }
    }
  ```
