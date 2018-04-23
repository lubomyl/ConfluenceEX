//------------------------------------------------------------------------------
// <copyright file="ConfluenceCommandPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using ConfluenceEX.Main;

namespace ConfluenceEX
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#1110", "#1112", "1.0", IconResourceID = 1400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(Guids.guidConfluencePackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideToolWindow(typeof(ContentListToolWindow), Style = VsDockStyle.Tabbed, Window = "3ae79031-e1bc-11d0-8f78-00a0c9110057")]
    public sealed class ConfluenceCommandPackage : Package
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfluenceCommand"/> class.
        /// </summary>
        public ConfluenceCommandPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        private void ShowContentListToolWindow(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.FindToolWindow(typeof(ContentListToolWindow), 0, true);
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        private void TestOnPropertyChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Test button clicked.");
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the tool window
                CommandID menuCommandID = new CommandID(Guids.guidConfluenceCommand, Guids.ConfluenceCommandId);
                CommandID toolbarMenuTestCommandID = new CommandID(Guids.guidConfluenceToolbarMenu, Guids.TestCommand1Id);

                MenuCommand onMenuCommandClickShowToolWindow = new MenuCommand(ShowContentListToolWindow, menuCommandID);
                MenuCommand onToolbarMenuClickTest = new MenuCommand(TestOnPropertyChanged, toolbarMenuTestCommandID);

                mcs.AddCommand(onMenuCommandClickShowToolWindow);
                mcs.AddCommand(onToolbarMenuClickTest);
            }
        }

        #endregion
    }
}
