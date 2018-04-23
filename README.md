# ConfluenceEX

Visual Studio extension integrating Atlassian Confluence.

##### How to debug
Mark `ConfluenceEX` as StartUp Project  
Set Debug mode  
Build&Start  

##### What does it demonstrate?
REST Client integrated into ToolWindow  
After project start-up goto and click on `Tool|Confluence`  
New ToolWindow with ToolBar should show up  
On ToolBar click on `Change` / `Add` to test MVVM functionality of change on binding  

## Solution structure
- ConfluenceEX *(main)*
- ConfluenceRESTClient *(class library)*

## Dependencies
- RestSharp 106.2.1
