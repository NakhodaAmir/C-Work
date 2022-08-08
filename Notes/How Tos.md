# How Tos
## Thread Safe Fully Lazy Singleton
```cs
public sealed class Singleton
{
  private Singleton() { }
  public Singleton Instance => Lazy.instance;
  
  private class Lazy
  {
    static Lazy() { }
    
    internal static readonly Singleton instance = new Singleton();
  }
}
```
