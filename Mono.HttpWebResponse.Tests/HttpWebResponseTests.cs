﻿//
// Test.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2018 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace Mono.HttpWebResponse.Tests
{
  [TestFixture]
  public class HttpWebResponseTests
  {
    const string ApiUrlBase = "http://localhost:8080/api/MyApi?success=";

    static readonly Uri
      ApiUriForOKResponse = new Uri(String.Concat(ApiUrlBase, Boolean.TrueString.ToLowerInvariant())),
      ApiUriForInternalServerErrorResponse = new Uri(String.Concat(ApiUrlBase, Boolean.FalseString.ToLowerInvariant()));

    /* The ORIGINAL reproduction case/code for this is:
     * https://github.com/SeleniumHQ/selenium/blob/selenium-3.4.0/dotnet/src/webdriver/Remote/HttpCommandExecutor.cs#L143
     * 
     * (the highlighted line is where the crash occurred).
     */

    [Test]
    public void StreamReader_ReadToEnd_should_not_raise_exception_for_200_OK_web_request()
    {
      // Arrange
      var response = GetResponse(ApiUriForOKResponse);
      if(response == null) Assert.Pass("Response was null, but that's OK");

      // Act
      StreamReader reader = null;

      // Intentionally not a using block with Dispose, in order to mirror original repro code
      try
      {
        var responseStream = response.GetResponseStream();
        reader = new StreamReader(responseStream, Encoding.UTF8);

        // Assert
        Assert.That(() => reader.ReadToEnd(), Throws.Nothing);
      }
      finally
      {
        if(reader != null)
          reader.Close();
      }
    }

    [Test]
    public void StreamReader_ReadToEnd_should_not_raise_exception_for_500_InternalServerError_web_request()
    {
      // Arrange
      var response = GetResponse(ApiUriForInternalServerErrorResponse);
      if(response == null) Assert.Pass("Response was null, but that's OK");

      // Act
      StreamReader reader = null;

      // Intentionally not a using block with Dispose, in order to mirror original repro code
      try
      {
        var responseStream = response.GetResponseStream();
        reader = new StreamReader(responseStream, Encoding.UTF8);

        // Assert
        Assert.That(() => reader.ReadToEnd(), Throws.Nothing);
      }
      finally
      {
        if(reader != null)
          reader.Close();
      }
    }

    System.Net.HttpWebResponse GetResponse(Uri uri)
    {
      var request = (HttpWebRequest) HttpWebRequest.Create(uri);

      request.Timeout = 2000;
      request.Accept = "application/json, image/png";
      request.KeepAlive = true;
      request.ServicePoint.ConnectionLimit = 2000;

      return GetResponseWhichMightBeResultOfException(request);
    }

    System.Net.HttpWebResponse GetResponseWhichMightBeResultOfException(HttpWebRequest request)
    {
      try
      {
        return request.GetResponse() as System.Net.HttpWebResponse;
      }
      catch(WebException ex)
      {
        // Access these two properties from the exception,
        // because the original repro case does
        object throwAway = ex.Status;
        throwAway = ex.Response;

        return ex.Response as System.Net.HttpWebResponse;
      }
    }
  }
}
