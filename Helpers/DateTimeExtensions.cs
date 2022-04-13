using System;

namespace SmartSchool.API.Helpers
{
    public static class DateTimeExtensions
    {
        //significa que é extensivo ao tipo this DateTime data
        public static int pegarIdadeAtual(this DateTime dateTime)
        {
            var dataAtual = DateTime.UtcNow;
            int idade = dataAtual.Year - dateTime.Year;

            if (dataAtual < dateTime.AddYears(idade)) idade--;

            return idade;
        }
    }
}
