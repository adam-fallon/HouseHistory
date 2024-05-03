#!/bin/bash
docker stop househistory-web && docker rm househistory-web
docker build -t househistory-web:latest .
# docker rmi $(docker images -qa -f 'dangling=true')
docker run --name househistory-web -p 8080:80 -d househistory-web:latest
