using CactiServer.Managers;
using CactiServer.Repos;
using CactiServer.Services;
using Database;
using Google.Api;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

IDatabase? dataDb = null;
try
{
    var db = DatabaseAccess.CreateInstance("DataDb");

    var dataDbSetting = builder.Configuration.GetConnectionString("DataDb");

    string provider = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data";
    string connectionString = dataDbSetting + "UID=waldo;PASSWORD=waldow;SslMode=none;Connection Timeout = 20";

    db.Initialize(new DatabaseCallback(), connectionString, provider);

    //ServerStatus.DatabaseServer = LogicSource.Ls.ServerSettings.DbServer;
    //ServerStatus.DatabaseProvider = LogicSource.Ls.ServerSettings.DbProvider;
    //ServerStatus.DatabaseSchema = schema;

    // Updates
    db.UpdateScriptParser.AddParseValue("%SCHEMA%", db.Schema);

    // Done!
    //logger.Info("-> Database initialized");
    dataDb = db;
}
catch (Exception e)
{
    // logger.Fatal(e, "Initialize database failed");
}

if (dataDb != null)
{
    builder.Services.AddSingleton(dataDb);

    builder.Services.AddSingleton<CactiRepo>();
    builder.Services.AddSingleton<PhotoRepo>();
    
}

builder.Services.AddSingleton<ICallbackManager, CallbackManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CactiService>();
app.MapGrpcService<PhotoService>();
app.MapGrpcService<FileService>();
app.MapGrpcService<CallbackService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();


public class DatabaseCallback : IDatabaseAccess
{
    public void DbLogBackState(DbState dbState)
    {

    }

    public void DbLogInfo(string message)
    {

    }

    public void DbQueryFailed(DbException dbException)
    {

    }
}
