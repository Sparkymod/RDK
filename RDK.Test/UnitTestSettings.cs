using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDK.Core.Reflection;
using RDK.Database;
using RDK.Database.Manager;

namespace RDK.TestSettings;

[TestClass]
public class UnitTestSettings
{
    [TestMethod]
    public void TestSerilog()
    {
        Settings.Serilog.LoadLog();
    }
}