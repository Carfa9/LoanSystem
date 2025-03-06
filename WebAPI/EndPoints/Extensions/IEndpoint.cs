namespace WebAPI.EndPoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
