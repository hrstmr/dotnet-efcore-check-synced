using dotnet_efcore_check_syned;
using Snapshooter.Xunit;
//using Snapshooter.Xunit;
using System.Diagnostics;

namespace Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //var db = new TestDbContext();

        // Create design-time services
        //var serviceCollection = new ServiceCollection();
        //serviceCollection.AddEntityFrameworkDesignTimeServices();
        ////serviceCollection.AddDbContextDesignTimeServices(db);
        //var serviceProvider = serviceCollection.BuildServiceProvider();

        //var dbContextModelSnapshot = new TestDbContextModelSnapshot();
        //var blogginModelSnapshot = new BloggingContextModelSnapshot();
        //var model = dbContextModelSnapshot.Model;

        //var migrationsCodeGenerator = serviceProvider.GetService<IMigrationsCodeGenerator>();

        //var snapshot = migrationsCodeGenerator?.GenerateSnapshot(
        //    "dbContextSnapshot",
        //    typeof(TestDbContext),
        //    "dbContextSnapshot",
        //    model);

        //Console.WriteLine(snapshot);
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

        ProcessStartInfo generateMigrationSnapshot =
            new()
            {
                FileName = "dotnet",
                Arguments =
                    "ef migrations add TEMP --json --no-build  -c TestDbContext --project \"..\\..\\..\\Tests.csproj\" -o \"MigrationsToDelete\" ",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        var generateMigrationSnapshotProcess = Process.Start(generateMigrationSnapshot);
        ArgumentNullException.ThrowIfNull(generateMigrationSnapshotProcess);
        string generateMigrationSnapshotProcessOutput =
            generateMigrationSnapshotProcess.StandardOutput.ReadToEnd();
        await generateMigrationSnapshotProcess.WaitForExitAsync();

        Console.WriteLine(generateMigrationSnapshotProcessOutput);

        ProcessStartInfo startInfo =
            new()
            {
                FileName = "dotnet",
                Arguments =
                    "ef dbcontext script -c TestDbContext --no-build --project ..\\..\\..\\Tests.csproj -o \"..\\..\\..\\Tests\\DbSnapshot.sql\"",
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

        //ProcessStartInfo removeMigrationSnapshot = new()
        //{
        //    FileName = "dotnet",
        //    Arguments = "ef migrations remove  --no-build  -c TestDbContext --project ..\\..\\..\\Tests.csproj",
        //    CreateNoWindow = true,
        //    RedirectStandardOutput = true,
        //    RedirectStandardError = true,
        //};
        //var removeMigrationSnapshotProcess = Process.Start(removeMigrationSnapshot);
        //ArgumentNullException.ThrowIfNull(removeMigrationSnapshotProcess);
        //string removeMigrationSnapshotProcessOutput = removeMigrationSnapshotProcess.StandardOutput.ReadToEnd();
        //await removeMigrationSnapshotProcess.WaitForExitAsync();

        //Console.WriteLine(removeMigrationSnapshotProcessOutput);

        string path = "..\\..\\..\\Tests\\DbSnapshot.sql";
        string content = File.ReadAllText(path);

        Snapshot.Match(content);
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
        //var db = new TestDbContext();

        // Create design-time services
        //var serviceCollection = new ServiceCollection();
        //serviceCollection.AddEntityFrameworkDesignTimeServices();
        //serviceCollection.AddDbContextDesignTimeServices(db);
        //var serviceProvider = serviceCollection.BuildServiceProvider();

        ////var dbContextModelSnapshot = new TestDbContextModelSnapshot();
        //var model = dbContextModelSnapshot.Model;

        //// Add a migration
        //var modelCodeGenerator = serviceProvider.GetService<IModelCodeGenerator>();


        //var migrationsCodeGenerator = serviceProvider.GetService<IMigrationsCodeGenerator>();

        //var snapshot = migrationsCodeGenerator?.GenerateSnapshot(
        //    "dbContextSnapshot",
        //    typeof(TestDbContext),
        //    "dbContextSnapshot",
        //    model);

        //Console.WriteLine(snapshot);



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

    [Fact]
    public async void CheckIfEfMigrationAddIsRequired()
    {
        var projLoction = @"..\..\..\..\dotnet-efcore-check-syned\dotnet-efcore-check-syned.csproj";
        var outputLoction = @"..\..\..\Tests\DbSnapshot.sql";

        ProcessStartInfo startInfo =
            new()
            {
                FileName = "dotnet",
                Arguments =
                    $"ef dbcontext script -c BloggingContext --no-build -p {projLoction} -o {outputLoction}",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
        string output = proc.StandardOutput.ReadToEnd();
        await proc.WaitForExitAsync();

        string content = File.ReadAllText(outputLoction);

        Snapshot.Match(content);
        Assert.NotNull(content);
    }

    [Fact]
    public async void CheckIfEfMigrationAddIsEmpty()
    {
        var migrationName = "SHOULD_BE_REMOVED_BEFORE_PR";
        ProcessStartInfo startInfo =
            new()
            {
                FileName = "dotnet",
                Arguments =
                    $"ef migrations add {migrationName} --json --no-build  -c BloggingContext --project ..\\..\\..\\..\\dotnet-efcore-check-syned\\dotnet-efcore-check-syned.csproj -o ..\\Tests\\CheckMigrationIsEmpty ",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
        string output = proc.StandardOutput.ReadToEnd();
        await proc.WaitForExitAsync();
        Console.WriteLine(output);

        string directoryPath = "..\\..\\..\\CheckMigrationIsEmpty";

        string[] files = Directory.GetFiles(directoryPath, $"*_{migrationName}.cs");
        var emptyMigration =
            @$"using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_efcore_check_syned.Migrations
{{
    /// <inheritdoc />
    public partial class {migrationName} : Migration
    {{
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {{

        }}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {{

        }}
    }}
}}
";
        foreach (string file in files)
        {
            string content = File.ReadAllText(file);
            Assert.Equal(emptyMigration, content);
            Console.WriteLine(content);
        }

        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
            Console.WriteLine("Folder deleted successfully.");
        }
        else
        {
            Console.WriteLine("Folder does not exist.");
        }
    }

    [Fact]
    public async void CheckIfEfMigrationAddIsEmptyWithoutUpdatingOriginalSnapshot()
    {
        var migrationName = "SHOULD_BE_REMOVED_BEFORE_PR";
        ProcessStartInfo startInfo =
            new()
            {
                FileName = "dotnet",
                Arguments =
                    $"ef migrations add {migrationName} --json --no-build -c TestDbContext --project ..\\..\\..\\Tests.csproj -o ..\\Tests\\CheckMigrationIsEmpty -n CustomNS",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
        string output = proc.StandardOutput.ReadToEnd();
        await proc.WaitForExitAsync();
        Console.WriteLine(output);

        string directoryPath = "..\\..\\..\\CheckMigrationIsEmpty";

        string[] files = Directory.GetFiles(directoryPath, $"*_{migrationName}.cs");
        var emptyMigration =
            @$"using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_efcore_check_syned.Migrations
{{
    /// <inheritdoc />
    public partial class {migrationName} : Migration
    {{
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {{

        }}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {{

        }}
    }}
}}
";
        foreach (string file in files)
        {
            string content = File.ReadAllText(file);
            Assert.Equal(emptyMigration, content);
            Console.WriteLine(content);
        }

        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, true);
            Console.WriteLine("Folder deleted successfully.");
        }
        else
        {
            Console.WriteLine("Folder does not exist.");
        }
    }

    [Fact]
    public async void CheckSnapshot()
    {
        var migrationName = "SHOULD_BE_REMOVED_BEFORE_PR";
        ProcessStartInfo startInfo =
            new()
            {
                FileName = "dotnet",
                Arguments =
                    $"ef migrations add {migrationName} --json --no-build -c TestDbContext --project ..\\..\\..\\Tests.csproj",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
        string output = proc.StandardOutput.ReadToEnd();
        await proc.WaitForExitAsync();
        Console.WriteLine(output);

        string directoryPath = "..\\..\\..\\Migrations";

        var files = Directory.GetFiles(directoryPath, $"*_{migrationName}*.cs");

        foreach (string file in files)
        {
            File.Delete(file);
        }

        var linesToIgnore = new List<string>
        {
            //// Lines from Main db snapshot
            "using System;",
            "using dotnet_efcore_check_syned;",
            "namespace dotnet_efcore_check_syned.Migrations",
            "[DbContext(typeof(BloggingContext))]",
            "partial class BloggingContextModelSnapshot : ModelSnapshot",
            //// Lines from Test db snapshot
            "using System;",
            "using Tests;",
            "namespace CustomNS",
            "[DbContext(typeof(TestDbContext))]",
            "partial class TestDbContextModelSnapshot : ModelSnapshot",
        };

        var productVersionLine = "modelBuilder.HasAnnotation(\"ProductVersion\",";

        var testDbSnapshotPath = "..\\..\\..\\Migrations\\TestDbContextModelSnapshot.cs";
        string testDbSnapshot = string.Join(
            "\r\n",
            File.ReadAllText(testDbSnapshotPath)
                .Split("\r\n")
                .Where(
                    line =>
                        !(linesToIgnore.Contains(line.Trim()) || line.Contains(productVersionLine))
                )
        );

        var mainDbSnapshotPath =
            "..\\..\\..\\..\\dotnet-efcore-check-syned\\Migrations\\BloggingContextModelSnapshot.cs";

        //string mainDbSnapshot = File.ReadAllText(mainDbSnapshotPath);
        string mainDbSnapshot = string.Join(
            "\r\n",
            File.ReadAllText(mainDbSnapshotPath)
                .Split("\r\n")
                .Where(
                    line =>
                        !(linesToIgnore.Contains(line.Trim()) || line.Contains(productVersionLine))
                )
        );

        Assert.Equal(mainDbSnapshot, testDbSnapshot);
    }
}

public class TestDbContext : BloggingContext { }

//public class DbSnapshotExposed : TestDbContextModelSnapshot {


//}
