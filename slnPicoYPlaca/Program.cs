using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace slnPicoYPlaca
{
    public class Program
    {
        static List<string> siglas = new List<string>();
        static List<string> provincias = new List<string>();
        static string lic = null;
        
        static DateTime Fecha = new DateTime();
        static int hora = -1;
        static string inputdate = null;
        static string selectedstate = null;
        static void Main(string[] args)
        {
            //Load posible license plate number
            siglas = LoadLoadSiglas(siglas);
            //Load Ecudorian States
            provincias = LoadProvincias(provincias);
            // Ask for License plate number
            Console.WriteLine("Ingrese la matricula o placa a validar , y presione la tecla Enter");
            bool cont = true;
            while (cont)
            {
                lic = Convert.ToString(Console.ReadLine());
                int validation = ValidarPlaca(lic);                
                switch (validation)
                {
                    case 0:
                        cont = false;
                        Console.WriteLine("Ingrese la Fecha (YYYY-MM-dd), y presione la tecla Enter");
                        break;
                    case 1:
                        Console.WriteLine("Matricula o Placa Erronea, los tres primeros digitos solo se permiten letras \n Ingrese nuevamente");
                        break;
                    case 2:
                        Console.WriteLine("Matricula o Placa Erronea \n Ingrese nuevamente");
                        lic = Convert.ToString(Console.ReadLine());
                        break;
                }

            }
            bool fecha = true;
            while (fecha)
            {
                inputdate= Convert.ToString(Console.ReadLine());
                try
                {
                    Fecha = DateTime.Parse(inputdate);
                    Console.WriteLine("Ingrese la hora en la cual desea circular, y presione la tecla Enter");
                    fecha = false;
                }
                catch
                {
                    Console.WriteLine("Ingreso de fecha erronea.\nIngrese la Fecha (YYYY-MM-dd), y presione la tecla Enter");
                }              
            }
            bool horaval = true;
            while (horaval)
            {
                string inputhora = Convert.ToString(Console.ReadLine());
                bool val = ValidarHora(inputhora);
                bool valrango = ValidarRangoHora(hora);
                if (val && valrango)
                {
                    int lastdigit = int.Parse(lic.Substring((lic.Length - 1), 1));
                    bool valcirculacion = ValidarCirculamientio(lastdigit, hora);

                    if (valcirculacion)
                    {
                        Console.WriteLine("Su carro con placas:" + lic + "\nProveniente de la provincia de:" + selectedstate + "\nPuede circular con normalidad en el distrito Metropolinato de quito");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Su carro con placas: " + lic + "\nProveniente de la provincia de: " + selectedstate + "\n No puede circular con normalidad en el distrito Metropolinato de quito");
                        Environment.Exit(0);
                    }
                }
                else if(!val)
                {
                    Console.WriteLine("Ingreso de hora erronea.\nSolo se permite ingresar valores enteros entre 0 - 24, no valores de tipo cadena.\nIngrese nuevamente el valor");
                }else if (!valrango)
                {
                    Console.WriteLine("Ingreso de hora erronea.\nSolo se permite ingresar valores enteros entre 0 - 24. \nIngrese nuevamente el valor");
                }
                
            }            
        }
        
        //Evaluar entrada hora
        private static bool ValidarHora(string horaingresada)
        {
            int output = -1;
            if (int.TryParse(horaingresada, out output))
            {
                hora = output;
                return true;
            }
            else
            {
                return false;
            }
            
        }
        //Evaluar rangos hora
        private static bool ValidarRangoHora(int horaingresada)
        {
            if (horaingresada>=0 && horaingresada<=24)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        //Evaluar Placa
        private static bool ValidarCirculamientio(int numero, int hora)
        {
            int dayoftheweek=(int)Fecha.DayOfWeek;           
            switch (dayoftheweek)
            {
                case 1:
                    if (numero==1 || numero==2)
                    {
                        if (hora>=5 && hora<=24)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;                     
                    }
                    
                case 2:
                    if (numero == 3 || numero == 4)
                    {
                        if (hora >= 5 && hora <= 24)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;

                    }
                case 3:
                    if (numero == 5 || numero == 6)
                    {
                        if (hora >= 5 && hora <= 24)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;

                    }
                    
                case 4:
                    if (numero == 7 || numero == 8)
                    {
                        if (hora >= 5 && hora <= 24)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                case 5:
                    if (numero == 9 || numero == 0)
                    {
                        if (hora >= 5 && hora <= 24)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;

                    }
                case 9:
                    return true;
                case 0:
                    return true;
            }
            return false;

        }

        private static int ValidarPlaca(string placa)
        {
            placa = placa.ToUpper();
            if (Regex.IsMatch(placa, @"^[a-zA-Z]+(?:-[0-9]+)*$") && placa.Length==8)
            {
                string validate = placa.Substring(0,2);
                int val = -1;
                if (!int.TryParse(validate,out val))
                {
                    string sigla = placa.Substring(0,1);
                    bool valsigla = siglas.Contains(sigla);
                    if (valsigla)
                    {
                        selectedstate = provincias[siglas.IndexOf(sigla)];
                        return 0;
                    }
                    else
                    {
                        return 3;
                    }                  
                }
                else
                {
                    return 1;
                }
            }
            else if(Regex.IsMatch(placa, @"^[a-zA-Z0-9]") && placa.Length==7)
            {
                string validate = placa.Substring(0, 2);
                int val = -1;
                if (!int.TryParse(validate, out val))
                {
                    string sigla = placa.Substring(0, 1);
                    bool valsigla = siglas.Contains(sigla);
                    string lastnumber = placa.Substring((placa.Length-1),1);
                    int vald = -1;
                    if (valsigla && int.TryParse(lastnumber,out vald))
                    {
                        selectedstate = provincias[siglas.IndexOf(sigla)];
                        return 0;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 2;
            }   
        }

        private static List<string> LoadLoadSiglas(List<string> siglas)
        {
            siglas.Add("A");
            siglas.Add("W");
            siglas.Add("S");
            siglas.Add("B");
            siglas.Add("G");
            siglas.Add("P");
            siglas.Add("U");
            siglas.Add("I");
            siglas.Add("Q");
            siglas.Add("C");
            siglas.Add("L");
            siglas.Add("K");
            siglas.Add("X");
            siglas.Add("R");
            siglas.Add("T");
            siglas.Add("H");
            siglas.Add("M");
            siglas.Add("Z");
            siglas.Add("O");
            siglas.Add("V");
            siglas.Add("Y");
            siglas.Add("E");
            siglas.Add("N");
            siglas.Add("J");
            return siglas;
        }

        private static List<string> LoadProvincias(List<string> provincias)
        {
            provincias.Add("Azuay");
            provincias.Add("Galápagos");
            provincias.Add("Pastaza");
            provincias.Add("Bolívar");
            provincias.Add("Guayas");
            provincias.Add("Pichincha");
            provincias.Add("Cañar");
            provincias.Add("Imbabura");
            provincias.Add("Orellana");
            provincias.Add("Carchi");
            provincias.Add("Loja");
            provincias.Add("Sucumbíos");
            provincias.Add("Cotopaxi");
            provincias.Add("Los Rios");
            provincias.Add("Tungurahua");
            provincias.Add("Chimborazo");
            provincias.Add("Manabí");
            provincias.Add("Zamora Chinchipe");
            provincias.Add("El Oro");
            provincias.Add("Morona Santiago");
            provincias.Add("Santa Elena");
            provincias.Add("Esmeraldas");
            provincias.Add("Napo");
            provincias.Add("Santo Domingo de los Tsáchilas");
            return provincias;
        }
    }
}
