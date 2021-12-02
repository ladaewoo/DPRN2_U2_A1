using System;

namespace Act1U2
{
    abstract class Inventario
    {
        protected int existenciaMinima = 0;
        protected int existenciaMaxima = 0;
        protected int articulosComprados = 0;
        protected int articulosVendidos = 0;
        protected double precioUnitario = 9.99;

        abstract public void comprar(int cantidad);
        abstract public void vender(int cantidad);

        public void imprimirExistencias()
        {
            if(articulosVendidos >= articulosComprados)
            {
                Console.WriteLine("Sin existencias en el inventario.");
                Console.WriteLine();
            }
            else if (articulosComprados >= existenciaMaxima)
            {
                Console.WriteLine("Almacenés llenos.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Hay {0} artículos en el inventario.", articulosComprados - articulosVendidos);
                Console.WriteLine();
            }
        }

        public void imprimirVentas()
        {
            Console.WriteLine("Se han vendido ${0} por la venta de {1} artículos.", articulosVendidos * precioUnitario, articulosVendidos);
            Console.WriteLine();
        }

        public Inventario(int minima, int maxima)
        {
            this.existenciaMinima = minima;
            this.existenciaMaxima = maxima;
        }
    }

    sealed class Articulos : Inventario 
    {
        private string nombre;
        private string presentacion;
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Presentacion
        {
            get { return presentacion.ToUpper(); }
            set { presentacion = value; }
        }

        public override void comprar(int cantidad)
        {
            articulosComprados += cantidad;
        }

        public override void vender(int cantidad)
        {
            articulosVendidos += cantidad;
            imprimirVentas();
        }

        public Boolean puedeComprar(int cantidad)
        {
            return (articulosComprados + cantidad <= existenciaMaxima);
        }

        public Boolean puedeVender(int cantidad)
        {
            return (articulosVendidos + cantidad <= articulosComprados);
        }
        public Articulos(int minima, int maxima) : base(minima, maxima)
        {
            
        }
    }


    class Program
    {
        private static int opcion;
        private static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("Hola, por favor selecciona la opción deseada.");
            Console.WriteLine();
            Console.WriteLine("1. Comprar artículos.");
            Console.WriteLine("2. Vender artículos.");
            Console.WriteLine("3. Ver existencias.");
            Console.WriteLine("4. Ver ventas.");
            Console.WriteLine("5. Salir.");
            Console.WriteLine();
            opcion = Convert.ToInt32(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            
            Menu();

            Articulos articulos = new Articulos(10, 20);

            articulos.Nombre = "Fertilizante orgánico";
            articulos.Presentacion = "Costal de 25kg";

            while (opcion < 6)
            {
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("¿Cuántos artículos deseas comprar?");
                        int cantidad = Convert.ToInt32(Console.ReadLine());
                        
                        if(articulos.puedeComprar(cantidad)) {
                            articulos.comprar(cantidad);
                        } else {
                            Console.WriteLine("No se puede comprar.");
                        } 

                        break;
                    case 2:
                        Console.WriteLine("¿Cuántos artículos deseas vender?");
                        cantidad = Convert.ToInt32(Console.ReadLine());
                        
                        if (articulos.puedeVender(cantidad)) {
                            articulos.vender(cantidad);
                        }
                        else
                        {
                            Console.WriteLine("No hay suficientes artículos en el inventario.");
                        }
                        
                        break;
                    case 3:
                        articulos.imprimirExistencias();
                        break;
                    case 4:
                        articulos.imprimirVentas();
                        break;
                    default:
                        break;
                }

                opcion = 0;
                Menu();
            }

            Console.ReadKey();
        }
    }
}
