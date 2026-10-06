# File      : server/db/docker/Dockerfile
# Developer : Mohamed El-Farsisi (M2)
# Reviewer  : Yasmeen Ayman (M3)
# Task(s)   : T010
# The official SQL Server image has no Full-Text Search, so add the package (FSD §4.3).
# Azure SQL Database already includes it. Check Microsoft's "Install SQL Server Full-Text Search on Linux" if the build breaks.
FROM mcr.microsoft.com/mssql/server:2022-latest
USER root
RUN apt-get update && apt-get install -y curl gnupg \
 && curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoftprod.gpg \
 && curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list \
      -o /etc/apt/sources.list.d/mssql-server-2022.list \
 && apt-get update && apt-get install -y mssql-server-fts && rm -rf /var/lib/apt/lists/*
USER mssql
