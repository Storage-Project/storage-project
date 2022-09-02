# Storage REST API
##To run:
```
dotnet clean
dotnet build
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet watch run
```



## Status Codes

estoque-api returns the following status codes in its API:

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 201 | `CREATED` |
| 400 | `BAD REQUEST` |
| 404 | `NOT FOUND` |
| 500 | `INTERNAL SERVER ERROR` |
