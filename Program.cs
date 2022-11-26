using System.Text;

string path;

do
{
    //Ex: C:\Folder\File.txt
    //Ex: picture.png

    Console.WriteLine("Introduceti calea absoluta la fisier sau numele fisierului in folderul \"Fisiere\"");

    path = Console.ReadLine();

    if (!path.Contains(@"\")) path = @"..\..\..\Fisiere\" + path;
    if (!File.Exists(path)) Console.WriteLine("Fisierul nu exista!");;

} while (!File.Exists(path));

FileStream fs = File.OpenRead(path);

byte[] buffer = new byte[16];

UTF8Encoding utf8 = new UTF8Encoding();
ulong index = 0;


while (fs.Read(buffer, 0, 16) > 0)
{
    Console.Write(index.ToString("X8") + ": ");

    for (int i = 1; i <= 16; i++)
    {
        Console.Write($"{buffer[i - 1].ToString("X2")} ");

        //caractere neafisabile sunt inlocuite cu un punct; 46 este codul caracterului '.'
        if (char.IsControl((char)buffer[i - 1])) buffer[i - 1] = 46;

        if (i % 8 == 0) Console.Write("| ");
        if (i % 16 == 0) Console.WriteLine(utf8.GetString(buffer, 0, 16));
    }

    for (int i = 0; i < 16; i++) buffer[i] = 0;
    index += 16;
}

fs.Close();
Console.WriteLine();
