using Azure.AI.Projects;

namespace LineRunner.Application.Configuration;

public class AzureAiOptions
{
    public string Endpoint { get; set; } 
    public string Key { get; set; }
    public string DeploymentModel { get; set; } 
}
