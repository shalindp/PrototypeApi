#!/bin/bash

git reset --hard origin/master

git pull

dotnet restore

dotnet publish -o /var/www/app1

systemctl restart app1.service