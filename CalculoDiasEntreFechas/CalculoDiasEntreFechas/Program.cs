using System;

namespace CalculoDiasEntreFechas
{
    class Program
    {
        static int cantidadDias, dia, mes, anio;
        static string fecha1, fecha2;

        static void Main(string[] args)
        {
            cantidadDias = 0;

            do
            {
                Console.WriteLine("Introduce la PRIMERA fecha (DD/MM/YYYY)");
                fecha1 = Console.ReadLine();
            } while (!comprobacionDatos(fecha1)); //Sigue pidiendo la fecha hasta que el usuario escriba bien los datos

            do
            {
                Console.WriteLine("Introduce la SEGUNDA fecha (DD/MM/YYYY)");
                fecha2 = Console.ReadLine();
            } while (!comprobacionDatos(fecha2)); //Sigue pidiendo la fecha hasta que el usuario escriba bien los datos

            // La cantidad de días total será la resta de la cantidad de días que hay desde el 1 de enero del año 0 hasta cada una de las fechas
            cantidadDias = Math.Abs(calcularDias(fecha1) - calcularDias(fecha2));

            Console.WriteLine("La cantidad de días entre la fecha " + fecha1 + " y la fecha " + fecha2 + " es de:  " + cantidadDias);

        }

        /// <summary>
        ///     Comprueba si los datos dados por el usuario son correctos o no.
        /// </summary>
        /// <param name="fecha">Cadena fecha que el usuario introduce por consola</param>
        /// <returns>TRUE si son correctos, FALSE en caso contrario</returns>
        static bool comprobacionDatos(string fecha)
        {
            if (fecha.Equals("")) //Que la cadena no esté vacía
                return false;
            if (fecha.Length != 10) //Que la cadena contenga 10 elementos (esto hace que 1000 <= año <= 9999)
                return false;
            if (!fecha[2].Equals('/') || !fecha[5].Equals('/')) //Que esté separado por comas (los días y meses que solo sean un dígito deberán ir precedidos por un 0)
                return false;

            return true;
        }


        /// <summary>
        ///     Calcula los días que hay entre el 1 de enero (mes 1) del año 0 hasta la fecha dada por parámetro
        /// </summary>
        /// <param name="fecha">Cadena fecha con la que se quiere calcular la diferencia</param>
        /// <returns>Número de días</returns>
        private static int calcularDias(string fecha)
        {
            int nDias = 0;
            sacarDatos(fecha);
            for (int i = 0; i < anio; i++)
            {
                if (esBisiesto(i))
                    nDias += 366;
                else
                    nDias += 365;

            }

            for (int i = 1; i < mes; i++)
            {
                nDias += diasPorMes(i, anio);
            }

            nDias += dia;

            return nDias;
        }


        /// <summary>
        ///     Extrae los datos de la cadena "fecha" que se le pasa por parámetro
        /// </summary>
        /// <param name="fecha">Cadena fecha dada por el usuario</param>
        private static void sacarDatos(string fecha)
        {
            dia = int.Parse(fecha.Substring(0, 2));
            mes = int.Parse(fecha.Substring(3, 2));
            anio = int.Parse(fecha.Substring(6));
        }


        /// <summary>
        ///     Indica los días que tiene el mes pasado por parámetro
        /// </summary>
        /// <param name="mes">Mes del que se quieren saber los días</param>
        /// <param name="anio">Año necesario para comprobar si febrero (mes 2) tiene 28 o 29 días</param>
        /// <returns>El número de días del mes (acorde a si el año es bisiesto o no)</returns>
        private static int diasPorMes(int mes, int anio)
        {
            if (mes == 01 || mes == 03 || mes == 05 || mes == 07 || mes == 08 || mes == 10 || mes == 12)
                return 31;
            else if (mes == 04 || mes == 06 || mes == 09 || mes == 11)
                return 30;
            else
            {
                if (esBisiesto(anio))
                    return 29;
                else
                    return 28;
            }
        }

        /// <summary>
        ///     Comprueba si el año pasado por parámetro es bisiesto o no
        /// </summary>
        /// <param name="anio">Año a comprobar</param>
        /// <returns>TRUE si es bisiesto, FALSE en caso contrario</returns>
        static bool esBisiesto(int anio)
        {
            if (anio % 4 == 0 && anio % 100 != 0 || anio % 400 == 0)
                return true;
            else
                return false;
        }
    }
}
