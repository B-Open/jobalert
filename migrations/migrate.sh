#!/usr/bin/env bash

if [ -z $1 ]
then
    echo "Usage:"
    echo "    - Migrate to latest version: ./migrate.sh up"
    echo "    - Migrate up 1 version: ./migrate.sh up 1"
    echo "    - Migrate down 1 version: ./migrate.sh down 1"
    echo ""
    echo "Environment variables default values:"
    echo "    - DB_HOST: host.docker.internal"
    echo "    - DB_DATABASE: jobalert"
    echo "    - DB_PORT: 3306"
    echo "    - DB_PASSWORD: secret"
    exit 1
fi

if [ -z $DB_HOST ]
then
    DB_HOST='host.docker.internal'
fi

if [ -z $DB_DATABASE ]
then
    DB_DATABASE='jobalert'
fi

if [ -z $DB_PASSWORD ]
then
    DB_PASSWORD='secret'
fi

if [ -z $DB_PORT ]
then
    DB_PORT='3306'
fi

docker run --rm -v $PWD:/migrations migrate/migrate \
    -path=/migrations \
    -database=mysql://root:$DB_PASSWORD@tcp\($DB_HOST:$DB_PORT\)/$DB_DATABASE \
    $@

