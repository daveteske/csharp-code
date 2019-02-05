## HttpClientFactory usages

Addresses the issue with .Net 4.5's HttpClient. [Socket exhaustion](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/) and
[Long lived instance not updating DNS](http://byterot.blogspot.com/2016/07/singleton-httpclient-dns.html).

3(4) ways to use the new HttpClientFactory

- Direct usage
- Named Clients
- Typed Clients
  - Encapsulated Type Clients

## Direct usage

Pretty much like the old way, except none of the old problems.

## Named Clients

Allows you to centralize the pre-configured settings for a specific host. Set the baseurl add headers.
Then just use it by **name**.

```csharp
var client = httpClientFactory.CreateClient("wikipediaClient");
var result = await client.GetStringAsync("/wiki/AltaVista");
```

## Typed Clients

Custom class to hold the setting. Strongly typed, no strings like Named Clients.

```csharp
var client = serviceProvider.GetRequiredService<WikiClient>();
var result = await client.Client.GetStringAsync("/wiki/AltaVista");
``` 

## Encapsulated Typed Clients

The basic Typed client still exposes HttpClient(). Also we can add logic to the endpoints instead of scattering it throughout the code.
```csharp
var client = serviceProvider.GetRequiredService<IEncapsulatedWikiClient>();
var result = await client.GetAltaVistaArticle();
``` 

### Setup
- Console Core 2.2 project
- Add the following packages
  - Microsoft.Extensions.DependencyInjection
  - Microsoft.Extensions.Http