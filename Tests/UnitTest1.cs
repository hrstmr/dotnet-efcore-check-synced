using dotnet_efcore_check_syned;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.DependencyInjection;
//using Snapshooter.Xunit;
using System.Diagnostics;
using Tests.MigrationsToDelete;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

        //var db = new TestDbContext();

        // Create design-time services
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddEntityFrameworkDesignTimeServices();
        //serviceCollection.AddDbContextDesignTimeServices(db);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var dbContextModelSnapshot = new TestDbContextModelSnapshot();
        var model = dbContextModelSnapshot.Model;
        
        var migrationsCodeGenerator = serviceProvider.GetService<IMigrationsCodeGenerator>();

        var snapshot = migrationsCodeGenerator?.GenerateSnapshot(
            "dbContextSnapshot",
            typeof(TestDbContext),
            "dbContextSnapshot",
            model);

        Console.WriteLine(snapshot);
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
        //dotnet ef dbcontext script

        ProcessStartInfo generateMigrationSnapshot = new()
        {
            FileName = "dotnet",
            Arguments = "ef migrations add TEMP --json --no-build  -c TestDbContext --project \"..\\..\\..\\Tests.csproj\" -o \"MigrationsToDelete\"",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        var generateMigrationSnapshotProcess = Process.Start(generateMigrationSnapshot);
        ArgumentNullException.ThrowIfNull(generateMigrationSnapshotProcess);
        string generateMigrationSnapshotProcessOutput = generateMigrationSnapshotProcess.StandardOutput.ReadToEnd();
        await generateMigrationSnapshotProcess.WaitForExitAsync();

        Console.WriteLine(generateMigrationSnapshotProcessOutput);

        ProcessStartInfo startInfo = new()
        {
            FileName = "dotnet",
            Arguments = "ef dbcontext script -c TestDbContext --no-build --project ..\\..\\..\\Tests.csproj -o \"..\\..\\..\\Tests\\DbSnapshot.sql\"",
            //Arguments = "ef migrations add TEMP --json --no-build  -c TestDbContext --project ..\\..\\..\\Tests.csproj",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
        string output = proc.StandardOutput.ReadToEnd();
        await proc.WaitForExitAsync();

        Console.WriteLine(output);



        ProcessStartInfo removeMigrationSnapshot = new()
        {
            FileName = "dotnet",
            Arguments = "ef migrations remove  --no-build  -c TestDbContext --project ..\\..\\..\\Tests.csproj",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        var removeMigrationSnapshotProcess = Process.Start(removeMigrationSnapshot);
        ArgumentNullException.ThrowIfNull(removeMigrationSnapshotProcess);
        string removeMigrationSnapshotProcessOutput = removeMigrationSnapshotProcess.StandardOutput.ReadToEnd();
        await removeMigrationSnapshotProcess.WaitForExitAsync();

        Console.WriteLine(removeMigrationSnapshotProcessOutput);

        string path = "..\\..\\..\\Tests\\DbSnapshot.sql";
        string content = File.ReadAllText(path);

        //Snapshot.Match(content);
        Assert.NotNull(content);
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


    public static bool CompareDbContextModelSnapshots(string snapshotFile1, string snapshotFile2)
    {
        return false;
        var db = new TestDbContext();

        // Create design-time services
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddEntityFrameworkDesignTimeServices();
        serviceCollection.AddDbContextDesignTimeServices(db);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var dbContextModelSnapshot = new TestDbContextModelSnapshot();
        var model = dbContextModelSnapshot.Model;

        // Add a migration
        var modelCodeGenerator = serviceProvider.GetService<IModelCodeGenerator>();


        var migrationsCodeGenerator = serviceProvider.GetService<IMigrationsCodeGenerator>();

        var snapshot = migrationsCodeGenerator?.GenerateSnapshot(
            "dbContextSnapshot",
            typeof(TestDbContext),
            "dbContextSnapshot",
            model);

        Console.WriteLine(snapshot);



        //var factory = new DesignTimeServicesBuilder().Build(typeof(TestDbContext));
        //var snapshot1 = LoadModelSnapshot(factory, snapshotFile1);
        //var snapshot2 = LoadModelSnapshot(factory, snapshotFile2);

        //if (snapshot1 == null || snapshot2 == null)
        //{
        //    throw new Exception("Failed to load model snapshots.");
        //}

        //var comparer = new SnapshotModelComparer();
        //var differences = comparer.Compare(snapshot1.Model, snapshot2.Model);

        //return differences.Count == 0;
    }

    //private static IDbContextModel LoadModelSnapshot(IDesignTimeServicesFactory factory, string snapshotFile)
    //{
    //    var fileInfo = new FileInfo(snapshotFile);
    //    var contextType = factory.GetContextTypes(fileInfo.Directory.FullName).FirstOrDefault();

    //    if (contextType == null)
    //    {
    //        return null;
    //    }

    //    var migrationAssembly = factory.CreateMigrationAssembly(contextType.Assembly.GetName().Name);
    //    var modelSnapshot = migrationAssembly.ModelSnapshot;

    //    if (modelSnapshot == null)
    //    {
    //        return null;
    //    }

    //    var loader = factory.CreateMigrationIdGenerator();
    //    var snapshotId = loader.GenerateId(fileInfo.Name);
    //    return modelSnapshot.Load(snapshotId);
    //}


}

public class TestDbContext : BloggingContext
{

}

//public class DbSnapshotExposed : TestDbContextModelSnapshot {


//}
