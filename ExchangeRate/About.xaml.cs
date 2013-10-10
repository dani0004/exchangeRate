using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace ExchangeRate
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        public void NavigateToData(object sender, RoutedEventArgs e)
        {
            try
            {
              
               // data_link.Foreground.SetValue("Style", "PhoneAccentBrush");
                    
                WebBrowserTask wbt = new Microsoft.Phone.Tasks.WebBrowserTask();
                wbt.Uri = new Uri("http://www.openexchangerates.org");
                wbt.Show();
            }
            catch (Exception ee)
            {
            }
        }

        private void ContactUs(object sender, EventArgs e)
        {
            //MessageBox.Show("contact us clicked");

            EmailComposeTask emailComposer = new EmailComposeTask();
            emailComposer.To = "<a href='mailto:dani0004@algonquinlive.com' target='_blank' rel='nofollow' >dani0004@algonquinlive.com</a>";
            emailComposer.Subject = "exchange rate errors";
            emailComposer.Body = "cannot download exchange rates from site";
            emailComposer.Show();

        }

        /* event handler for back button */
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}