using Azure.AI.Projects;

namespace LineRunner.Application.Configuration;

public class AzureAiOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string DeploymentModel { get; set; } = string.Empty;
}
