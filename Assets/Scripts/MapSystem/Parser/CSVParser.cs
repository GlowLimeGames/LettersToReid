/*
 * Author(s): Isaiah Mann
 * Description: Parses CSV files
 * Usage: [no notes]
 */

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
                // Need to reverse the x-axis so the file reads in correctly
                result[x, y] = allStringsByWord[height - y - 1][x].Trim();
            }
        }
        return result;
    }
}
