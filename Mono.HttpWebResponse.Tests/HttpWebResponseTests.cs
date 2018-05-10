//
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

    [Test]
    public void StreamReader_ReadToEnd_should_not_raise_exception_for_200_OK_web_request()
    {
      // Arrange
      var response = GetResponse(ApiUriForOKResponse);

      // Act
      var responseStream = response.GetResponseStream();
      using(var reader = new StreamReader(responseStream, Encoding.UTF8))
      {
        // Assert
        Assert.That(() => reader.ReadToEnd(), Throws.Nothing);
      }
    }

    [Test]
    public void StreamReader_ReadToEnd_should_not_raise_exception_for_500_InternalServerError_web_request()
    {
      // Arrange
      var response = GetResponse(ApiUriForInternalServerErrorResponse);

      // Act
      var responseStream = response.GetResponseStream();
      using(var reader = new StreamReader(responseStream, Encoding.UTF8))
      {
        // Assert
        Assert.That(() => reader.ReadToEnd(), Throws.Nothing);
      }
    }

    System.Net.HttpWebResponse GetResponse(Uri uri)
    {
      var request = (HttpWebRequest) WebRequest.Create(uri);
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
        return ex.Response as System.Net.HttpWebResponse;
      }
    }
  }
}
