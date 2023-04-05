using System;
using System.Collections.Generic;

// Clase que representa un consumible en la máquina expendedora
public class Consumable : IConsumable
{
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

       // Constructor
    public Consumable(string nombre, decimal precio, int cantidad)
    {
        Nombre = nombre;
        Precio = precio;
        Cantidad = cantidad;
    }
}