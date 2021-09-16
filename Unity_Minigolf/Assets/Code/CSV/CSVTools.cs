using System;
using System.Collections.Generic;
using System.IO; //we need System.IO for saving the csv Document
using UnityEngine; 

namespace Code.CSV
{
    public class CSVTools
    {
        
        public static void CreateEmptyCsv(List<string> csvHeader, string fileAddress, char separator = ',')
        {
            
            
            // initialize the variables as being empty
            List<string> csvLines = new List<string>();
            string finalHeaders = "";
            // headers as first row, separated with comma
            foreach (var header in csvHeader)
            {
                // attach each header to our List of headers
                finalHeaders += header + separator;
            }
            csvLines.Add(finalHeaders);

            // Create an empty file
            String path = Path.Combine(Application.dataPath, "ExperimentData");
            Directory.CreateDirectory(path);
            
            // writing the csvLines to the csvFile:
            SaveCsv(csvLines, fileAddress);
        }
        
        public static void UpdateCsv(List<List<string>> csvData, string fileAddress, char separator = ',')
        {
            // creating csvLines in the correct format from csvData collected in the Statistics
            List<string> csvLines = new List<string>();
            foreach (var rowOfData in csvData)
            {
                // initialize finalRowOfData as being empty
                string finalRowOfData = "";
                // create single row of data
                foreach (string dataItem in rowOfData)
                {
                    // attach each dataItem from the rowOfData to the finalRowOfData
                    finalRowOfData += dataItem + separator;
                }
                csvLines.Add(finalRowOfData);
            }
            // writing the csvLines to the csvFile:
            SaveCsv(csvLines, fileAddress);
        }

        public static List<string> CreateCsv(List<List<string>> data, List<string> csvHeader, char separator = ',')
        {
            // initialize the variables as being empty
            List<string> finalData = new List<string>();
            string finalHeaders = "";
            // headers as first row, separated with comma
            
            foreach (var header in csvHeader)
            {
                // attach each header to our List of headers
                finalHeaders += header + separator;
            }
            finalData.Add(finalHeaders);

            // create and add each row of data after each other
            foreach (List<string> rowOfData in data)
            {
                string finalRowOfData = "";
                // create single Row of Data
                foreach (string dataItem in rowOfData)
                {
                    finalRowOfData += dataItem + separator;
                }
                finalData.Add(finalRowOfData);
            }
            return finalData;
        }

        // after creating CSV file, export and save it
        // return bool to check functionality
        public static bool SaveCsv(List<string> csvLines, string fileAddress)
        {
            try
            {
                using (StreamWriter csvWriter = new StreamWriter(fileAddress, true))
                {
                    foreach (string lineInCsv in csvLines)
                    {
                        // csvWriter prints individual line into document, then place full file at fileAddress
                        csvWriter.WriteLine(lineInCsv);
                    }
                    csvWriter.Flush();
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

        public static string GetPath()
        {
            if (Application.isEditor) return Application.dataPath + "/Assets/CSVData/data.csv";
            else
            {
                return Path.Combine(Application.dataPath, "ExperimentData", "data.csv");
            }
        }
    }
}
