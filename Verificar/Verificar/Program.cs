using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string nombreArchivo = "miarchivo.txt";
        string contenido = "Este es el contenido del archivo";

        Console.WriteLine("Ingrese el nombre del archivo que desea crear:");
        nombreArchivo = Console.ReadLine();
        Console.WriteLine("Ingrese el contenido que desea que lleve el archivo:");
        contenido = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("¿Qué desea hacer?\n[E]ditar\n[V]er\n[S]alir");
            char opcion = char.Parse(Console.ReadLine().ToUpper());

            switch (opcion)
            {
                case 'E':
                    Console.WriteLine("Ingrese el texto adicional para el archivo:");
                    string textoAdicional = Console.ReadLine();
                    contenido += Environment.NewLine + textoAdicional;
                    Console.WriteLine("Texto añadido al archivo.");
                    break;
                case 'V':
                    VerArchivo(nombreArchivo, contenido);
                    break;
                case 'S':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ingrese una opción válida.");
                    break;
            }
        }
    }

    static void VerArchivo(string nombreArchivo, string contenido)
    {
        try
        {
            File.WriteAllText(nombreArchivo, contenido);
            Console.WriteLine($"Archivo '{nombreArchivo}' creado y contenido escrito con éxito.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocurrió un error al crear o escribir en el archivo: " + e.Message);
        }

        try
        {
            using (StreamReader sr = new StreamReader(nombreArchivo))
            {
                string linea;
                string[] palabrasClave = { "si", "mientras" };
                Console.WriteLine($"Contenido del archivo '{nombreArchivo}':");

                while ((linea = sr.ReadLine()) != null)
                {
                    Console.WriteLine(linea);

                    foreach (string palabra in palabrasClave)
                    {
                        if (Regex.IsMatch(linea, @"\b" + Regex.Escape(palabra) + @"\b", RegexOptions.IgnoreCase))
                        {
                            Console.WriteLine("Se encontró una palabra clave en el archivo.");
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocurrió un error al leer el archivo: " + e.Message);
        }
    }
}
