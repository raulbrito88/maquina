using System;
using System.Collections.Generic;

// Interfaz para los productos consumibles de la máquina expendedora
public interface IConsumable
{
    string Nombre { get; set; }
    decimal Precio { get; set; }
    int Cantidad { get; set; }
}