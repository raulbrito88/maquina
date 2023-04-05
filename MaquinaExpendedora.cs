using System;
using System.Collections.Generic;

// Clase que representa la máquina expendedora
public class MaquinaExpendedora
{
    private List<IConsumable> consumibles = new List<IConsumable>();
    private bool salir;



// Método para mostrar el menú

    public void MostrarMenu()
    {
        do
        {
            Console.WriteLine("****¡BIENVENIDOS A SU MÁQUINA DISPENSADORA!****\n");
            Console.WriteLine("SELECCIONE UN MODO DE USUARIO:\n");
            Console.WriteLine("1. Modo proveedor");
            Console.WriteLine("2. Modo cliente");
            Console.WriteLine("3. Salir\n");

            int opcion = 0;
            bool valido = false;
            do
            {
                Console.Write("Ingrese su opción: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese 1, 2 o 3.\n");
                    continue;
                }
                valido = int.TryParse(input, out opcion);
                if (!valido || opcion < 1 || opcion > 3)
                {
                    Console.WriteLine("Opción inválida. Por favor, ingrese 1, 2 o 3.\n");
                }
            } while (!valido || opcion < 1 || opcion > 3);

            switch (opcion)
            {
                case 1:
                    ModoProveedor();
                    break;
                case 2:
                    ModoCliente();
                    break;
                case 3:
                    salir = true;
                    break;
            }
        } while (!salir); 
    }

// Método para manejar el modo "proveedor"

    public bool ModoProveedor()
    {
        Console.WriteLine("\n---MODO PROVEEDOR---\n");
        do
        {
            Console.WriteLine("SELECCIONE UNA OPCIÓN:");
            Console.WriteLine("1. Agregar nuevo producto");
            Console.WriteLine("2. Rellenar inventario de un producto");
            Console.WriteLine("3. Regresar al menú principal\n");
            Console.Write("Ingrese el número de opción: ");
            int opcion;
            if (int.TryParse(Console.ReadLine(), out opcion))
            Console.WriteLine();

            if (opcion == 1)
            {
                Console.Write("Ingrese el nombre del producto: ");
                string nombre = Console.ReadLine() ?? "";
                Console.Write("Ingrese el precio del producto: $");
                decimal precio = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Ingrese la cantidad inicial del producto: ");
                int cantidad = Convert.ToInt32(Console.ReadLine());

               var consumible = new Consumable(nombre, precio, cantidad);
                AgregarConsumible(consumible);
                Console.WriteLine($"\n¡EL PRODUCTO '{nombre}' HA SIDO AGREGADO A LA LISTA!");
                Console.WriteLine();
            }
            else if (opcion == 2)
            {
                Console.Write("Ingrese el nombre del producto: ");
                string nombre = Console.ReadLine() ?? "";
                Console.Write("Ingrese la cantidad a rellenar: ");
                int cantidad = Convert.ToInt32(Console.ReadLine());

                RellenarInventario(nombre, cantidad);
                Console.WriteLine();
            }
            else if (opcion == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("\n¡OPCIÓN INVÁLIDA!. Por favor, seleccione la opción 1, 2 o 3.\n");
                Console.WriteLine();
            }
        }while (true);
        return true;
    }

// Método para agregar nuevos productos a la lista de consumibles

    public void AgregarConsumible(IConsumable consumible)
    {
        consumibles.Add(consumible);
    }

// Método para rellenar el inventario de un producto existente

    public void RellenarInventario(string nombre, int cantidad)
    {
        var consumible = consumibles.Find(c => c.Nombre == nombre);
        if (consumible != null)
        {
            consumible.Cantidad += cantidad;
            Console.WriteLine($"\n¡INVENTARIO DE '{nombre}' RELLENADO CON {cantidad} UNIDADES!.");
            Console.WriteLine($"\n¡AHORA '{nombre}' TIENE UN TOTAL DE {consumible.Cantidad} UNIDADES EN EL INVENTARIO!.\n");
        }
        else
        {
            Console.WriteLine($"¡EL CONSUMIBLE '{nombre}' NO EXISTE EN LA LISTA!\n");
        }
    }
   
