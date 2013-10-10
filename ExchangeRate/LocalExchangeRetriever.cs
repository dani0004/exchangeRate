using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate
{
    class LocalExchangeRetriever
    {
        private float exchangeRate = 0.00f;
        public LocalExchangeRetriever()
        {
        }

        public void GetConvertRate(String country)
        {
            switch (country)
            {
                case "CAD":
                    exchangeRate = 1.13f;
                    break;
                case "EUR":
                    exchangeRate = 1.50f;
                    break;
                case "JPY":
                    exchangeRate = 0.97f;
                    break;
                case "CHN":
                    exchangeRate = 0.96f;
                    break;

            }

            
        }

        /* the function GetExchangeRate
         * interface function
         * @return float - the exchange rate
         * **/
        public float GetExchangeRate()
        {
            return exchangeRate;
        }
    }
}
