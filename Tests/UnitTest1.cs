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
    public async void TestDotNetVersion()
    {
        //dotnet ef migrations add TEMP --project .\Tests\Tests.csproj
        //dotnet ef dbcontext script -c TestDbContext -p .\Tests\Tests.csproj -o ".\Tests\DbSnapshot.sql"
        //C:\Work\Projects\dotnet-efcore-check-syned\Tests\bin\Debug\net7.0\Tests\Tests.csproj
        //C:\Work\Projects\dotnet-efcore-check-syned\Tests\bin\Debug\this\Tests\Tests.csproj
        //C:\Work\Projects\dotnet-efcore-check-syned\Tests\bin\Debug\this\Tests\Tests.csproj
        //dotnet ef migrations add TEMP --project ..\..\..\Tests.csproj
        //dotnet ef migrations add TEMP --no-build --project ..\..\..\Tests.csproj
        ProcessStartInfo startInfo = new()
        {
            FileName = "dotnet",
            Arguments = "ef migrations add TEMP --json --no-build --project ..\\..\\..\\Tests.csproj",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
        string output = proc.StandardOutput.ReadToEnd();
        await proc.WaitForExitAsync();
        Assert.NotNull(output);
        //TestDbContextModelSnapshot.

        //var builder = new DbContextOptionsBuilder<BloggingContext>();

        //await using var context = new BloggingContext();
        //{

        //    await context.Database.MigrateAsync();
        //}

        //Process process = new Process();
        //process.StartInfo.FileName = "dotnet";
        //process.StartInfo.Arguments = "--version";
        ////process.StartInfo.WorkingDirectory = "/path/to/working/directory";
        //process.Start();
        //process.WaitForExit();
        //string output = process.StandardOutput.ReadToEnd();
        //Assert.Contains("5.", output);
    }

}

public class TestDbContext : BloggingContext
{

}