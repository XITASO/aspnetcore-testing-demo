using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Tests.Helpers;
using WebApp;
using WebApp.Model;

namespace Tests.Setup
{
    public class TestContext : IDisposable
    {
        public string ProjectsEndpoint => "/api/Projects";
        public string ParticipantsEndPoint => "/api/participants";
        private TestServer server;
        private HttpClient client;
        private MyDbContext dbContext;
        private IServiceScope scope;

        public TestServer Server => server ?? (server = new TestServer(WebHostBuilder()));
        public HttpClient Client => client ?? (client = Server.CreateClient());

        public MyDbContext DBContext =>
            dbContext ?? (dbContext = InitializeDatabase());

        private IServiceScope Scope => scope ?? (scope = Server.Host.Services.CreateScope());

        public void Dispose()
        {
            client?.Dispose();
            client = null;
            scope?.Dispose();
            scope = null;
            server?.Dispose();
            server = null;
            dbContext?.Dispose();
            dbContext = null;
        }

        protected TService GetService<TService>()
        {
            return Scope.ServiceProvider.GetRequiredService<TService>();
        }

        public async Task<TResult> GetAsync<TResult>(Uri href)
        {
            return await GetAsync<TResult>(href.ToString());
        }

        public async Task<TResult> GetAsync<TResult>(string href)
        {
            var response = await Client.GetAsync(href).EnsureSuccess();
            var result = await response.DeserializeAsync<TResult>();
            return result;
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string href, T dto)
        {
            return await Client.PutAsJsonAsync(href, dto).EnsureSuccess();
        }
        public async Task<HttpResponseMessage> PostAsync<T>(string href, T dto)
        {
            return await Client.PostAsJsonAsync(href, dto).EnsureSuccess(HttpStatusCode.Created);
        }

        private IWebHostBuilder WebHostBuilder()
        {
            var host = new WebHostBuilder();
            host
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<MyDbContext>(opts => opts.UseSqlite(DBContext.Database.GetDbConnection()));
                });
            return host;
        }

        private static MyDbContext InitializeDatabase(bool initializeDatabaseWithTestData = false)
        {
            var dbConnection = new SqliteConnection("DataSource=:memory:");
            dbConnection.Open();
            var dbOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlite(dbConnection)
                .EnableSensitiveDataLogging()
                .Options;
            var dbContext = new MyDbContext(dbOptions);
            dbContext.Database.Migrate();
            if (initializeDatabaseWithTestData)
            {
                dbContext.InitializeDatabaseWithFakeData();
            }
            return dbContext;
        }
    }
}