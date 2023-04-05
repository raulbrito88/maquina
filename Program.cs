using System;

// Clase que arranca la máquina expendedora
class Program
{
    static void Main(string[] args)
    {
        var maquina = new MaquinaExpendedora();
        maquina.MostrarMenu();
    }
}