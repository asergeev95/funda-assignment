# funda-assignment

## Solution structure
* `docker` folder: `Dockerfile` and `docker-compose` for running application in docker container
* `src` folder: api, infrastructure and business logic layers of the application
* `tests` folder: all the kinds of tests

<img width="338" alt="image" src="https://user-images.githubusercontent.com/26343229/163012725-9365d4f6-f7b3-47fa-aa62-fbd0aa532148.png">


## How to run with Docker
1. Clone a project to your local directory: `git clone https://github.com/asergeev95/funda-assignment.git`
2. Execute in command line: `cd funda-assignment`
3. Execute in command line: `docker-compose up -d --build`


## How to test

1. Run the application. If you can't use Docker launch it via Rider or VisualStudio.
2. Start any browser an go to `http://localhost:8080/swagger`
3. You will see swagger page where you can test the application

## Application requests

`POST ​/api​/v1​/real-estate​/top-agents` accept a request body 

```json
{
  "apartmentFeatures": "string",
  "take": 0
}
```
`take` stands for the number of top rentals to be returned

`apartmentFeatures` stands for the features of apartments that you're looking for. This is Flags enum under the hood so you can specify several of then like this:

```
curl -X 'POST' \
'http://localhost:8080/api/v1/real-estate/top-agents' \
-H 'accept: text/plain' \
-H 'Content-Type: application/json-patch+json' \
-d '{
"apartmentFeatures": "Tuin, Balcon, Dakterras",
"take": 7
}' | json_pp
```
```
curl -X 'POST' \
'http://localhost:8080/api/v1/real-estate/top-agents' \
-H 'accept: text/plain' \
-H 'Content-Type: application/json-patch+json' \
-d '{
"apartmentFeatures": "Tuin, Balcon",
"take": 5
}' | json_pp

```
Also you can skip this property 
```
curl -X 'POST' \
  'http://localhost:8080/api/v1/real-estate/top-agents' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "take": 5 }' | json_pp
```

Also you can skip `take` property. Default value `10` will be used. 
```
curl -X 'POST' \
  'http://localhost:8080/api/v1/real-estate/top-agents' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{}' | json_pp
```


