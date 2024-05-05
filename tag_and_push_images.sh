#!/bin/bash
docker compose build
docker tag househistory-api adamfallon.azurecr.io/househistory/househistory-api:latest
docker tag househistory-web adamfallon.azurecr.io/househistory/househistory-web:latest
docker push adamfallon.azurecr.io/househistory/househistory-api:latest
docker push adamfallon.azurecr.io/househistory/househistory-web:latest