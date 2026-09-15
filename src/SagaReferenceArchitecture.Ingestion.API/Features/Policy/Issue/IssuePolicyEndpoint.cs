namespace SagaReferenceArchitecture.Ingestion.API.Features.Policy.Issue;

/// <summary>
/// Represents the endpoint for issuing policies in the SagaReferenceArchitecture application.  
/// </summary>
public static class IssuePolicyEndpoint
{
    /// <summary>
    /// Maps the endpoint for issuing policies to the specified route builder.  
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapIssuePolicy(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/issue-policy",
            async (
                IssuePolicyCommad command,
                IssuePolicyHandler handler,
                CancellationToken cancellationToken) =>
            {
                var policyId = await handler.Handle(
                    command,
                    cancellationToken);

                return Results.Accepted(
                    $"/issue-policy/{policyId}",
                    new
                    {
                        policyId
                    });
            });

        return endpoints;
    }
}
