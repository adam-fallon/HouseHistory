#!/bin/bash
docker stop househistory-api && docker rm househistory-api
docker build -t househistory-api:latest .
# docker rmi $(docker images -qa -f 'dangling=true')
docker run --name househistory-api -p 8081:8080 -d househistory-api:latest 