/*
 * Author(s): Isaiah Mann
 * Description: Parses CSV files
 * Usage: [no notes]
 */

using UnityEngine;

public class CSVParser : Parser
{
    public string[,] ParseCSVFromResources (string fileName) {
        string csv = GetTextAssetInResources(CSVPath(fileName)).text;
        return ParseCSV(csv);
    }

    public string[,] ParseCSV (string csv) {
        string[,] result;
        string[] allStringsByLine = csv.Split('\n');
        string[][] allStringsByWord = new string[allStringsByLine.Length][];
        for (int i = 0; i < allStringsByLine.Length; i++) {
            allStringsByWord[i] = allStringsByLine[i].Split(',');
        }
        int width = allStringsByLine[0].Split(',').Length;
        int height = allStringsByLine.Length;
        result = new string[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
				if(x < result.GetLength(0) && 
					y < result.GetLength(1) && 
					height - y - 1 < allStringsByWord.GetLength(0) && 
					height - y - 1 >= 0 &&
					x < allStringsByWord[height - y - 1].Length)
				{
	                // Need to reverse the x-axis so the file reads in correctly
	                result[x, y] = allStringsByWord[height - y - 1][x].Trim();
				}
            }
        }
        return result;
    }
}
