# DaemonApp-ProtectedApi-AzureAD-OAuth-Jwt

This repo contains two applications
1. .Net core Console app
2. asp.net core api app

These apps are used to depict a sample of how to configure a daemon console app and a protected webapi in order to authenticate console app in webapi using Azure active directory. 

It follow OAuth pattern. Console app 1st calls Azure active directory to obtain a token which then passes the access token as a bearer token in http request's authentication header to webapi. 

Webapi will then check the token in the http request header against Azure active directory and once authenticated, it will send the response back to console app. 

Note: 
1. We need to register the webapi in Azure active directory and configure settings (client secret credentials / expose api / api permission for the app roles configured in manifest)
2. For demo purpose, I have placed the secret in console appsettings.json file. 
   But it is not recommended for production application. Secrets can be placed in key store (ex: Azure Key Vault) and could be used in application.


Feel free to contact me if you need more help or Open a issue or pull request if you find anything wrong / needs to be improved. 

DEPENDENCIES:
1. .Net Core 
2. Asp .Net Core
3. Azure account
4. VS Code
5. GIT
6. Internet connection
