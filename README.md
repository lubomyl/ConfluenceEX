# ConfluenceEX

Visual Studio extension integrating Atlassian Confluence.

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
Need ti sign-in at [My testing Confluence server](lubomyl2.atlassian.net/wiki)  

##### What does it demonstrate?
REST Client integrated into ToolWindow  
After project start-up goto and click on `Tool|Confluence` if the Confluence Toolbar is not already shown    
New ToolWindow with ToolBar should show up  
  
Before successful sign-in:  
- Fill in credentials for Basic authentication *(credentials are not stored after application shutdown)*  

After successful sign-in:  
- On ToolBar click on `Change` / `Add` to test MVVM functionality of change on binding at `Home` View 
- On ToolBar click on `Connect` to check authentication state
- On ToolBar click on `Home` icon to switch back    

## Solution structure
- ConfluenceEX *(main)*
- ConfluenceRESTClient *(class library)*

## Dependencies
- RestSharp 106.2.1
