﻿using System;

namespace ConfluenceEX.Main
{
    static class Guids
    {
        public const string guidConfluenceCommandString = "41B9DDCE-7D88-4F3B-828B-B94CB6BCD659";
        public const string guidConfluenceToolWindow = "7DBF4838-11BB-4532-A01E-5032EA568D22";
        public const string guidConfluencePackageString = "1b707c1d-1af7-4e9e-8efb-6af8e4d465b7";
        public const string guidConfluenceToolbarMenuString = "1E127E55-FDD7-4091-AA9F-AD0465FAF33F";

        public const int ConfluenceToolbar = 0x1000;
        public const int ConfluenceCommandId = 0x0101;

        public const int TestCommandHome = 0x0129;
        public const int TestCommandEditId = 0x0130;
        public const int TestCommandAddId = 0x0131;
        public const int TestCommandConnectionId = 0x0132;
        public const int TestCommandRefreshId = 0x0133;

        public static readonly Guid guidConfluenceCommand = new Guid(guidConfluenceCommandString);
        public static readonly Guid guidConfluencePackage = new Guid(guidConfluencePackageString);
        public static readonly Guid guidConfluenceToolbarMenu = new Guid(guidConfluenceToolbarMenuString);
    };
}
