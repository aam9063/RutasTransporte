using System;
using System.Collections.Generic;
using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;

class Program
{
    const int MAX_RUTAS = 4;
    const int MAX_TRANSPORTES = 15;

    static void Main()
    {
        // Inicialización del modelo

        Rutas[] rutas = new Rutas[4];
        rutas[0] = new Rutas("A", "Alicante - Valencia");
        rutas[1] = new Rutas("B", "Alicante - Madrid");
        rutas[2] = new Rutas("C", "Alicante - Barcelona");
        rutas[3] = new Rutas("D", "Alicante - Andorra");

        Transporte[] transportes = new Transporte[15];
        transportes[0] = new Avion("Ryan", 20);
        transportes[1] = new Avion("Iberia", 16);
        transportes[2] = new Autobus("Alsa", 12);
        transportes[3] = new Tren("Renfe", 13);
        transportes[4] = new Tren("Avro", 15);
        transportes[5] = new Tren("Ouigo", 16);
        transportes[6] = new Tren("Iryo", 17);
        transportes[7] = new Avion("Ryan", 12);
        transportes[8] = new Avion("Iberia", 11);
        transportes[9] = new Autobus("Alsa", 10);
        transportes[10] = new Tren("Renfe", 9);
        transportes[11] = new Tren("Avro", 8);
        transportes[12] = new Tren("Ouigo", 6);
        transportes[13] = new Tren("Iryo", 5);
        transportes[14] = new Tren("Iryo", 23);

        // Asignar transportes a rutas
        rutas[0].AddAvion((Avion)transportes[0]);
        rutas[0].AddAvion((Avion)transportes[1]);
        rutas[0].AddAutobus((Autobus)transportes[2]);
        rutas[0].AddTren((Tren)transportes[3]);
        rutas[0].AddTren((Tren)transportes[4]);
        rutas[0].AddTren((Tren)transportes[5]);
        rutas[0].AddTren((Tren)transportes[6]);
        rutas[1].AddAvion((Avion)transportes[7]);
        rutas[1].AddAvion((Avion)transportes[8]);
        rutas[1].AddAutobus((Autobus)transportes[9]);
        rutas[1].AddTren((Tren)transportes[10]);
        rutas[1].AddTren((Tren)transportes[11]);
        rutas[2].AddTren((Tren)transportes[12]);
        rutas[1].AddTren((Tren)transportes[13]);
        rutas[2].AddTren((Tren)transportes[14]);

        bool flag = true;
        do
        {
            Console.WriteLine(".......................");
            Console.WriteLine("       Menú");
            Console.WriteLine(".......................");
            Console.WriteLine("1. Listar las rutas");
            Console.WriteLine("2. Información de una ruta");
            Console.WriteLine("3. Listar rutas ordenadas");
            Console.WriteLine("4. Información extendida");
            Console.WriteLine("5. Listar rutas y transportes");
            Console.WriteLine("6. Reservar avión asientos");
            Console.WriteLine("7. reservar tren vagón");
            Console.WriteLine("8. Exit");

            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());
            string respuesta = "";
            switch (opcion)
            {
                case 1:
                    ListarRutas(rutas);
                    break;
                case 2:
                    InformacionRuta(rutas);
                    break;
                case 3:
                    ListarRutasOrdenadas(rutas);
                    break;
                case 4:
                    InformacionExtendida(rutas);
                    break;
                case 5:
                    ListarRutasYTransporte(rutas);
                    break;
                case 6:
                    ReservarAvionAsientos(rutas);
                    break;
                case 7:
                    ReservarTrenVagon(rutas);
                    break;

                case 8:
                    Console.WriteLine("Salir del programa");
                    flag = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }
        } while (flag == true);

        static void ListarRutas(Rutas[] rutas)
        {
            foreach (var ruta in rutas)
            {
                if (ruta != null)
                {
                    Console.WriteLine($"Nombre: {ruta.GetNombre()}, Ruta: {ruta.GetDestinos()}, NumTransportes: {ruta.GetNumTransportes()}");
                }
            }
        }

        static void InformacionRuta(Rutas[] rutas)
        {
            Console.Write("Nombre de la Ruta: ");
            string nombreRuta = Console.ReadLine().ToUpper();

            foreach (var ruta in rutas)
            {
                if (ruta != null && ruta.GetNombre().ToUpper() == nombreRuta)
                {
                    Console.WriteLine($"Nombre: {ruta.GetNombre()}, Ruta: {ruta.GetDestinos()}, NumTransportes: {ruta.GetNumTransportes()}");
                    return;
                }
            }

            Console.WriteLine("Ruta no encontrada.");
        }

