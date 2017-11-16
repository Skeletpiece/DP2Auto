﻿using DP2_Auto_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Convertions))]
namespace DP2_Auto_App.Models
{
    class Convertions : IConvertionsIT
    {
        public void ConSend()
        {
        }
        public void ConReceived()
        {
            //start
            var startcad = "";
            var check = "";
            var lngcad = "";
            int lgcad = 0;

            string[] autostart;
            autostart = new string[4];
            int i = 0;

            var checksum = "";

            String value = "7EAB04ACE9F01564F04584F05828F064837E0404";
            var chars = value.ToCharArray();
            System.Diagnostics.Debug.WriteLine("Clave original: " + value);
            for (int ctr = 0; ctr < chars.Length; ctr++)
            {
                if (ctr < 4) // cabecera de la cadena
                {
                    startcad += chars[ctr];
                    if (ctr == 0 || ctr == 1)
                    {
                        check += chars[ctr];
                    }
                }
                else if (ctr > 3 && ctr < 6) // numeros de sensores que envia
                {
                    lngcad += chars[ctr];
                }
                else if (ctr > 5 && ctr < 10) // estado del auto
                {
                    var cad = chars[ctr].ToString();
                    autostart[i] = cad;
                    i++;
                    if (i == 3)
                    {
                        check += lngcad + lngcad;
                        Int32.TryParse(lngcad, out lgcad);
                    }
                }
                else if (ctr < chars.Length && ctr > chars.Length - 7)
                {
                    checksum += chars[ctr];
                }
            }
            // validando la data
            if (startcad == "7EAB" && chars.Length == 16 + 6 * lgcad && checksum == check)
            {
                double[] sensors;
                sensors = new double[6];

                int npass = 0;
                var lngsen = "";
                var lgsen = 0;
                var str = "";
                double istr = 0;

                for (int ctr = 10; ctr < chars.Length; ctr++) // recorriendo la data
                {
                    if (ctr > 9 && ctr < (10 + 6 * lgcad))
                    {
                        var cad = chars[ctr].ToString();
                        if (npass > 0 && npass < 3)
                        {
                            lngsen += cad;
                        }
                        else if (npass == 3 || npass == 4)
                        {
                            str += cad;
                        }
                        if (npass == 2)
                        {
                            Int32.TryParse(lngsen, out lgsen);
                        }
                        else if (npass == 5)
                        {
                            istr = Convert.ToInt32(str, 16);
                            sensors[lgsen - 1] = istr + double.Parse(cad) / 10;
                            str = lngsen = "";
                            npass = -1;
                            lgsen = 0;
                        }
                        npass++;
                    }
                }
                System.Diagnostics.Debug.WriteLine(sensors[0] + "," + sensors[1] + "," + sensors[2] + "," + sensors[3] + "," + sensors[4] + "," + sensors[5]);
            }
            /* Salida de variables
             *  
             * autostart = array con los valores de los sensores
             * sensors = array con valores de sensores 
            */

            //finish
        }
    }
}