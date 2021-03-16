## Getting Started

You can run solution from Visual Studio 2019 IDE via IIS or run following commands from solution root:

```powershell
dotnet build
dotnet run --project ./src/Photo.Manufacture.Api/Photo.Manufacture.Api.csproj
```

Depending on the host (IIS or Kestrel) substitute Url below:

Create order

```shell
curl -X POST "https://localhost:44366/Orders" -H  "accept: application/json" -H  "Content-Type: application/json-patch+json" -d "{\"orderId\":\"TestOrder\",\"orderItems\":[{\"productId\":1,\"quantity\":1},{\"productId\":2,\"quantity\":2},{\"productId\":5,\"quantity\":2}]}"
```

Get order by id

```shell
curl -X GET "https://localhost:44366/Orders/1" -H  "accept: text/plain"
```

Get all orders

```shell
curl -X GET "https://localhost:44366/Orders" -H  "accept: text/plain"
```

Run tests:

```powershell
dotnet test ./tests/Photo.Manufacture.UnitTests/Photo.Manufacture.UnitTests.csproj
```



Danyl Novhorodov