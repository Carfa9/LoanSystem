namespace WebAPI.Stresstests;
using NBomber.CSharp;
using NBomber.Http;
using NBomber.Http.CSharp;

public class StressTests
{
    
[Fact]
    public void Test1()
    {
        using var HttpClient = new HttpClient();
        var scenario = Scenario.Create("GetAllUsers", async context => // Skapar ett scenario!
        {
            var request = Http.CreateRequest("GET", "http://localhost:5109/api/users");
            var response = await Http.Send(HttpClient, request);
            return response;
        }).WithoutWarmUp();

        NBomberRunner
            .RegisterScenarios(scenario)
            .WithWorkerPlugins(new HttpMetricsPlugin(new[] { HttpVersion.Version1}))
            .Run();
    }
    
}