// Método para manejar el modo "cliente"

    public void ModoCliente()
    {
        Console.WriteLine("\n---MODO CLIENTE---");
        bool regresar = false;
        do
        {
            Console.WriteLine("\nSELECCIONE UNA OPCIÓN:");
            Console.WriteLine("1. Mostrar productos disponibles");
            Console.WriteLine("2. Comprar producto");
            Console.WriteLine("3. Regresar al menú principal\n");
            Console.Write("Ingrese el número de opción: ");
            int opcion = Convert.ToInt32(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    MostrarProductosDisponibles();
                    break;
                case 2:
                    ComprarProducto();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("\n¡OPCIÓN INVÁLIDA!. Por favor seleccione una opción válida.\n");
                    break;
            }
        }while (!regresar);
    }

 // Método para mostrar productos disponibles para el cliente

    public void MostrarProductosDisponibles()
    {
        foreach (var consumible in consumibles)
        {
            Console.WriteLine($"\nPRODUCTO: {consumible.Nombre}, PRECIO: ${consumible.Precio}, CANTIDAD: {consumible.Cantidad}");
        }
    }

// Método para comprar un producto

    private void ComprarProducto()
    
    {
            Console.WriteLine("\nIngrese el nombre del producto que desea comprar: ");
        string nombreProducto = Console.ReadLine() ?? "";
        var consumible = consumibles.Find(c => c.Nombre == nombreProducto);
        while (consumible == null)
        {
            Console.WriteLine($"\nEL PRODUCTO '{nombreProducto}' NO EXISTE EN LA LISTA DE PRODUCTOS.\n");
            Console.WriteLine("\nIngrese el nombre del producto que desea comprar: ");
            nombreProducto = Console.ReadLine() ?? "";
            consumible = consumibles.Find(c => c.Nombre == nombreProducto);
        }

        Console.WriteLine($"\nProducto seleccionado: {consumible.Nombre}, Precio: ${consumible.Precio}");
        Console.WriteLine($"\nIngrese el dinero con el que desea pagar el producto: $");
        decimal dineroIngresado = Convert.ToDecimal(Console.ReadLine());
        if (dineroIngresado < consumible.Precio)
        {
            Console.WriteLine("\n¡EL DINERO INGRESADO ES INSUFICIENTE PARA COMPRAR EL PRODUCTO!.\n");
            return;
        }
            
            // Cálculo del cambio con la menor cantidad de monedas posibles
            
            decimal cambio = dineroIngresado - consumible.Precio;
            int cantidadMonedas500 = 0;
            int cantidadMonedas200 = 0;
            int cantidadMonedas100 = 0;
            int cantidadMonedas50 = 0;

            
            while (cambio >= 500)
            {
                cantidadMonedas500++;
                cambio -= 500;
            }
            while (cambio >= 200)
            {
                cantidadMonedas200++;
                cambio -= 200;
            }
            while (cambio >= 100)
            {
                cantidadMonedas100++;
                cambio -= 100;
            }
            while (cambio >= 50)
            {
                cantidadMonedas50++;
                cambio -= 50;
            }

            consumible.Cantidad--;
            Console.WriteLine($"\nPRODUCTO COMPRADO: {consumible.Nombre}, PRECIO: ${consumible.Precio}");
            Console.WriteLine($"DEVOLUCIÓN: ${dineroIngresado - consumible.Precio} en...");
            Console.WriteLine($"{cantidadMonedas500} moneda(s) de $500");
            Console.WriteLine($"{cantidadMonedas200} moneda(s) de $200");
            Console.WriteLine($"{cantidadMonedas100} moneda(s) de $100");
            Console.WriteLine($"{cantidadMonedas50} moneda(s) de $50");
    }
}

