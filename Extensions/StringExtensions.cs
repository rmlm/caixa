using System.Globalization;

namespace CaixaDespesas.Extensions
{
    public static class StringExtensions{
        public static decimal ToDecimal(this String value){
            if(string.IsNullOrWhiteSpace(value)) return 0;
            
            value = value.Replace(".", string.Empty);
            value = value.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

            var conveted = decimal.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);

            return conveted;
        } 
    }
}