        static void ListarRutasOrdenadas(Rutas[] rutas)
        {
            Array.Sort(rutas, new RutasComparar());
            foreach (var ruta in rutas)
            {
                if (ruta != null)
                {
                    Console.WriteLine($"Nombre: {ruta.GetNombre()}, Ruta: {ruta.GetDestinos()}, NumTransportes: {ruta.GetNumTransportes()}");
                }
            }
        }

        static void InformacionExtendida(Rutas[] rutas)
        {
            Console.Write("Nombre de la Ruta: ");
            string nombreRutaExtendida = Console.ReadLine().ToUpper();

            foreach (var ruta in rutas)
            {
                if (ruta != null && ruta.GetNombre().ToUpper() == nombreRutaExtendida)
                {
                    Console.WriteLine($"Tenemos {ruta.GetNumTransportes()} transportes, Destinos: {ruta.GetDestinos()}");
                    ruta.Show();
                    return;
                }
            }

            Console.WriteLine("Ruta no encontrada.");
        }

        static void ListarRutasYTransporte(Rutas[] rutas)
        {
            foreach (var ruta in rutas)
            {
                if (ruta != null)
                {
                    Console.WriteLine($"Nombre: {ruta.GetNombre()}, Ruta: {ruta.GetDestinos()}, NumTransportes: {ruta.GetNumTransportes()}");
                    ruta.Show();
                }
            }
        }

        static void ReservarAvionAsientos(Rutas[] rutas)
        {
            Console.Write("Introduzca la ruta: ");
            string rutaNombre = Console.ReadLine().ToUpper();

            bool rutaEncontrada = false;
            foreach (var ruta in rutas)
            {
                if (ruta != null && ruta.GetNombre().ToUpper() == rutaNombre)
                {
                    rutaEncontrada = true;
                    foreach (var avion in ruta.GetAviones())
                    {
                        if (avion != null)
                        {
                            avion.SetNumAsientos(avion.GetNumAsientos() + 1);
                            Console.WriteLine($"Avión actualizado: {avion.GetEmpresa()} - Asientos: {avion.GetNumAsientos()}");
                            ruta.Show();
                        }
                    }
                    break;
                }
            }

            if (!rutaEncontrada)
            {
                Console.WriteLine("Ruta no encontrada.");
            }
        }

        static void ReservarTrenVagon(Rutas[] rutas)
        {
            Console.Write("Introduzca la ruta: ");
            string rutaNombre = Console.ReadLine().ToUpper();

            bool rutaEncontrada = false;
            foreach (var ruta in rutas)
            {
                if (ruta != null && ruta.GetNombre().ToUpper() == rutaNombre)
                {
                    rutaEncontrada = true;
                    foreach (var tren in ruta.GetTrenes())
                    {
                        if (tren != null)
                        {
                            tren.SetNumVagones(tren.GetNumVagones() + 1);
                            Console.WriteLine($"Tren actualizado: {tren.GetEmpresa()} - Vagones: {tren.GetNumVagones()}");
                        }
                    }
                    break;
                }
            }

            if (!rutaEncontrada)
            {
                Console.WriteLine("Ruta no encontrada.");
            }
        }


    }
}

public interface IMedios
{
    public void Show();

}

public class RutasComparar : IComparer<Rutas>
{
    public int Compare(Rutas x, Rutas y)
    {
        return - 1 * string.Compare(x.GetDestinos(), y.GetDestinos()); //Para ordenar de forma descendente
    }
}

public class PreciosComparar : IComparer<Transporte>
{
    public int Compare(Transporte x, Transporte y)
    {
        return x.GetPrecio().CompareTo(y.GetPrecio());
    }
}


public class Rutas : IMedios
{
    private string Nombre;
    private string Destinos;
    private int numAviones;
    private int numTrenes;
    private int numAutobuses;
    public int MAX_TRANSPORTES;
    private Avion[] Avion;
    private Tren[] Tren;
    private Autobus[] AutoBus;

    public Rutas(string nombre, string destinos)
    {
        this.Nombre = nombre;
        this.Destinos = destinos;
        this.MAX_TRANSPORTES = 15;
        this.Avion = new Avion[MAX_TRANSPORTES];
        this.Tren = new Tren[MAX_TRANSPORTES];
        this.AutoBus = new Autobus[MAX_TRANSPORTES];
    }

    public string GetNombre()
    {
        return this.Nombre;
    }

    public void SetNombre(string nombre)
    {
        this.Nombre = nombre;
    }

    public string GetDestinos()
    {
        return this.Destinos;
    }

    public void SetDestinos(string destinos)
    {
        this.Destinos = destinos;
    }

