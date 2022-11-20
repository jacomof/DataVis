using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;

public class PopulateElementsBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    float[,] _valueMatrix;  
    Vector3[] _valueVectors;
    void Start()
    {
        var loadData = gameObject.GetComponent<loadDataBehaviour>();
        var _data = loadData.GetDataFrame();
        var _size = new int[]{_data.Rows.ToList().Count, _data.Columns.ToList().Count};
        _valueVectors = new Vector3[_size[0]];
        _valueMatrix = new float[_size[0],3];

        foreach(var col in _data.Columns){

            if(Object.ReferenceEquals(col.DataType, "".GetType())){

                //Mapping to 0,1,2 done here
                Debug.Log("Breaking!!");
                break;

            }else{
                
                foreach(var elem in col){
                    
                    _valueMatrix[0,1] = (float) elem;
                    Debug.Log("Elem is: " + elem.ToString());

                }
            }

        }
        /*
        Debug.Log("Size is: " + _size[0].ToString() + ", " + _size[1].ToString());
        for(long i = 0; i < _size[0]; ++i){
                var _xVal = (float) _data[i,0];
                var _yVal = (float) _data[i,1];
                var _zVal = (float) _data[i,2];
                _valueVectors[i] = new Vector3(_xVal,_yVal,_zVal);
        }
        */


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
