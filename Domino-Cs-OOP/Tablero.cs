using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino_Cs_OOP
{
    class Tablero
    {
        private readonly Ficha[] tablero;
        private Ficha primero;
        private Ficha ultimo;
        private int total;

        public Tablero()
        {
            tablero = new Ficha[28];

            for (int i = 0; i < 28; i++) 
            {
                tablero[i] = null;
            }

            total = 0;

            primero = null;
            ultimo = null;
        }

        public Ficha[] GetTablero
        {
            get { return tablero; }
        }

        public int Total
        {
            get { return total; }
            set { total = value; }
        }

        public Ficha Primero
        {
            get { return primero; }
            set { primero = value; }
        }

        public Ficha Ultimo
        {
            get { return ultimo; }
            set { ultimo = value; }
        }

        /*
         * 1 para al lado del primero;
         * 2 para al lado de ultimo
         */
        public void InsertarFicha(Ficha ficha, int lado)
        {
            if (primero == null)
            {
                tablero[total] = ficha;
                primero = tablero[total];
                ultimo = tablero[total];
                total++;

                return;
            }

            if (lado == 2)
            {
                tablero[total] = ficha;

                ultimo = tablero[total];

                total++;

                return;
            }
            else if (lado == 1)
            {
                for (int j = total; j > 0; j--)
                {
                    tablero[j] = tablero[j - 1];
                }

                tablero[0] = ficha;

                primero = tablero[0];

                total++;

                return;
            }
        }

        /*
         * Retorna 1 si es una ficha correcta;
         * Retorna -1 si no es una ficha correcta.
         * Lado => 1 para al lado del primero (la izquierda), 2 para al lado del ultimo (la derecha)
         */
        public bool VerificarFichaCorrecta(Ficha ficha, int lado)
        {
            if (lado == 1)
            {
                if (primero.Valores[0] == ficha.Valores[1] || primero.Valores[0] == ficha.Valores[0])
                {
                    return true;
                }

                return false;
            }
            else if (lado == 2)
            {
                if (ultimo.Valores[1] == ficha.Valores[0] || ultimo.Valores[1] == ficha.Valores[1])
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /*
         * Retorna 1 si es una ficha con el lado correcto;
         * Retorna -1 si no es una ficha con el lado correcto.
         * Lado => 1 para al lado del primero (la izquierda), 2 para al lado del ultimo (la derecha)
         */
        public bool VerificarFichaLadoCorrecto(Ficha ficha, int lado)
        {
            if (lado == 1)
            {
                if (primero.Valores[0] == ficha.Valores[1])
                {
                    return true;
                }

                return false;
            }
            else if (lado == 2)
            {
                if (ultimo.Valores[1] == ficha.Valores[0])
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public new string ToString()
        {
            string x = "\t";

            for (int i = 0; i < total; i++)
            {
                if (i == 0)
                {
                    if (i == total - 1)
                    {
                        x += " <-P" + tablero[i].ToString() + "U-> ";
                    }
                    else
                    {
                        x += " <-P" + tablero[i].ToString() + " ";
                    }
                }
                else if (i == total - 1)
                {
                    x += " " + tablero[i].ToString() + "U-> ";
                }
                else
                {
                    x += " " + tablero[i].ToString() + " ";
                }
            }

            x += "\n \n";

            return x;
        }
    }
}
