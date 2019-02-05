# HttpClientFactory

Addresses the issue with .Net 4.5's HttpClient. [Socket exhaustion](https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/) and
[Long lived instance not updating DNS](http://byterot.blogspot.com/2016/07/singleton-httpclient-dns.html).

There are 3 ways to use the new HttpClientFactory.

- Direct usage
- Named Clients
- Typed Clients

## Direct usage



## Named Clients

## Typed Clients

 

### Setup
- Console Core 2.2 project
- Add the following packages
  - Microsoft.Extensions.DependencyInjection
  - Microsoft.Extensions.Http