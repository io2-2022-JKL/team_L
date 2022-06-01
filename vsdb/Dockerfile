FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

RUN apt-get -y update  && \
        apt-get install -y curl && \
        curl -sL https://deb.nodesource.com/setup_14.x | bash - && \
        apt-get install -y nodejs && \
        apt-get install -y dos2unix

RUN npm install mssql

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . /usr/src/app

RUN chmod +x /usr/src/app/import-data.sh

ENV SA_PASSWORD Pass123word!
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

EXPOSE 1433

USER mssql
CMD /bin/bash ./entrypoint.sh