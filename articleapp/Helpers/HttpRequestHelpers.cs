
using System.Text;
using System.Text.Json;


namespace articleapp.Helpers
{
    public static class HttpRequestHelper
    {
       
        public static async Task<T> PostRequest<T>(string url, object content,string? token = null)
        {
            using (var client = new HttpClient())
            {
                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                var requestContent = new StringContent(
                    JsonSerializer.Serialize(content),
                    Encoding.UTF8,
                    "application/json");
                Uri uri = new Uri(url);
                var response = await client.PostAsync(uri, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var contentType = response.Content.Headers.ContentType?.MediaType;

                    if (contentType == "application/json")
                    {
                        try
                        {
                            return JsonSerializer.Deserialize<T>(responseContent);
                        }
                        catch (JsonException ex)
                        {
                            throw new JsonException(ex.Message);
                        }
                    }
                    else
                    {
                        return (T)(object)responseContent;
                    }
                }
                else
                {
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.RequestMessage}");
                }
            }
        }




        public static async Task<T> GetRequest<T>(string url, string? token = null)
        {
            
            try
            {
                using (var client = new HttpClient()) 
                {
                    if (token != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }


                    var response = await client.GetAsync(url);
                   
                    string jsonString = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    try
                    {
                        return JsonSerializer.Deserialize<T>(jsonString, options);
                    }
                    catch
                    {
                        return default;
                    }



                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP request failed: {e.Message}");
                throw;
            }
            
        }


        public static async Task<string> DeleteRequest(string url, string? token = null, object? body = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (token != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);

                    if (body != null)
                    {
                        string jsonBody = JsonSerializer.Serialize(body);
                        request.Content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");
                    }

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    return "deleted";
                }
            }
            catch (HttpRequestException e)
            {
               
                throw new Exception(e.Message);
            }
        }




    }
}









/*
 
 public static async Task<T> PostRequest<T>(string url, object content)
  {
      using (var client = new HttpClient())
      {
          var requestContent = new StringContent(
              JsonSerializer.Serialize(content),
              Encoding.UTF8,
              "application/json");
          Uri uri = new Uri(url);
          var response = await client.PostAsync(uri, requestContent);

          if (response.IsSuccessStatusCode)
          {
              var responseContent = await response.Content.ReadAsStringAsync();
              var contentType = response.Content.Headers.ContentType?.MediaType;

              if (contentType == "application/json")
              {
                  try
                  {
                      return JsonSerializer.Deserialize<T>(responseContent);
                  }
                  catch (JsonException)
                  {
                    
                      if (typeof(T) == typeof(string))
                      {
                          return (T)(object)responseContent;
                      }
                      throw;
                  }
              }
              else
              {
                 
                  if (typeof(T) == typeof(string))
                  {
                      return (T)(object)responseContent;
                  }
                  throw new InvalidOperationException($"content unknown: {contentType}");
              }
          }

         throw new HttpRequestException($"{responseContent}");
      }
  }
 
 
 */