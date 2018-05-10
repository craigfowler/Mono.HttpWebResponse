using System.Net;
using System.Web.Http;

namespace Mono.HttpWebResponse.WebApp.Controllers
{
  public class MyApiController : ApiController
  {
    // Simple API GET action.  If the parameter is true then it will return a valid HTTP 200 response.
    // If the parameter is false then it will return an HTTP 403 Forbidden
    public int Get(bool success)
    {
      if(success)
        // Chosen by fair dice roll, guaranteed to be random.
        return 4;

      throw new HttpResponseException(HttpStatusCode.Forbidden);
    }

  }
}
