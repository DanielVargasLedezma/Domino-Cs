using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino_Cs_OOP
{
    class Jugador
    {
        private readonly Ficha[] fichas;

        private int numero_Fichas;

        public Jugador()
        {
            fichas = new Ficha[28];

            for (int i = 0; i < 28; i++)
            {
                fichas[i] = null;
            }

            numero_Fichas = 0;
        }

        public Ficha[] Fichas
        {
            get { return fichas; }
        }

        public int Numero_Fichas
        {
            get { return numero_Fichas; }
            set { numero_Fichas = value; }
        }

        public void InsertarFicha(Ficha ficha)
        {
            ficha.Estado = 2;

            fichas[numero_Fichas] = ficha;

            Numero_Fichas++;
        }

        public Ficha GetFichaAlta()
        {
            Ficha fichaAlta = null;

            int[] valoresAlta = new int[2];

            valoresAlta[0] = -1;
            valoresAlta[1] = -1;

            for (int i = 0; i < numero_Fichas; i++)
            {
                if (fichas[i].Valores[0] > valoresAlta[0])
                {
                    valoresAlta[0] = fichas[i].Valores[0];
                    valoresAlta[1] = fichas[i].Valores[1];
                    fichaAlta = fichas[i];
                }
                else if (fichas[i].Valores[0] == valoresAlta[0])
                {
                    if (fichas[i].Valores[1] > valoresAlta[1])
                    {
                        valoresAlta[0] = fichas[i].Valores[0];
                        valoresAlta[1] = fichas[i].Valores[1];
                        fichaAlta = fichas[i];
                    }
                }
            }

            return fichaAlta;
        }

        public Ficha GetAndPopFichaAlta()
        {
            Ficha fichaAlta = null;

            int[] valoresAlta = new int[2];

            valoresAlta[0] = -1;
            valoresAlta[1] = -1;

            int posFichaAlta = -1;

            for (int i = 0; i < numero_Fichas; i++)
            {
                if (fichas[i].Valores[0] > valoresAlta[0])
                {
                    valoresAlta[0] = fichas[i].Valores[0];
                    valoresAlta[1] = fichas[i].Valores[1];
                    fichaAlta = fichas[i];

                    posFichaAlta = i;
                }
                else if (fichas[i].Valores[0] == valoresAlta[0])
                {
                    if (fichas[i].Valores[1] > valoresAlta[1])
                    {
                        valoresAlta[0] = fichas[i].Valores[0];
                        valoresAlta[1] = fichas[i].Valores[1];
                        fichaAlta = fichas[i];

                        posFichaAlta = i;
                    }
                }
            }

            fichas[posFichaAlta] = null;

            ShiftAllToLeft();

            numero_Fichas--;

            return fichaAlta;
        }

        public Ficha GetDobleAlta()
        {
            Ficha fichaAlta = null;

            int valoresDobleAlta = -1;

            for (int i = 0; i < numero_Fichas; i++)
            {
                if (fichas[i].Valores[0] == fichas[i].Valores[1])
                {
                    if (fichas[i].Valores[0] > valoresDobleAlta)
                    {
                        valoresDobleAlta = fichas[i].Valores[0];
                        fichaAlta = fichas[i];
                    }
                }
            }

            return fichaAlta;
        }

        public Ficha GetAndPopDobleAlta()
        {
            Ficha fichaAlta = null;

            int valorDobleAlta = -1;

            int posFichaAlta = -1;

            for (int i = 0; i < numero_Fichas; i++)
            {
                if (fichas[i].Valores[0] == fichas[i].Valores[1])
                {
                    if (fichas[i].Valores[0] > valorDobleAlta)
                    {
                        valorDobleAlta = fichas[i].Valores[0];
                        fichaAlta = fichas[i];

                        posFichaAlta = i;
                    }
                }
            }

            fichas[posFichaAlta] = null;

            ShiftAllToLeft();

            numero_Fichas--;

            return fichaAlta;
        }

        public Ficha GetFichaEnNumero(int numero)
        {
            return fichas[numero];
        }

        public Ficha GetFichaAndPopEnNumero(int numero)
        {
            Ficha ficha = fichas[numero];

            fichas[numero] = null;

            ShiftAllToLeft();

            numero_Fichas--;

            return ficha;
        }

        public void ShiftAllToLeft()
        {
            for (int i = 0; i < numero_Fichas; i++)
            {
                if (fichas[i] == null)
                {
                    fichas[i] = fichas[i + 1];
                    fichas[i + 1] = null;
                }
            }
        }

        public void DarVueltaAFicha(int numero_ficha)
        {
            int auxiliar;
            auxiliar = fichas[numero_ficha].Valores[0];
            fichas[numero_ficha].Valores[0] = fichas[numero_ficha].Valores[1];
            fichas[numero_ficha].Valores[1] = auxiliar;
        }

        public new string ToString()
        {
            string x;

            x = "Sus fichas: \n\n\t";

            for (int i = 0; i < numero_Fichas; i++)
            {
                x += "(" + i + ") : " + fichas[i].ToString() + "]\t";
            }

            x += "\n\n";

            return x; 
        }
    }
}
