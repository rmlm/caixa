using System.Globalization;
using CaixaDespesas.Models;

namespace CaixaDespesas.Extensions
{
    public static class MovementExtensions{
        public static string ToDetail(this Movement movement, bool showDate){
            if(movement == null) return string.Empty;

            if(showDate)
                return $"{movement.Date:dd-MM-yyyy} - {movement.Description} - {movement.Ammount}€";

            return $"{movement.Description} - {movement.Ammount}€";
        } 
    }
}

