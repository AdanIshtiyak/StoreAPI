namespace StoreAPI.ViewModels
{
  public class ApiResponse
  {
    public int StatusCode { get; set; }

    public IResponseContext Content {get;set;}
  }

  public interface IResponseContext { }
}
