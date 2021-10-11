using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino_Cs_OOP
{
    class Ficha
    {
        private readonly int[] valores;

        /*1 para en pozo, 2 para en manos de un jugador, 3 para puesta en el tablero*/
        private int estado;

        public Ficha()
        {
            valores = new int[2];

            for(int i = 0; i < 2; i++)
            {
                valores[i] = 0;
            }

            estado = 0;
        }

        public Ficha(int valor1, int valor2)
        {
            valores = new int[2];

            valores[0] = valor1;
            valores[1] = valor2;

            estado = 1;
        }

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public int[] Valores
        {
            get { return valores; }
        }

        public static bool FichaMasAltaEntre(Ficha f1, Ficha f2)
        {
            if (f1.Valores[0] > f2.Valores[0])
            {
                return true;
            }
            else if (f2.Valores[0] > f1.Valores[0])
            {
                return false;
            }
            else
            {
                if (f1.Valores[1] > f2.Valores[1])
                {
                    return true;
                }
                else if (f2.Valores[1] > f1.Valores[1])
                {
                    return false;
                }

                return false;
            }
        }

        public new string ToString()
        {
            string x;

            x = "[" + valores[0].ToString() + " | " + valores[1].ToString() + "]";

            return x;
        }
    }
}
