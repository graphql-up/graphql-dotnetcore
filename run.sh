#!/bin/bash

dotnet restore
npm i
npm run build
dotnet run
