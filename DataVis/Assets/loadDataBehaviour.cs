using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Data.Analysis;
using System.IO;
using UnityEditor;

public class loadDataBehaviour : MonoBehaviour
{
    [SerializeField] private TextAsset dataAsset;
    // Start is called before the first frame update
    void Start()
    {
        
        TextFieldParser csvParser = new TextFieldParser(AssetDatabase.GetAssetPath(dataAsset));
        csvParser.CommentTokens = new string[] { "#" };
        csvParser.SetDelimiters(new string[] {","});
        csvParser.HasFieldsEnclosedInQuotes = true;
    
        csvParser.ReadLine();

        while (!csvParser.EndOfData)
        {
            string[] fields = csvParser.ReadFields();
        }
        DataFrame readData = DataFrame.LoadCsvFromString(dataAsset.ToString(), header:false);
        Debug.Log("File read!");
        bool[] arrayBool = {false,true,false,true,true,false};
        //Debug.Log(readData.ToString());
        DataFrameColumnCollection dfc = readData.Columns;
        DataFrameColumn[] dfcArray = new StringDataFrameColumn[]{(StringDataFrameColumn) dfc["Column0"], (StringDataFrameColumn) dfc["Column1"]};
        DataFrame subData = new DataFrame(dfcArray);
        Debug.Log((readData[arrayBool]).ToString());
        Debug.Log(subData.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
