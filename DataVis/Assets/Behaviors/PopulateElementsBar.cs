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
    private Vector3[] _valueVectors;
    private float _scaleSize;
    public Vector3 AxisScale{get; private set;}
    [SerializeField] private float Margin = 0.0f;
    [SerializeField] GameObject VisualizationElement;

    public struct VisualizationElementCell
    {

        public VisualizationElementCell(string _cat1, string _cat2, float _numVal){
            Category1 = _cat1;
            Category2 = _cat2;
            NumericValue = _numVal;
        }

        public string Category1 { get; set;}
        public string Category2 { get; set;}
        public float NumericValue { get; set;}

    }
   
    //Pre: LoadDataBehavior has a filled DataFrame from which point data can be extracted
    //This data points have the following format [category1, category2, NumericValue]
    //category1 & category2 will be interpreted as categories, and will be stored as strings
    //numeric will be the height of the bar and will be interpreted as a float
    //Post: The visualization is populated with bars representing the input data
     public Vector3 DoPopulate()
    {

        var _scaleSize = (gameObject.transform.localScale.x)*(PLANE_SIZE);
        Debug.Log("_scaleSize es: " + _scaleSize.ToString());
        var loadData = gameObject.GetComponent<LoadDataBehaviour>();
        var _data = loadData.LoadNumeric();

        //------Construct floor matrix where the numeric values will be placed----------
        
        //Create DataFrameColumns with the categorical data, and order in order for the visualization axis to be ordered as well
        var _cols = _data.Columns;
        var _catValues1 = (_cols[0].ValueCounts()).OrderBy("Values");
        var _catValues2 = (_cols[1].ValueCounts()).OrderBy("Values");

        
        var _numCatValues1 = _catValues1.Rows.ToList().Count;
        var _numCatValues2 = _catValues2.Rows.ToList().Count;

        var _barFloorMatrix = new VisualizationElementCell[_numCatValues1, _numCatValues2];

        //Map categories to indexes of the floor matrix
        var _catValuesDict1 = new Dictionary<string, int>();
        int ind = 0;
        foreach(var elem in _catValues1.Columns[0]){

            _catValuesDict1[elem.ToString()] = ind;
            ind++;
        }

        var _catValuesDict2 = new Dictionary<string, int>();
        ind = 0;
        foreach(var elem in _catValues2.Columns[0]){

            _catValuesDict2[elem.ToString()] = ind;
            ind++;

        }

        //Fill the matrix using the existing values in the dataframe
        //Remember, category 1 are elements of x axis, category 2 are elements of z axis
        foreach(var elem in _data.Rows)
        {
            string _cat1 = elem[0].ToString();
            string _cat2 = elem[1].ToString();
            int _catIndex1 = _catValuesDict1[_cat1];
            int _catIndex2 = _catValuesDict2[_cat2];

            float _val = (float) elem[2];
            _barFloorMatrix[_catIndex1, _catIndex2].Category1 = _cat1;
            _barFloorMatrix[_catIndex1, _catIndex2].Category2 = _cat2;
            _barFloorMatrix[_catIndex1, _catIndex2].NumericValue = _val;
        }
        
        //Set Axis parameter for position calculation and enable quick access to it from outside the class
        AxisScale = new Vector3(_numCatValues1, (float) _data.Columns[2].Max(), _numCatValues2);
        placeElems(_barFloorMatrix);
        return AxisScale;

    }

    private void placeElems(VisualizationElementCell[,] _barFloorMatrix)
    {

        var _plane_size = VisualizationBehavior.PLANE_SIZE;
        float _unitX = _plane_size/AxisScale.x;
        float _unitZ = _plane_size/AxisScale.z;
        float _unitMiddle = _unitX/2;

        int numElemX = (int) AxisScale.x;
        int numElemZ = (int) AxisScale.z;


        var _xColDif = 1.0f/AxisScale.x;
        var _zColDif = 1.0f/AxisScale.z;

        for(int i = 0; i < numElemZ; ++i){

            
            for(int j = 0; j < numElemX; ++j){
                if(_barFloorMatrix[j,i].NumericValue >0.0f){
                    //Calculate position of bar
                    var _posZ = _unitMiddle + (((float)i)*_unitZ);
                    var _posX = _unitMiddle + (((float)j)*_unitX);
                    float _ySize = (_barFloorMatrix[j,i].NumericValue / AxisScale.y)*_plane_size;
                    var _posY = _ySize/2.0f;
                    var _pos = new Vector3(_posX, _posY, _posZ);

                    //Set size of bar
                    Vector3 _size = new Vector3(_unitX*0.8f, _ySize, _unitZ*0.8f);

                    //Color ranges from blue to red on Z axis
                    //And from blue to green on X axis
                    //Minimum blue at right upper edge of visualization!
                    var _zColRed = (((float)i)*_zColDif);
                    var _xColGreen = (((float)j)*_xColDif);
                    var _xBlueColor = (1.0f - (((float)j)*_zColDif))*0.5f;
                    var _zBlueColor = (1.0f - (((float)i)*_xColDif))*0.5f;
                    
                    Color _barColor = new Color(_zColRed, _xColGreen, _xBlueColor+_zBlueColor);
                    

                    //Instantiate bar and set properties 
                    var _bar = Instantiate(VisualizationElement, gameObject.transform);
                    _bar.transform.localPosition = _pos;
                    _bar.transform.localScale = _size;
                    _bar.GetComponent<Renderer>().material.SetColor("_BaseColor", _barColor);
                }

            }

        }

        Debug.Log(AxisScale.ToString());

    }
}