﻿

#pragma checksum "C:\Users\dimit_000\Desktop\WP\OmegaSplicer\Joystick.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "585FF4742B5DA4D23DA4A14780B979FD"
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
    partial class Joystick : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 39 "..\..\Joystick.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).ManipulationStarted += this.ellipseSense_ManipulationStarted;
                 #line default
                 #line hidden
                #line 39 "..\..\Joystick.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).ManipulationCompleted += this.ellipseSense_ManipulationCompleted;
                 #line default
                 #line hidden
                #line 39 "..\..\Joystick.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).ManipulationDelta += this.ellipseSense_ManipulationDelta;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


