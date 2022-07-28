using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = SingletonAnalyzer.Test.CSharpCodeFixVerifier<
    SingletonAnalyzer.SingletonAnalyzerAnalyzer,
    SingletonAnalyzer.SingletonAnalyzerCodeFixProvider>;

namespace SingletonAnalyzer.Test
{
    [TestClass]
    public class SingletonAnalyzerUnitTest
    {
        //No diagnostics expected to show up
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"
public class Test : Singleton
{
    private Test()
    {
    }
}

public class Singleton
{
}
";

            await VerifyCS.VerifyAnalyzerAsync(test);
        }

    }
}
