using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Data.Analysis;

public class loadDataBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var path="/home/joaquin/DataVis/DataVis/Assets/Data/test_data.csv";
        TextFieldParser csvParser = new TextFieldParser(path);
        csvParser.CommentTokens = new string[] { "#" };
        csvParser.SetDelimiters(new string[] {","});
        csvParser.HasFieldsEnclosedInQuotes = true;
    
        csvParser.ReadLine();

        while (!csvParser.EndOfData)
        {
            string[] fields = csvParser.ReadFields();
        }
    
        Debug.Log("File read!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
