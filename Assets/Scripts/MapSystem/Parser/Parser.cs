/*
 * Author(s): Isaiah Mann
 * Description: Base class for parsing text
 * Usage: [no notes]
 */

using System.IO;
using UnityEngine;
using k = Global;

public abstract class Parser 
{
    const string JSON_DIR = k.JSON_DIR;
    const string CSV_DIR = k.CSV_DIR;
    const string PREFABS_DIR = k.PREFABS_DIR;

    protected const string DEFAULT_JOIN_KEY = "+";
    protected const string DEFAULT_DELEGATE_KEY = ":";
    protected const string DEFAULT_DELEGATE_SEPARATOR_KEY = "|";


    protected TextAsset GetTextAssetInResources(string path) 
    {
        return Resources.Load<TextAsset>(path);
    }

    protected string JSONPath(string fileName) 
    {
        return Path.Combine(JSON_DIR, fileName);
    }

    protected string CSVPath(string fileName)
    {
        return Path.Combine(CSV_DIR, fileName);
    }
        
    protected string PrefabsPath(string fileName) 
    {
        return Path.Combine(PREFABS_DIR, fileName);
    }
     
    protected string removeQuoteMarks(string source)
    {
        return source.Replace("\"", "");
    }

    protected string[] removeQuoteMarksArr(params string[] source)
    {
        for(int i = 0; i < source.Length; i++)
        {
            source[i] = removeQuoteMarks(source[i]);
        }
        return source;
    }

}
