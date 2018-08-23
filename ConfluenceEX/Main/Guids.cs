using System;

namespace ConfluenceEX.Main
{
    static class Guids
    {
        public const string GUID_CONFLUENCE_COMMAND_STRING = "41B9DDCE-7D88-4F3B-828B-B94CB6BCD659";
        public const string GUID_CONFLUENCE_TOOL_WINDOW_STRING = "7DBF4838-11BB-4532-A01E-5032EA568D22";
        public const string GUID_CONFLUENCE_PACKAGE_STRING = "1b707c1d-1af7-4e9e-8efb-6af8e4d465b7";
        public const string GUID_CONFLUENCE_TOOLBAR_MENU_STRING = "1E127E55-FDD7-4091-AA9F-AD0465FAF33F";

        public const int CONFLUENCE_TOOLBAR_ID = 0x1000;
        public const int CONFLUENCE_COMMAND_ID = 0x0101;

        public const int COMMAND_HOME_ID = 0x0129;
        public const int COMMAND_BACK_ID = 0x0130;
        public const int COMMAND_FORWARD_ID = 0x0131;
        public const int COMMAND_CONNECTION_ID = 0x0132;
        public const int COMMAND_REFRESH_ID = 0x0133;

        public static readonly Guid guidConfluenceCommand = new Guid(GUID_CONFLUENCE_COMMAND_STRING);
        public static readonly Guid guidConfluencePackage = new Guid(GUID_CONFLUENCE_PACKAGE_STRING);
        public static readonly Guid guidConfluenceToolbarMenu = new Guid(GUID_CONFLUENCE_TOOLBAR_MENU_STRING);
    };
}
