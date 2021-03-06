# ConfluenceEX

Visual Studio extension integrating Atlassian Confluence.

##### Testing with own Confluence instance
See: [Wiki - How to configure ConfluenceEX as consumer](https://github.com/lubomyl/ConfluenceEX/wiki/How-to-configure-ConfluenceEX-as-consumer)

##### Missing ignored settings file
See: [Wiki - Ignored setting file](https://github.com/lubomyl/ConfluenceEX/wiki/Ignored-settings-file)

##### How to debug
- Mark `ConfluenceEX` as StartUp Project  
- Set Debug mode  
- In order to load project as VSPackage in Experimental instance of VS follow [CodeProject](https://www.codeproject.com/Tips/832362/Resetting-the-Visual-Studio-Experimental-Instance) or do following:  
  1. Right-click on `ConfluenceEX project|Properties`
  2. Select `Debug` bookmark
  3. As external program navigate to: `"MicrosoftVS installation location"\Common7\IDE\devenv.exe`
  4. As command line argument on  start-up add `/RootSuffix Exp`
- Build&Start  

##### Authentication protection
Need to sign-in at [My testing Confluence server](https://lubomyl3.atlassian.net/wiki)  

##### What does it demonstrate?
REST Client integrated into ToolWindow  
After project start-up goto and click on `View|Confluence` if the Confluence Toolbar is not already shown    
New ToolWindow with ToolBar should show up  
  
Before successful sign-in:  
- If not already signed-in with oauth_access_token from registry User Settings Store click on `Redirect`
- By Signing-in and clicking on `Allow/Přijmout` give ConfluenceEX rights to make rest api calls with your identity
- Copy generated oauth_verifier (ověřovací kód) and navigate back to ConfluenceEX
- Paste copied oauth oauth_verifier (ověřovací kód) and click on `Sign-in`

After successful sign-in:  
- On ToolBar click on `Connect` to check authentication state
- On ToolBar click on `Home` icon to show list of available Spaces
- Click on specific `Space` link to see list of content available under this space
- Click on specific `Content` link to see details
- Click on `Open in build-in/external tab...` to open link in new Visual Studio built-in browser tab or external browser tab

## Solution structure
- ConfluenceEX *(main)*
- ConfluenceRESTClient *(class library)*

## Dependencies
- RestSharp 106.2.1: currently not in use *(no support for OAuth with RSA)*
- DevDefined.OAuth 0.2.0
