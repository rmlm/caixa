using System.Globalization;
using CaixaDespesas.Models;

namespace CaixaDespesas.Extensions
{
    public static class MovementExtensions{
        public static string ToDetail(this Movement movement){
            if(movement == null) return string.Empty;

            return $"{movement.Date:dd-MM-yyyy} - {movement.Description} - {movement.Ammount} €";
        } 
    }
}

