using Azure.AI.Projects;

namespace LineRunner.Application.Configuration;

public class AzureAiOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
    public string ProjectName { get; set; }
}
