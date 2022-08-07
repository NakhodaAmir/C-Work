- [**Base Persistent Singleton**](BasePersistentSingleton.cs)
<br>The base class of the [**Persistent Singleton**](PersistentSingleton.cs) class. Inherits ScriptableObject.
- [**Persistent Singleton**](PersistentSingleton.cs)
<br>Inherit this class to create a persistent singleton scriptable objects. Persistent singletons persists between scenes and will not be destroyed when loading to a new scene. These singletons are thread safe and fully lazy.
- [**Persistent Singleton Behaviour**](PersistentSingletonBehaviour.cs)
<br>Since persistent singletons are scriptable objects, they do not contain any unity methods such as Awake(), Start(), Update(), etc... Therefore, a persistent singleton will create a GameObject, the persistent singleton behaviour, to help call these missing functions. Only one persistent singleton behaviour will be created. This object will persist between scenes.
- [**Singleton**](Singleton.cs)
<br>Inherit this class to create a singleton. This singleton class inherits from MonoBehaviour. A singleton will ensure that there is only one instance of it. If there are two instances of the same object, one of the instances will be destroyed. This is not a thread safe singleton. This singleton will not persist between scenes.

# Example Code
```cs
using MirJan.Unity.Helpers;

//Persistent Singleton Example
//Ensure to create an asset
public class ExampleSingleton : PersistentSingleton<ExampleSingleton>
{
  public void DoSomething() { }
}

//Singleton Example
public class ExampleSingleton2 : Singleton<ExampleSingleton2>
{
  public void DoSomething() { }
}

public class TestExample
{
  public void Test()
  {
    ExampleSingleton.Instance.DoSomething();
    
    ExampleSingleton2.Instance.DoSomething();
  }
}
```
