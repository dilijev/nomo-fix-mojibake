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

            // Step 1: Decode the bytes as UTF-8 (even though they were intended to be Windows-1252)
            string incorrectlyDecodedString = Encoding.UTF8.GetString(fileBytes);

            // Step 2: Re-decode this string as Windows-1252
            byte[] windows1252Bytes = Encoding.GetEncoding("Windows-1252").GetBytes(incorrectlyDecodedString);

            // Step 3: Re-encode the correctly interpreted data back to UTF-8
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(Encoding.GetEncoding("Windows-1252").GetString(windows1252Bytes));
            Console.WriteLine($"Output bytes: {utf8Bytes.Length}");

            // Construct the output file name
            string outputFileName = fileName + ".fixed";

            // Write the bytes to a new file
            File.WriteAllBytes(outputFileName, utf8Bytes);

            Console.WriteLine($"Fixed file written to {outputFileName}\n");
        }
    }
}
