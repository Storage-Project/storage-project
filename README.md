# Storage REST API
## To run:
```
dotnet clean
dotnet build
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet watch run
```

## Endpoints

| Path | Description |
| :--- | :--- |
| GET v1/products | get all products |
| GET v1/products/{id} | get product by id |
| GET v1/products/filters?descriprion={description}&category={category}&quantity={quant} | filter by one or all of the parameters (category, description and quantity (lte) |
| POST v1/products | create product |
| PUT v1/products/{id} | update product by id|
| DELETE v1/products/{id} | delete product by id |


## Status Codes

estoque-api returns the following status codes in its API:

| Status Code | Description |
| :--- | :--- |
| 200 | `OK` |
| 201 | `CREATED` |
| 400 | `BAD REQUEST` |
| 404 | `NOT FOUND` |
| 500 | `INTERNAL SERVER ERROR` |
