using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;
using static VisualizationBehavior;

public class PopulateElementsBar : MonoBehaviour
{
    // Start is called before the first frame update
    private float[,] _valueMatrix;  
    private Vector3[] _valueVectors;

    private float _scaleSize;

    public Vector3 AxisScale;

    [SerializeField] private float Margin = 0.0f;

    [SerializeField] GameObject VisualizationElement;
    void Start()
    {
        
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
    //Pre: LoadDataBehavior has a filled DataFrame from which point data can be extracted
    //Post: The visualization is populated with scatter points representing the input data
     public Vector3 DoPopulate()
    {
        var _scaleSize = (gameObject.transform.localScale.x)*(PLANE_SIZE);
        Debug.Log("_scaleSize es: " + _scaleSize.ToString());
        var loadData = gameObject.GetComponent<LoadDataBehaviour>();
        var _data = loadData.GetDataFrame();
        var _size = new int[]{_data.Rows.ToList().Count, _data.Columns.ToList().Count};
        _valueVectors = new Vector3[_size[0]];
        _valueMatrix = new float[_size[0],3];
        int j = 0;
        foreach(var _col in _data.Columns){
            int i = 0;
            if(Object.ReferenceEquals(_col.DataType, "".GetType())){

                //Mapping to 0,1,2 done here
                Debug.Log("Breaking!!");
                break;

            }else{
                //Since there'll be categorical data in X and Z dimensions, number of elements is taken as scale 
                if(j == 0 || j == 2)
                    AxisScale[j] = _size[0];
                else
                    AxisScale[j] = (float) _col.Max();
                foreach(var elem in _col){
                    
                    _valueMatrix[i,j] = (float) elem;
                    Debug.Log("Elem is: " + elem.ToString());
                    i++;

                }

            }
            j++;
        }
        placeElems(_valueMatrix);
        return AxisScale;
    }

    private void placeElems(float[,] _valueMatrix)
    {
        var plane_size = VisualizationBehavior.PLANE_SIZE;
        float _unitX = plane_size/AxisScale.x;
        float _unitZ = plane_size/AxisScale.z;
        float _unitMiddle = _unitX/2;

        int numElemX = (int) AxisScale.x;
        int numElemZ = (int) AxisScale.z;


        for(int i = 0; i < numElemZ; ++i){
            for(int j = 0; i < numElemX; ++j){
        
            /*var _visTop = plane_size - Margin;
            var _pointPos = new Vector3((_valueMatrix[i,0]), 0, (_valueMatrix[i,2]));
            _pointPos = new Vector3(_pointPos.x/AxisScale.x, 0, _pointPos.z/AxisScale.z);
            _pointPos = _pointPos*_visTop;*/
            var posx = _unitMiddle + (((float)i)*_unitZ);
            var posz = _unitMiddle + (((float)j)*_unitX);
            var _pos = new Vector3(posx, 0, posz);
            var _point = Instantiate(VisualizationElement, gameObject.transform);
            _point.transform.position = _pos;
            
            }

        }
        Debug.Log(AxisScale.ToString());

    }
}