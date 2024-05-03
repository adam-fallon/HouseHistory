#!/bin/bash
APP_NAME="househistory-web"
docker stop $(docker ps -a -q) && docker rm $(docker ps -a -q)

DOCKERFILE_PATH="Dockerfile"
IMAGE_NAME="househistory-web"
IMAGE_TAG="latest"

docker build -t "$IMAGE_NAME:$IMAGE_TAG" .
docker rmi $(docker images -qa -f 'dangling=true')
docker run -d -p 8080:80 "$APP_NAME"
