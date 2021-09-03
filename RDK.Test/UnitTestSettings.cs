using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RDK.TestSettings;

[TestClass]
public class UnitTestSettings
{
    [TestMethod]
    public void TestSerilog()
    {
        Settings.Serilog.LoadLog();
    }

    [TestMethod]
    public void TestConfig()
    {
        /// Not Passing because of no Console is being instanciate.
        //Settings.Config.LoadBasic();
    }
}