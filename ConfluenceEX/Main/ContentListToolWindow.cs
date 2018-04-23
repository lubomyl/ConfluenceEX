﻿using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using ConfluenceEX.Main;
using ConfluenceEX.View;
using System.ComponentModel.Design;

namespace ConfluenceEX
{

    [Guid(Guids.guidContentListToolWindow)]
    public class ContentListToolWindow : ToolWindowPane
    {
        private readonly ContentListView _view;

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public ContentListToolWindow() : base(null)
        {
            this.Caption = Resources.ConflueceToolWindowTitle;

            _view = new ContentListView();
            base.Content = _view;
            this.ToolBar = new CommandID(Guids.guidConfluencePackage, Guids.ConfluenceToolbar);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
