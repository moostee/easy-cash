
FROM --platform=$BUILDPLATFORM node:18.13.0-bullseye-slim as builder


RUN mkdir /project

WORKDIR /project

RUN npm install -g @angular/cli@15


COPY package.json package-lock.json ./
RUN npm ci --legacy-peer-deps

COPY . .
CMD ["ng", "serve", "--host", "0.0.0.0"]
