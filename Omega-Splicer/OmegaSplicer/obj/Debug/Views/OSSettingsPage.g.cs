﻿

#pragma checksum "C:\Users\dimitricisneros\Desktop\WP\OmegaSplicer\Views\OSSettingsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "819CA7ECCDFE174F9EE0A6A17587C6E8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OmegaSplicer
{
    partial class SettingsPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 60 "..\..\Views\OSSettingsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.MainPage;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 61 "..\..\Views\OSSettingsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.SaveSettings;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


