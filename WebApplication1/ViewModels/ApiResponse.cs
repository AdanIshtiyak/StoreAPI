namespace WebApplication1.ViewModels
{
  public class ApiResponse
  {
    public int StatusCode { get; set; }

    public IResponseContext Content {get;set;}
  }

  public interface IResponseContext { }
}
