using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        foreach (var fileName in args)
        {
            // Read the file as binary data
            byte[] fileBytes = File.ReadAllBytes(fileName);
            Console.WriteLine($"Processing file: {fileName}");
            Console.WriteLine($"Input bytes: {fileBytes.Length}");

            // Decode the bytes from CP-1252 to a string
            string decodedString = Encoding.GetEncoding("Windows-1252").GetString(fileBytes);

            // Re-encode the string to UTF-8 bytes
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(decodedString);
            Console.WriteLine($"Output bytes: {utf8Bytes.Length}");

            // Construct the output file name
            string outputFileName = fileName + ".fixed";

            // Write the bytes to a new file
            File.WriteAllBytes(outputFileName, utf8Bytes);

            Console.WriteLine($"Fixed file written to {outputFileName}\n");
        }
    }
}