    public int GetNumAviones()
    {
        return this.numAviones;
    }

    public void SetNumAviones(int numAviones)
    {
        this.numAviones = numAviones;
    }

    public int GetNumTrenes()
    {
        return this.numTrenes;
    }

    public void SetNumTrenes(int numTrenes)
    {
        this.numTrenes = numTrenes;
    }

    public Avion[] GetAviones()
    {
        return this.Avion;
    }

    public Tren[] GetTrenes()
    {
        return this.Tren;
    }

    public Autobus[] GetAutobuses()
    {
        return this.AutoBus;
    }

    public void AddAvion(Avion avion)
    {
        if (numAviones < Avion.Length)
        {
            this.Avion[numAviones] = avion;
            numAviones++;
        }
    }
    public void AddTren(Tren tren)
    {
        if (numTrenes < Tren.Length)
        {
            this.Tren[numTrenes] = tren;
            numTrenes++;
        }
    }
    public void AddAutobus(Autobus autobus)
    {
        if (numAutobuses < AutoBus.Length)
        {
            this.AutoBus[numAutobuses] = autobus;
            numAutobuses++;
        }
    }

    public int GetNumTransportes()
    {
        return this.numAviones + this.numTrenes + this.numAutobuses;
    }

    public void ReservarVagonTren()
    {
        foreach (var tren in Tren)
        {
            if (tren != null)
            {
                tren.SetNumVagones(tren.GetNumVagones() + 1);
            }
        }
    }

    // Métodos para manejar la ruta
    public virtual void Show()
    {
        Console.WriteLine($"Nombre:\t{GetNombre()}\tRuta:\t{GetDestinos()}\tNumTransportes:\t{GetNumTransportes()}");
        foreach (var avion in Avion)
        {
            if (avion != null)
            {
                Console.WriteLine(avion.ToString());
            }
        }
        foreach (var tren in Tren)
        {
            if (tren != null)
            {
                Console.WriteLine(tren.ToString());
            }
        }
        foreach (var autobus in AutoBus)
        {
            if (autobus != null)
            {
                Console.WriteLine(autobus.ToString());
            }
        }
    }
}


public abstract class Transporte
{
    private string Empresa;
    private int Precio;

    // Métodos y propiedades comunes
    public string GetEmpresa()
    {
        return Empresa;
    }

    public void SetEmpresa(string empresa)
    {
        Empresa = empresa;
    }

    public int GetPrecio()
    {
        return Precio;
    }

    public void SetPrecio(int precio)
    {
        Precio = precio;
    }

    public Transporte(string Empresa, int Precio)
    {
        this.Empresa = Empresa;
        this.Precio = Precio;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Empresa: {GetEmpresa}, Precio: {GetPrecio}");
    }
}

public class Tren : Transporte
{
    // Atributos y métodos específicos de Tren
    private int numVagones;

    public Tren(string empresa, int precio) : base(empresa, precio)
    {
    }

    public int GetNumVagones()
    {
        return this.numVagones;
    }

    public int SetNumVagones(int numVagones)
    {
        return this.numVagones = numVagones;
    }

    public override void Show()
    {
        Console.WriteLine($"Número de Vagones: {GetNumVagones()}");
    }

    public override string ToString()
    {
        return $"Tren - Empresa: {GetEmpresa()}, Precio: {GetPrecio()}, Número de Vagones: {GetNumVagones()}";
    }
}

public class Avion : Transporte
{
    // Atributos y métodos específicos de Avión

    private int numAsientos;

    public int GetNumAsientos()
    {
        return this.numAsientos;
    }

    public void SetNumAsientos(int numAsientos)
    {
        this.numAsientos = numAsientos;
    }

    public Avion(string empresa, int precio) : base(empresa, precio)
    {
    }
    public override void Show()
    {
        Console.WriteLine($"Número de Asientos: {GetNumAsientos()}");
    }

    public override string ToString()
    {
        return $"Avion - Empresa: {GetEmpresa()}, Precio: {GetPrecio()}, Número de Asientos: {GetNumAsientos()}";
    }
}

public class Autobus : Transporte
{
    // Atributos y métodos específicos de Autobús
    private int numAsientos;
    public Autobus(string empresa, int precio) : base(empresa, precio)
    {
    }

    public int GetNumAsientos()
    {
        return this.numAsientos;
    }

    public void SetNumAsientos(int numAsientos)
    {
        this.numAsientos = numAsientos;
    }

    public override void Show()
    {
    }

    public override string ToString()
    {
        return $"Autobus - Empresa: {GetEmpresa()}, Precio: {GetPrecio()}, Número de Asientos: {GetNumAsientos()}";
    }
}





