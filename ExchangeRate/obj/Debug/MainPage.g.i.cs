﻿#pragma checksum "C:\WindowsPhoneProjects\ExchangeRate\ExchangeRate\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "70F6186111E1784721CACB6494578764"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ExchangeRate {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.StackPanel inputPanel;
        
        internal System.Windows.Controls.TextBlock baseTxt;
        
        internal System.Windows.Controls.TextBox baseVal;
        
        internal System.Windows.Controls.Button cdnBtn;
        
        internal System.Windows.Controls.Button euroBtn;
        
        internal System.Windows.Controls.Button yenBtn;
        
        internal System.Windows.Controls.Button yuanBtn;
        
        internal System.Windows.Controls.TextBlock resultTxt;
        
        internal System.Windows.Controls.TextBox resultVal;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton CloseKeypad;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem AboutExchange;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem FetchData;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/ExchangeRate;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.inputPanel = ((System.Windows.Controls.StackPanel)(this.FindName("inputPanel")));
            this.baseTxt = ((System.Windows.Controls.TextBlock)(this.FindName("baseTxt")));
            this.baseVal = ((System.Windows.Controls.TextBox)(this.FindName("baseVal")));
            this.cdnBtn = ((System.Windows.Controls.Button)(this.FindName("cdnBtn")));
            this.euroBtn = ((System.Windows.Controls.Button)(this.FindName("euroBtn")));
            this.yenBtn = ((System.Windows.Controls.Button)(this.FindName("yenBtn")));
            this.yuanBtn = ((System.Windows.Controls.Button)(this.FindName("yuanBtn")));
            this.resultTxt = ((System.Windows.Controls.TextBlock)(this.FindName("resultTxt")));
            this.resultVal = ((System.Windows.Controls.TextBox)(this.FindName("resultVal")));
            this.CloseKeypad = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("CloseKeypad")));
            this.AboutExchange = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("AboutExchange")));
            this.FetchData = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("FetchData")));
        }
    }
}

