using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;
using static VisualizationBehavior;

public class PopulateElementsScatter : MonoBehaviour
{
    // Start is called before the first frame update
    private float[,] _valueMatrix;  
    private Vector3[] _valueVectors;

    private float _scaleSize;

    public Vector3 AxisScale;

    [SerializeField] private float Margin = 0.2f;

    [SerializeField] GameObject VisualizationElement;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //Pre: LoadDataBehavior has a filled DataFrame from which point data can be extracted
    //The data is assumed to have the order X, Y, Z (meaning column 1 is X data, column 2 is Y data 
    //and column 3 is Z data, this is for convenience since the visualization is essentialy a set of 3D points)
    //Post: The visualization is populated with scatter points representing the input data
     public Vector3 DoPopulate()
    {
        var _scaleSize = (gameObject.transform.localScale.x)*(PLANE_SIZE);
        Debug.Log("_scaleSize es: " + _scaleSize.ToString());
        var loadData = gameObject.GetComponent<LoadDataBehaviour>();
        var _data = loadData.LoadNumeric();
        var _size = new int[]{_data.Rows.ToList().Count, _data.Columns.ToList().Count};
        _valueVectors = new Vector3[_size[0]];
        _valueMatrix = new float[_size[0],3];
        float[] _valueMaxes = new float[3];
        int j = 0;
        foreach(var _col in _data.Columns){
            int i = 0;
            if(Object.ReferenceEquals(_col.DataType, "".GetType())){

                //Mapping to 0,1,2 done here
                Debug.Log("Breaking!!");
                break;

            }else{
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
        initializeAxisLabels(AxisScale.x, AxisScale.y, AxisScale.z);
        return AxisScale;
    }

    private void placeElems(float[,] _valueMatrix)
    {
        var plane_size = VisualizationBehavior.PLANE_SIZE;
        for(int i = 0; i < _valueMatrix.GetLength(0); ++i){

            var _visTop = plane_size - Margin;
            var _pointPos = new Vector3((_valueMatrix[i,0]), (_valueMatrix[i,1]), (_valueMatrix[i,2]));
            _pointPos = new Vector3(_pointPos.x/AxisScale.x, _pointPos.y/AxisScale.y, _pointPos.z/AxisScale.z);
            _pointPos = _pointPos*plane_size;
            var _point = Instantiate(VisualizationElement, gameObject.transform);
            _point.transform.localPosition = _pointPos;

        }
        Debug.Log(AxisScale.ToString());

    }

    private void initializeAxisLabels(float _maxXValue, float _maxYValue, float _maxZValue)
    {
        
        VisualizationBehavior _visBehave = gameObject.GetComponent<VisualizationBehavior>(); 
        System.Object[] _objList = new System.Object[]{_maxXValue, _maxYValue, _maxZValue};
        AxisParentBehavior _axisParentBehav = _visBehave._axis.GetComponent<AxisParentBehavior>();
        _axisParentBehav.IsXZNumeric = true;
        _axisParentBehav.initializeLines(_objList);

    }
}
