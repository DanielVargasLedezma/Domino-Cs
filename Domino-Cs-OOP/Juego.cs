using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Domino_Cs_OOP
{
    class Juego
    {
        private readonly Ficha[] fichas;
        private readonly Jugador[] jugadores;
        private readonly Tablero table;

        private readonly Random _random;

        public Juego()
        {
            _random = new Random();

            fichas = new Ficha[28];

            for(int i = 0; i < 28; i++)
            {
                fichas[i] = new Ficha();
            }

            jugadores = new Jugador[2];

            for(int i = 0; i < 2; i++)
            {
                jugadores[i] = new Jugador();
            }

            table = new Tablero();
        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public void IniciarJuego()
        {
            IniciarFichasRevueltas();

            RepartirFichas();

            Menu();
        }

        private void IniciarFichasRevueltas()
        {
            int[] posiciones_seteadas = new int[28];
            int random;

            for (int i = 0; i < 28; i++)
            {
                posiciones_seteadas[i] = 0;
            }

            int contador = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    random = RandomNumber(0, 28);

                    while (posiciones_seteadas[random] == 1)
                    {
                        random = RandomNumber(0, 28);
                    }

                    fichas[random].Valores[0] = i;
                    fichas[random].Valores[1] = j;

                    fichas[random].Estado = 1;

                    posiciones_seteadas[random] = 1;

                    contador++;

                    if (contador == 28)
                    {
                        break;
                    }
                }
            }
        }

        private void ComerDelPozo(Jugador jugador)
        {
            int random;

            random = RandomNumber(0, 28);

            while (fichas[random].Estado != 1)
            {
                random = RandomNumber(0, 28);
            }

            jugador.InsertarFicha(fichas[random]);
        }

        /*
         * Retorna 1 si esta vacio;
         * Retrona -1 si no lo esta
         */
        private bool CheckIfPozoVacio()
        {
            int contador = 0;

            for (int i = 0; i < 28; i++)
            {
                if (fichas[i].Estado == 1)
                {
                    contador++;
                }

                if (contador != 0)
                    return false;
            }

            if (contador == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void RepartirFichas()
        {
            int[] posiciones_seteadas = new int[28];
            int random;

            for (int i = 0; i < 28; i++)
            {
                posiciones_seteadas[i] = 0;
            }

            for (int i = 0; i < 7; i++)
            {
                random = RandomNumber(0, 28);

                while (posiciones_seteadas[random] == 1)
                {
                    random = RandomNumber(0, 28);
                }

                //printf("%i\n", random);

                jugadores[0].InsertarFicha(fichas[random]);

                posiciones_seteadas[random] = 1;
            }

            //printf("\n");

            for (int i = 0; i < 7; i++)
            {
                random = RandomNumber(0, 28);

                while (posiciones_seteadas[random] == 1)
                {
                    random = RandomNumber(0, 28);
                }

                //printf("%i\n", random);

                jugadores[1].InsertarFicha(fichas[random]);

                posiciones_seteadas[random] = 1;
            }
        }

        /*
         * Retorna 1 si hay fichas jugables;
         * Retrona -1 si no.
         */
        private bool VerificarSiHayFichasJugables(Jugador jugador)
        {
            int contador = 0;

            for (int i = 0; i < jugador.Numero_Fichas; i++)
            {
                if (table.Primero.Valores[0] == jugador.Fichas[i].Valores[0] || table.Primero.Valores[0] == jugador.Fichas[i].Valores[1])
                {
                    contador++;
                    if (contador > 0)
                        return true;
                }
                else if (table.Ultimo.Valores[1] == jugador.Fichas[i].Valores[0] || table.Ultimo.Valores[1] == jugador.Fichas[i].Valores[1])
                {
                    contador++;
                    if (contador > 0)
                        return true;
                }
            }

            if (contador == 0)
                return false;
            else
                return true;
        }

        private void Menu()
        {
            int jugadorActivo;

            bool resultado;

            bool fichaPar = false;

            Ficha fichaAltaJugador1 = jugadores[0].GetDobleAlta();
            Ficha fichaAltaJugador2 = jugadores[1].GetDobleAlta();

            if (fichaAltaJugador1 != null && fichaAltaJugador2 == null)
            {
                Console.WriteLine("Ficha mas alta del jugador 1: {0}\n", fichaAltaJugador1.ToString());
                Console.WriteLine("El jugador 2 no tiene una ficha doble y por ende no tiene nada que comparar\n");

                resultado = true;

                fichaPar = true;
            }
            else if (fichaAltaJugador1 != null && fichaAltaJugador2 == null)
            {
                Console.WriteLine("El jugador 1 no tiene una ficha doble y por ende no tiene nada que comparar\n");
                Console.WriteLine("Ficha mas alta del jugador 2: {0}\n", fichaAltaJugador2.ToString());

                resultado = false;

                fichaPar = true;
            }
            else if (fichaAltaJugador1 != null && fichaAltaJugador2 != null)
            {
                Console.WriteLine("Ficha mas alta del jugador 1: {0}\n", fichaAltaJugador1.ToString());

                Console.WriteLine("Ficha mas alta del jugador 2: {0}\n", fichaAltaJugador2.ToString());

                resultado = Ficha.FichaMasAltaEntre(fichaAltaJugador1, fichaAltaJugador2);

                fichaPar = true;
            }
            else
            {
                Console.WriteLine("Ningun jugador tiene una ficha doble, buscando la ficha normal mas alta entre los jugadores...\n");

                fichaAltaJugador1 = jugadores[0].GetFichaAlta();
                fichaAltaJugador2 = jugadores[1].GetFichaAlta();

                Console.WriteLine("Ficha mas alta del jugador 1: {0}\n", fichaAltaJugador1.ToString());

                Console.WriteLine("Ficha mas alta del jugador 2: {0}\n", fichaAltaJugador2.ToString());

                resultado = Ficha.FichaMasAltaEntre(fichaAltaJugador1, fichaAltaJugador2);
            }

            Console.Write("Determinando el ganador");
            Console.Out.Flush();
            Thread.Sleep(1000);
            Console.Write(".");
            Console.Out.Flush(); // Force the output to be printed
            Thread.Sleep(1000);
            Console.Write(".");
            Console.Out.Flush(); // Force the output to be printed
            Thread.Sleep(1000);
            Console.Write(".\n\n");

            if (resultado)
            {
                jugadorActivo = 0;
                Console.WriteLine("El ganador es el jugador 1, este empieza el juego y pone su ficha alta para empezar\n");
                Console.Out.Flush();
                Thread.Sleep(3000);

                if (fichaPar)
                {
                    Ficha fichaAJugar = jugadores[jugadorActivo].GetAndPopDobleAlta();
                    fichaAJugar.Estado = 3;
                    table.InsertarFicha(fichaAJugar, 0);
                }
                else
                {
                    Ficha fichaAJugar = jugadores[jugadorActivo].GetAndPopFichaAlta();
                    fichaAJugar.Estado = 3;
                    table.InsertarFicha(fichaAJugar, 0);
                }
            }
            else if (!resultado)
            {
                jugadorActivo = 1;
                Console.WriteLine("El ganador es el jugador 2, este empieza el juego y pone su ficha alta para empezar\n");
                Console.Out.Flush();
                Thread.Sleep(3000);

                if (fichaPar)
                {
                    Ficha fichaAJugar = jugadores[jugadorActivo].GetAndPopDobleAlta();
                    fichaAJugar.Estado = 3;
                    table.InsertarFicha(fichaAJugar, 0);
                }
                else
                {
                    Ficha fichaAJugar = jugadores[jugadorActivo].GetAndPopFichaAlta();
                    fichaAJugar.Estado = 3;
                    table.InsertarFicha(fichaAJugar, 0);
                }
            }
            else
            {
                return;
            }

            while (!PedirAccion(jugadores[jugadorActivo], jugadorActivo))
            {
                if (jugadorActivo == 1)
                {
                    jugadorActivo = 0;
                }
                else
                {
                    jugadorActivo = 1;
                }
            }

            Console.WriteLine("\n\nEl ganador es el jugador {0}!!!\n", (jugadorActivo + 1).ToString());
            Console.Write("Saliendo en 3 ");
            Console.Out.Flush();
            Thread.Sleep(1000);
            Console.Write("2 ");
            Console.Out.Flush();
            Thread.Sleep(1000);
            Console.Write("1");
        }

        private bool PedirAccion(Jugador jugador, int numeroJugador)
        {
            string line;

            Console.Clear();

            Console.WriteLine("Jugador n°{0}\n", (numeroJugador + 1).ToString());

            Console.WriteLine("El tablero es: \n");

            Console.Write("{0}", table.ToString());

            Console.Write("{0}", jugador.ToString());

            int opcion = 0, invertir = 0, lado = 0;

            if (VerificarSiHayFichasJugables(jugador))
            {
                //Elegir de las fichas que tiene
                Console.WriteLine("Digite cual ficha es la que desea jugar.");
                Console.WriteLine("Recuerde que la ficha debe tener el valor en el lateral opuesto de donde desea conectarla y poseer el mismo valor.\n");
                Console.WriteLine("Si no tiene el valor en el lateral correcto puede rotar la ficha, para ello digite el valor '404'.");
                Console.WriteLine("Rango valido (0 - {0}) o 404 para invertir\n", (jugador.Numero_Fichas - 1).ToString());
                Console.Write("-> ");
                line = Console.ReadLine();

                opcion = int.Parse(line);

                if (opcion == 404)
                {
                    Console.WriteLine("Digite cual ficha es la que desea invertir.");
                    Console.WriteLine("Rango valido (0 - {0})\n", (jugador.Numero_Fichas - 1).ToString());
                    Console.Write("-> ");
                    line = Console.ReadLine();

                    invertir = int.Parse(line);

                    if (invertir > jugador.Numero_Fichas || invertir < 0)
                    {
                        Console.WriteLine("Se ha digitado una ficha que no esta en el rango contemplado, volviendo a la eleccion de ficha...");
                        Console.Out.Flush();
                        Thread.Sleep(3000);

                        return PedirAccion(jugador, numeroJugador);
                    }

                    jugador.DarVueltaAFicha(invertir);

                    return PedirAccion(jugador, numeroJugador);
                }
                else
                {
                    if (opcion < 0 || opcion > jugador.Numero_Fichas)
                    {
                        Console.WriteLine("Se ha digitado una ficha que no esta en el rango contemplado, volviendo a la eleccion de ficha...");
                        Console.Out.Flush();
                        Thread.Sleep(3000);

                        return PedirAccion(jugador, numeroJugador);
                    }
                    else
                    {
                        Console.WriteLine("Digite el lado por el cual desea poner la ficha.\n");
                        Console.WriteLine("\t1) Al lado del primero (la izquierda de este)");
                        Console.WriteLine("\t2) Al lado del ultimo (la derecha de este)\n");
                        Console.Write("-> ");
                        line = Console.ReadLine();

                        lado = int.Parse(line);

                        if (lado < 0 || lado > 2)
                        {
                            Console.WriteLine("Se ha digitado un lado que no esta en el rango contemplado, volviendo a la eleccion de ficha...");
                            Console.Out.Flush();
                            Thread.Sleep(3000);

                            return PedirAccion(jugador, numeroJugador);
                        }

                        if (!table.VerificarFichaCorrecta(jugador.GetFichaEnNumero(opcion), lado))
                        {
                            Console.WriteLine("Se ha digitado una ficha que no posee ningun valor lateral que pueda conectar con el lado que se selecciono, volviendo a la eleccion de ficha...");
                            Console.Out.Flush();
                            Thread.Sleep(3000);

                            return PedirAccion(jugador, numeroJugador);
                        }

                        if (!table.VerificarFichaLadoCorrecto(jugador.GetFichaEnNumero(opcion), lado))
                        {
                            Console.WriteLine("Se ha digitado una ficha que no posee el valor en el lateral que se puede conectar con el lado que se selecciono, volviendo a la eleccion de ficha...");
                            Console.Out.Flush();
                            Thread.Sleep(3000);

                            return PedirAccion(jugador, numeroJugador);
                        }

                        Ficha fichaAInsertar = jugador.GetFichaAndPopEnNumero(opcion);

                        fichaAInsertar.Estado = 3;

                        table.InsertarFicha(fichaAInsertar, lado);
                    }
                }
            }
            else
            {
                //Comer del pozo
                if (!CheckIfPozoVacio())
                {
                    Console.WriteLine("Usted no tiene fichas para poner. Por eso debe comer del pozo.");
                    Console.WriteLine("Digite cualquier caracter para comer del pozo.\n");
                    Console.Write("-> ");
                    line = Console.ReadLine();

                    if (line.Length != 0)
                    {
                        ComerDelPozo(jugador);
                    }

                    return PedirAccion(jugador, numeroJugador);
                }
                else
                {
                    Console.WriteLine("Usted no tiene fichas para poner. Por eso debe comer del pozo, pero el pozo se encuentra vacio....");
                    Console.WriteLine("Por eso se saltara su turno.\n");

                    Console.Out.Flush();
                    Thread.Sleep(3000);

                    return false;
                }
            }

            if (jugador.Numero_Fichas == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
