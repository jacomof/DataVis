using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Data.Analysis;
using System.IO;
using UnityEditor;

public class loadDataBehaviour : MonoBehaviour
{
    [SerializeField] private TextAsset dataAsset;
    [SerializeField] private bool HasHeader;
    public DataFrame Data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadData()
    {
        DataFrame readData = DataFrame.LoadCsvFromString(dataAsset.ToString(), header:true);
        Debug.Log("File read!");
        bool[] arrayBool = {false,true,false,true,true,false};
        //Debug.Log(readData.ToString());
        Data = readData;
    }

    public DataFrame GetDataFrame(){

        return Data;

    }

}
