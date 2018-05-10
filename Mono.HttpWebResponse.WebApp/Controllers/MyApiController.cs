using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mono.HttpWebResponse.WebApp.Controllers
{
  public class MyApiController : ApiController
  {
    // Simple API GET action.  If the parameter is true then it will return a valid HTTP 200 response.
    // If the parameter is false then it will return an HTTP 500 Internal Server Error
    public int Get(bool success)
    {
      if(success)
        // Chosen by fair dice roll, guaranteed to be random.
        return 4;


      throw new HttpResponseException(GetExceptionResponseMessage());
    }

    HttpResponseMessage GetExceptionResponseMessage()
    {
      var output = new HttpResponseMessage(HttpStatusCode.InternalServerError);

      var outputString = @"This is a particularly long UTF8 string.
The length may or may not have anything to do with the error of course.
I only include it because the code where I originally repro'd this had a particularly
long response.";

      var lotsOfSpam = String.Join(", ", Enumerable.Range(0, 500).Select(x => "spam"));
      outputString = String.Concat(outputString,  lotsOfSpam);

      // To be closer still to the repo case, I'm adding a trailing null character
      outputString = String.Concat(outputString, '\0');

      output.Content = new StringContent(outputString);

      return output;
    }

  }
}
