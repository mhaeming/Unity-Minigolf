using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //we need System.IO for saving the csv Document

public class CSVTools
{
    public static List<string> GenerateCsv(List<List<string>> data, List<string> csvHeader, char separator = ',')
    {
        // initialize the variables as being empty
        List<string> finalData = new List<string>();
        string finalHeaders = "";
        // headers as first row, separated with comma
        foreach (var header in csvHeader)
        {
            // attach each header to our List of headers
            finalHeaders = finalHeaders + header + separator;
        }
        finalData.Add(finalHeaders);

        // create and add each row of data after each other
        foreach (List<string> rowOfData in data)
        {
            string finalRowOfData = "";
            // create single Row of Data
            foreach (string dataItem in rowOfData)
            {
                finalRowOfData = finalRowOfData + dataItem + separator;
            }
            finalData.Add(finalRowOfData);
        }
        return finalData;
    }

    // after creating CSV file, export and save it
    // return bool to check functionality
    public static bool SaveCsv(List<string> csvLines, string fileAddress, string extension = ".csv")
    {
        try
        {
            using (StreamWriter csvWriter = new StreamWriter(fileAddress + extension))
            {
                foreach (string lineInCsv in csvLines)
                {
                    // csvWriter prints individual line into document, then place full file at fileAddress
                    csvWriter.WriteLine(lineInCsv);
                }
                csvWriter.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
        return true;
    }
}
