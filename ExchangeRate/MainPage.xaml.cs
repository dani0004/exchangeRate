using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExchangeRate.Resources;
using System.Windows.Input;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using System.Collections;
using System.Threading.Tasks;
using System.Globalization;

namespace ExchangeRate
{
    /* This is the View */
    public partial class MainPage : PhoneApplicationPage
    {
        /* local variables */
        float exchangeRate=0.00f;
        float baseAmount=0.00f;
        String localeString = "";
        String usSymbol = "USD";
        bool baseTrue = true;
        private bool localType = true;
        private bool errorState = false;
        private int tindex = 0;
        bool msgWritten = false;

        ExchangeRetriever exchangeObj;
      

        //use for testing
        private double usdConvert = 1.13;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            exchangeObj = new ExchangeRate.ExchangeRetriever();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        /* tap event handlers ****/
        
        private void Rate_tap_1(Object sender, RoutedEventArgs e)
        {
            Button pp =(Button) sender;
           
           
          
            GetBaseValue();
            if (baseTrue)
            {
                errorState = false;
                localType = false;
                if (localType)
                {
                    CalculateConversionLocal(pp.Name);
                }
                else
                {
                     CalculateConversionFromSite(pp.Name);
                }

                if (!errorState)
                {
                    float cnvFloat = baseAmount * exchangeRate;
                    //formats string as currency the current locale is en-US which gives $
                    //read the whole float second argument from index 0
                    // String aa = String.Format("{0:C}", cnvFloat);
                    resultVal.Text = GetLocalizedCurrency(cnvFloat);

                    bool fromFile = exchangeObj.GetFromFile();

                    if (fromFile)
                    {
                        if (!msgWritten)
                        {
                            CreateInfoBox();
                            msgWritten = true;
                        }
                    }

                    resultVal.IsEnabled = false;
                }
                else { this.Focus(); }
            }
           
        }
        /* the function CalculateConversionLocal
        * Used for testing
         * calls a function in the Model LocalExchangeRetriever
        * @param - String the button that was pressed
        * **/
        private void CalculateConversionLocal(String aa)
        {

            String country = DetermineCountry(aa); 
            LocalExchangeRetriever localRet = new LocalExchangeRetriever();
     
            localRet.GetConvertRate(country);
            exchangeRate = localRet.GetExchangeRate();
           
           
        }
        /* the function DetermineCountry
        * Determines the country code given the button that was pressed
        * @param - String the button that was pressed
        * **/
        private String DetermineCountry(String aa)
        {
            String country = "";

            switch (aa)
            {
                case "cdnBtn":
                    country = ("CAD");
                    localeString = "en-US";
                    break;
                case "euroBtn":
                    country = ("EUR");
                    localeString = "fr-FR";
                    break;
                case "yenBtn":
                    country = ("JPY");
                    localeString = "ja-JP";
                    break;
                case "yuanBtn":
                    country = ("CNY");
                    localeString = "zh-CN";
                    break;
            }
            return country;
        }
        /* the function CalculateConversionFromSite
        * Calls a function in the Model exchangeObj to get the rate
        * @param - String the button that was pressed
        * **/
        private void CalculateConversionFromSite(String aa)
        {

            String country = DetermineCountry(aa); 
            exchangeRate = exchangeObj.GetExchangeRate(country);
            if (exchangeRate ==-1.0)
            {
                DisplayError("Error: Currency country code is not found");
                errorState = true;
            }
            if (exchangeRate == -2.0)
            {
                DisplayError("External services not available at this time; will use stored values");
                CalculateConversionLocal(aa);
                if (!msgWritten)
                {
                    CreateInfoBox();
                    msgWritten = true;
                }
                errorState = false;
            }
            
        }

       

        /**Other event handlers  **/
        /* the function AboutButton_Click
         * navigates to the about page
        * @param - Object sender an event object
         * @param -EventArgs the event that was generated
        * **/
        /* Click event handler for About button */
        //Use Windows Navigation service to navigate to new page in app
        private void AboutButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }
        /* the function BaseGotFocus
        *reinitializes the state of the view
       * @param - Object sender an event object
        * @param - RoutedEventArgs the event that was generated
       * **/
        public void BaseGotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                baseVal.Text = " ";
                resultVal.Text = " ";
                inputPanel.Children.RemoveAt(tindex-1);
                msgWritten = false;
                baseVal.IsEnabled = true;
                baseVal.Focus();
            }
            catch (Exception ee)
            {
            }
        }

        public void Fetch_Click(object sender, EventArgs e)
        {
            exchangeObj.RetrieveExchangeRates();
        }
        /*  end event handlers **/

        /* other functions **/
        /* add currency symbol to given string of converted currency **/ 
        private String GetLocalizedCurrency(float aa)
        {

            // create a "Culture" specifier
            CultureInfo cultureSelected = new CultureInfo(localeString);   
     
            String bb = string.Format(cultureSelected, "{0:C}", aa);    
           
            return bb;

  
        }

        public void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            ApplicationBarIconButton ic = (ApplicationBarIconButton)sender;

            if (ic.Text.Trim() == "close")
            {
                
                exchangeObj.RetrieveExchangeRates();
                this.Focus();
            }
        }
        


        private bool GetBaseValue()
        {
             baseTrue = false;
            try
            {
                //retrieve value of base val text box
                // String bb= baseVal.Text;
                //double doubleResult;
                baseAmount = System.Convert.ToSingle(baseVal.Text);
                // bool trueFalse = double.TryParse(bb, out doubleResult);

                
               baseTrue=true;
            }
            catch (Exception ex) {
                baseTrue = false;
                DisplayError("Error - currency value entered must be numeric");
                resultVal.Text = " ";
                baseVal.Focus();
            }
            return baseTrue;
           
        }

        /**error handlers **/
        private void DisplayError(String aa)
        {
            MessageBox.Show(aa);
        }

        private void CreateInfoBox()
        {
            TextBox new_textbox = new TextBox();
            int mm = inputPanel.Children.Count;
            new_textbox.Name = "txt" + tindex;
            new_textbox.IsEnabled = false;
            mm++;
            tindex = mm;
            
           new_textbox.Text = "Reporting from stored values last collected at 0.00 HRS GMT";

           inputPanel.Children.Add(new_textbox);


        }

        /* end other functions **/

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}