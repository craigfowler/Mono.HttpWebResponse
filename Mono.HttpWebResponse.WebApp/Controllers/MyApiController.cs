using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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

      // Perhaps it's latency that causes problems?
      Thread.Sleep(5000);

      throw new HttpResponseException(GetExceptionResponseMessage());
    }

    HttpResponseMessage GetExceptionResponseMessage()
    {
      var output = new HttpResponseMessage(HttpStatusCode.InternalServerError);

      var outputString = GetLongResponseText();

      // To be closer still to the original repro case, I'm adding some trailing null
      // character (the original repro case has some, apparently)
      outputString = AddSomeTrailingNullCharacters(outputString, 3);

      output.Content = new StringContent(outputString, Encoding.UTF8, "application/json");

      return output;
    }

    string GetLongResponseText()
    {
      // Here's a huge lump of JSON which matches the response text I have seen from originally repro'ing the issue
      return @"{
  ""hCode"": 12681836,
  ""stackTrace"": [
    {
      ""methodName"": ""newInstance0"",
      ""fileName"": null,
      ""className"": ""sun.reflect.NativeConstructorAccessorImpl"",
      ""nativeMethod"": true,
      ""lineNumber"": -2,
      ""hCode"": 513928194,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""newInstance"",
      ""fileName"": null,
      ""className"": ""sun.reflect.NativeConstructorAccessorImpl"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -432498851,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""newInstance"",
      ""fileName"": null,
      ""className"": ""sun.reflect.DelegatingConstructorAccessorImpl"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -2147429624,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""newInstance"",
      ""fileName"": null,
      ""className"": ""java.lang.reflect.Constructor"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -1851634561,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""createException"",
      ""fileName"": ""W3CHttpResponseCodec.java"",
      ""className"": ""org.openqa.selenium.remote.http.W3CHttpResponseCodec"",
      ""nativeMethod"": false,
      ""lineNumber"": 150,
      ""hCode"": 1568352053,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""decode"",
      ""fileName"": ""W3CHttpResponseCodec.java"",
      ""className"": ""org.openqa.selenium.remote.http.W3CHttpResponseCodec"",
      ""nativeMethod"": false,
      ""lineNumber"": 115,
      ""hCode"": -825939059,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""decode"",
      ""fileName"": ""W3CHttpResponseCodec.java"",
      ""className"": ""org.openqa.selenium.remote.http.W3CHttpResponseCodec"",
      ""nativeMethod"": false,
      ""lineNumber"": 45,
      ""hCode"": -825939129,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""execute"",
      ""fileName"": ""HttpCommandExecutor.java"",
      ""className"": ""org.openqa.selenium.remote.HttpCommandExecutor"",
      ""nativeMethod"": false,
      ""lineNumber"": 164,
      ""hCode"": -1852790165,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""execute"",
      ""fileName"": ""DriverCommandExecutor.java"",
      ""className"": ""org.openqa.selenium.remote.service.DriverCommandExecutor"",
      ""nativeMethod"": false,
      ""lineNumber"": 82,
      ""hCode"": -633333582,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""execute"",
      ""fileName"": ""RemoteWebDriver.java"",
      ""className"": ""org.openqa.selenium.remote.RemoteWebDriver"",
      ""nativeMethod"": false,
      ""lineNumber"": 637,
      ""hCode"": 1767885124,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""execute"",
      ""fileName"": ""RemoteWebElement.java"",
      ""className"": ""org.openqa.selenium.remote.RemoteWebElement"",
      ""nativeMethod"": false,
      ""lineNumber"": 272,
      ""hCode"": -1473293613,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""isEnabled"",
      ""fileName"": ""RemoteWebElement.java"",
      ""className"": ""org.openqa.selenium.remote.RemoteWebElement"",
      ""nativeMethod"": false,
      ""lineNumber"": 146,
      ""hCode"": 164455703,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke0"",
      ""fileName"": null,
      ""className"": ""sun.reflect.NativeMethodAccessorImpl"",
      ""nativeMethod"": true,
      ""lineNumber"": -2,
      ""hCode"": 703090916,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": null,
      ""className"": ""sun.reflect.NativeMethodAccessorImpl"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -1217602907,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": null,
      ""className"": ""sun.reflect.DelegatingMethodAccessorImpl"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": 506642458,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": null,
      ""className"": ""java.lang.reflect.Method"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": 708160817,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": ""EventFiringWebDriver.java"",
      ""className"": ""org.openqa.selenium.support.events.EventFiringWebDriver$EventFiringWebElement$1"",
      ""nativeMethod"": false,
      ""lineNumber"": 332,
      ""hCode"": 1557030886,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""isEnabled"",
      ""fileName"": null,
      ""className"": ""com.sun.proxy.$Proxy5"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -697797254,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""isEnabled"",
      ""fileName"": ""EventFiringWebDriver.java"",
      ""className"": ""org.openqa.selenium.support.events.EventFiringWebDriver$EventFiringWebElement"",
      ""nativeMethod"": false,
      ""lineNumber"": 378,
      ""hCode"": -1344798144,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke0"",
      ""fileName"": null,
      ""className"": ""sun.reflect.NativeMethodAccessorImpl"",
      ""nativeMethod"": true,
      ""lineNumber"": -2,
      ""hCode"": 703090916,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": null,
      ""className"": ""sun.reflect.NativeMethodAccessorImpl"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -1217602907,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": null,
      ""className"": ""sun.reflect.DelegatingMethodAccessorImpl"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": 506642458,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": null,
      ""className"": ""java.lang.reflect.Method"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": 708160817,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""invoke"",
      ""fileName"": ""KnownElements.java"",
      ""className"": ""org.openqa.selenium.remote.server.KnownElements$1"",
      ""nativeMethod"": false,
      ""lineNumber"": 64,
      ""hCode"": -2017324380,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""isEnabled"",
      ""fileName"": null,
      ""className"": ""com.sun.proxy.$Proxy6"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -697767463,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""call"",
      ""fileName"": ""GetElementEnabled.java"",
      ""className"": ""org.openqa.selenium.remote.server.handler.GetElementEnabled"",
      ""nativeMethod"": false,
      ""lineNumber"": 31,
      ""hCode"": -61461350,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""call"",
      ""fileName"": ""GetElementEnabled.java"",
      ""className"": ""org.openqa.selenium.remote.server.handler.GetElementEnabled"",
      ""nativeMethod"": false,
      ""lineNumber"": 23,
      ""hCode"": -61461358,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""run"",
      ""fileName"": null,
      ""className"": ""java.util.concurrent.FutureTask"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": 424519275,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""run"",
      ""fileName"": ""DefaultSession.java"",
      ""className"": ""org.openqa.selenium.remote.server.DefaultSession$1"",
      ""nativeMethod"": false,
      ""lineNumber"": 176,
      ""hCode"": -255143467,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""runWorker"",
      ""fileName"": null,
      ""className"": ""java.util.concurrent.ThreadPoolExecutor"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -1208971944,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""run"",
      ""fileName"": null,
      ""className"": ""java.util.concurrent.ThreadPoolExecutor$Worker"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": -166011880,
      ""class"": ""java.lang.StackTraceElement""
    },
    {
      ""methodName"": ""run"",
      ""fileName"": null,
      ""className"": ""java.lang.Thread"",
      ""nativeMethod"": false,
      ""lineNumber"": -1,
      ""hCode"": 1432591020,
      ""class"": ""java.lang.StackTraceElement""
    }
  ],
  ""suppressed"": [],
  ""additionalInformation"": ""\nDriver info: org.openqa.selenium.firefox.FirefoxDriver\nCapabilities [{moz:profile=C:\\Users\\ADMINI~1\\AppData\\Local\\Temp\\rust_mozprofile.9xL1opPi5y70, rotatable=false, timeouts={implicit=0.0, pageLoad=300000.0, script=30000.0}, pageLoadStrategy=normal, platform=ANY, proxy=Proxy(pac: http://127.0.0.1:19876/pac.js), specificationLevel=0.0, moz:accessibilityChecks=false, acceptInsecureCerts=false, browserVersion=53.0, platformVersion=10.0, moz:processID=2756.0, browserName=firefox, javascriptEnabled=true, platformName=windows_nt}]\nSession ID: fd731392-2653-491e-b6ac-d79c61ac066d"",
  ""localizedMessage"": ""The element reference of <html> stale: either the element is no longer attached to the DOM or the page has been refreshed\nFor documentation on this error, please visit: http://seleniumhq.org/exceptions/stale_element_reference.html\nBuild info: version: '3.4.0', revision: 'unknown', time: 'unknown'\nSystem info: host: 'WIN-SB3ER6JQ6ME', ip: '10.100.18.238', os.name: 'Windows 10', os.arch: 'x86', os.version: '10.0', java.version: '1.8.0_91'\nDriver info: org.openqa.selenium.firefox.FirefoxDriver\nCapabilities [{moz:profile=C:\\Users\\ADMINI~1\\AppData\\Local\\Temp\\rust_mozprofile.9xL1opPi5y70, rotatable=false, timeouts={implicit=0.0, pageLoad=300000.0, script=30000.0}, pageLoadStrategy=normal, platform=ANY, proxy=Proxy(pac: http://127.0.0.1:19876/pac.js), specificationLevel=0.0, moz:accessibilityChecks=false, acceptInsecureCerts=false, browserVersion=53.0, platformVersion=10.0, moz:processID=2756.0, browserName=firefox, javascriptEnabled=true, platformName=windows_nt}]\nSession ID: fd731392-2653-491e-b6ac-d79c61ac066d"",
  ""class"": ""org.openqa.selenium.StaleElementReferenceException"",
  ""buildInformation"": {
    ""buildRevision"": ""unknown"",
    ""releaseLabel"": ""3.4.0"",
    ""hCode"": 13169642,
    ""buildTime"": ""unknown"",
    ""class"": ""org.openqa.selenium.internal.BuildInfo""
  },
  ""message"": ""The element reference of <html> stale: either the element is no longer attached to the DOM or the page has been refreshed\nFor documentation on this error, please visit: http://seleniumhq.org/exceptions/stale_element_reference.html\nBuild info: version: '3.4.0', revision: 'unknown', time: 'unknown'\nSystem info: host: 'WIN-SB3ER6JQ6ME', ip: '10.100.18.238', os.name: 'Windows 10', os.arch: 'x86', os.version: '10.0', java.version: '1.8.0_91'\nDriver info: org.openqa.selenium.firefox.FirefoxDriver\nCapabilities [{moz:profile=C:\\Users\\ADMINI~1\\AppData\\Local\\Temp\\rust_mozprofile.9xL1opPi5y70, rotatable=false, timeouts={implicit=0.0, pageLoad=300000.0, script=30000.0}, pageLoadStrategy=normal, platform=ANY, proxy=Proxy(pac: http://127.0.0.1:19876/pac.js), specificationLevel=0.0, moz:accessibilityChecks=false, acceptInsecureCerts=false, browserVersion=53.0, platformVersion=10.0, moz:processID=2756.0, browserName=firefox, javascriptEnabled=true, platformName=windows_nt}]\nSession ID: fd731392-2653-491e-b6ac-d79c61ac066d"",
  ""systemInformation"": ""System info: host: 'WIN-SB3ER6JQ6ME', ip: '10.100.18.238', os.name: 'Windows 10', os.arch: 'x86', os.version: '10.0', java.version: '1.8.0_91'"",
  ""cause"": null,
  ""supportUrl"": ""http://seleniumhq.org/exceptions/stale_element_reference.html""
}";
    }

    string AddSomeTrailingNullCharacters(string source, int howMany)
    {
      var nullCharacters = new String('\0', howMany);
      return String.Concat(source, nullCharacters);
    }
  }
}
