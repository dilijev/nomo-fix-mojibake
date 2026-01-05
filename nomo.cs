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
            Console.WriteLine($"Input length (bytes): {fileBytes.Length}");

            // Display the file contents in hex and ASCII
            // Console.WriteLine("File contents (Hex): " + BitConverter.ToString(fileBytes).Replace("-", " "));
            // Console.WriteLine("File contents (ASCII): " + Encoding.ASCII.GetString(fileBytes));

            // Step 1: Decode the bytes as UTF-8 (even though they were intended to be Windows-1252)
            string incorrectlyDecodedString = Encoding.UTF8.GetString(fileBytes);
            // Console.WriteLine("Incorrectly Decoded String (UTF-8): " + incorrectlyDecodedString);

            // Step 2: Re-decode this string as Windows-1252
            byte[] outputBytes = Encoding.GetEncoding("Windows-1252").GetBytes(incorrectlyDecodedString);
            // Console.WriteLine("Re-encoded bytes (Windows-1252): " + BitConverter.ToString(outputBytes).Replace("-", " "));
            Console.WriteLine($"Output length (bytes): {outputBytes.Length}");

            // Construct the output file name
            string outputFileName = fileName + ".fixed";

            // Write the bytes to a new file
            File.WriteAllBytes(outputFileName, outputBytes);

            Console.WriteLine($"Fixed file written to {outputFileName}\n");
        }
    }
}
