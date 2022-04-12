# funda-assignment

## How to run with Docker
1. Clone a project to your local directory: `git clone https://github.com/asergeev95/funda-assignment.git`
2. Execute in command line: `cd funda-assignment`
3. Execute in command line: `docker-compose up -d --build`


## How to test
Based on previous step (How to run with Docker) 

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
}'
```
```
curl -X 'POST' \
'http://localhost:8080/api/v1/real-estate/top-agents' \
-H 'accept: text/plain' \
-H 'Content-Type: application/json-patch+json' \
-d '{
"apartmentFeatures": "Tuin, Balcon",
"take": 5
}'

```
Also you can skip this property 
```
curl -X 'POST' \
  'http://localhost:8080/api/v1/real-estate/top-agents' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
  "take": 5 }'
```

Also you can skip `take` property. Default value `10` will be used. 
```
curl -X 'POST' \
  'http://localhost:8080/api/v1/real-estate/top-agents' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{}'
```


