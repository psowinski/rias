#!/bin/sh

GROUPNAME=riasResourcesGroup
WEBAPPNAME=riaswebapp

#Create group for resources
az group create --location westeurope --name $GROUPNAME

#Web app --------------------------
#create app service plan
az appservice plan create --name $WEBAPPNAME --resource-group $GROUPNAME --sku FREE
#create webapp
az webapp create --name $WEBAPPNAME --resource-group $GROUPNAME --plan $WEBAPPNAME
#prepare deployment zip
rm -r ./Rias.Client/dist/*
npm run --prefix ./Rias.Client build
cd ./Rias.Client/dist
zip -r $WEBAPPNAME.zip .
cd ../..
#create dev slot
#az webapp deployment slot create --name $WEBAPPNAME --resource-group $GROUPNAME #--slot dev
#deploy from git
#az webapp deployment source config --name $WEBAPPNAME --resource-group $GROUPNAME --slot dev \
#    --repo-url $GITREPO --branch master --manual-integration
#deploy from zip
az webapp deployment source config-zip --name $WEBAPPNAME --resource-group $GROUPNAME \
    --src ./Rias.Client/dist/$WEBAPPNAME.zip #--slot dev
#swap wormed slots
#az webapp deployment slot swap --name $WEBAPPNAME --resource-group $GROUPNAME --slot dev
#remove zip
rm ./Rias.Client/dist/$WEBAPPNAME.zip
#----------------------------------

echo http://$WEBAPPNAME.azurewebsites.net
