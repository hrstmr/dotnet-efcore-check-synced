using dotnet_efcore_check_syned;
using System.Diagnostics;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public void TestDotNetVersion()
    {
        Process process = new Process();
        process.StartInfo.FileName = "dotnet";
        process.StartInfo.Arguments = "--version";
        //process.StartInfo.WorkingDirectory = "/path/to/working/directory";
        process.Start();
        process.WaitForExit();
        string output = process.StandardOutput.ReadToEnd();
        Assert.Contains("5.", output);
    }

}

public class TestDbContext : BloggingContext
{

}