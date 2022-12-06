using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Data.Analysis;
using System.IO;
using UnityEditor;

public class LoadDataBehaviour : MonoBehaviour
{
    [SerializeField] public TextAsset dataAsset;
    [SerializeField] private bool HasHeader;
    public DataFrame Data {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads data trying to guess types of columns
    public DataFrame LoadData()
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        DataFrame readData = DataFrame.LoadCsvFromString(dataAsset.ToString(), header:true);
        Debug.Log("File read!");
        return readData;

    }

    //Loads data assuming first two columns contain string data, and the last column contains numerical values (read as floats)
    public DataFrame LoadCategoric()
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        var ts = new System.Type[] { string.Empty.GetType(), string.Empty.GetType(), 0.0f.GetType()};
        DataFrame readData = DataFrame.LoadCsvFromString(dataAsset.ToString(), header:true, dataTypes: ts);
        Debug.Log("File read!");
        return readData;

    }

    public DataFrame LoadNumeric()
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Type _floatType = 0.0f.GetType();
        DataFrame readData = DataFrame.LoadCsvFromString(dataAsset.ToString(), header:true, dataTypes: new System.Type[]{_floatType,_floatType,_floatType});
        Debug.Log("File read!");
        return readData;

    }

    public DataFrame LoadPie()
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        var ts = new System.Type[] {string.Empty.GetType(), 0.0f.GetType()};
        DataFrame readData = DataFrame.LoadCsvFromString(dataAsset.ToString(), header:true, dataTypes: ts);
        return readData;


    }
    public DataFrame GetDataFrame(){

        return Data;

    }

}
