using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;
using static VisualizationBehavior;

//Class that represents a behavior that populates a visualization with bars encoding the data imported from the input file
//selected in the main visualization behavior. It first loads the data into a Dataframe, then creates a list of (different) categories
//from the first and second columns of the DataFrame, then creates a matrix (named floor matrix) with this categories lists as rows and columns (respectively)
//assigning each cell a position in the X and Z plane, and finally places one bar for each of the combination of categories that has a value.
public class PopulateElementsBar : MonoBehaviour
{
    private Vector3[] _valueVectors;

    //The scales of the axis in each direction
    public Vector3 AxisScale{get; private set;}

    //The GameObject that is used as visualization element to instantiate the bars
    [SerializeField] private GameObject VisualizationElement;

    //An array containing a qualitative color pallete to color the bars
    //It's initialized in the editor
    //This limits the amount of different bars that can be displayed inside the visualization
    [SerializeField] private Color[] ColorArray;

    //MaxYValue represents the maximum height of any bar inside the visualization
    [SerializeField] private float MaxYValue = 1.0f;
    

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

        var loadData = gameObject.GetComponent<LoadDataBehaviour>();
        var _data = loadData.LoadCategoric();

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
        //Categories 1 (_cat1) are elements of the X axis, categories 2 (_cat2) are elements of the Z axis
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
        //Set max value (usually 1) that represents the maximum height of any bar inside the visualization
        //All bars Y values are mapped to the range [0, Max Value]
        float _maxYValue = MaxYValue;

        //Set Axis parameter for position calculation and enable quick access to it from outside the class
        AxisScale = new Vector3(_numCatValues1, _maxYValue, _numCatValues2);
        
        //Place elements inside the visualization using obtained data
        placeElems(_barFloorMatrix);

        //Use labels to initialize the axis
        initializeAxisLabels(_catValues1, _maxYValue,_catValues2);

        return AxisScale;

    }

    //Private auxiliary function that receives a floor matrix and instantiate the bars in their respective position inside the visualization
    private void placeElems(VisualizationElementCell[,] _barFloorMatrix)
    {
        
        var _plane_size = VisualizationBehavior.PLANE_SIZE;

        //Units represent how much does a cell in the matrix occupy in visualization space
        float _unitX = _plane_size/AxisScale.x;
        float _unitZ = _plane_size/AxisScale.z;
        float _unitMiddleX = _unitX/2.0f;
        float _unitMiddleZ = _unitZ/2.0f;

        int numElemX = (int) AxisScale.x;
        int numElemZ = (int) AxisScale.z;


        var _xColDif = 1.0f/AxisScale.x;
        var _zColDif = 1.0f/AxisScale.z;

        int colorIndex = 0;

        for(int i = 0; i < numElemZ; ++i){
            for(int j = 0; j < numElemX; ++j){
                if(_barFloorMatrix[j,i].NumericValue >0.0f){

                    //Calculate position of bar
                    var _posZ = _unitMiddleZ + (((float)i)*_unitZ);
                    var _posX = _unitMiddleX + (((float)j)*_unitX);
                    float _ySize = (_barFloorMatrix[j,i].NumericValue / AxisScale.y)*_plane_size;
                    var _posY = _ySize/2.0f;
                    var _pos = new Vector3(_posX, _posY, _posZ);

                    //Set size of bar
                    Vector3 _size = new Vector3(_unitX*0.5f, _ySize, _unitZ*0.5f);
                    
                    //Instantiate bar and set properties 
                    var _bar = Instantiate(VisualizationElement, gameObject.transform);
                    _bar.transform.localPosition = _pos;
                    _bar.transform.localScale = _size;

                    try
                    {
                        Color _barColor = ColorArray[colorIndex];
                        Renderer r =  _bar.GetComponent<Renderer>();
                        r.material.SetColor("_BaseColor", _barColor);
                        r.material.SetColor("_EmissionColor", _barColor);
                        colorIndex++;
                    }
                    catch (System.IndexOutOfRangeException e)
                    {
                        
                        throw new System.IndexOutOfRangeException("To many bars inside the visualization. There are not enough colors to represent them all!", e);
                       
                    }
                    
                }

            }

        }

        Debug.Log(AxisScale.ToString());

    }

    private void initializeAxisLabels(DataFrame _catValues1, float _maxYValue,DataFrame _catValues2)
    {

        List<string> _labelListX = _catValues1.Columns.GetStringColumn("Values").ToList<string>();
        List<string> _labelListZ = _catValues2.Columns.GetStringColumn("Values").ToList<string>();
        
        VisualizationBehavior _visBehave = gameObject.GetComponent<VisualizationBehavior>(); 
        System.Object[] _objList = new System.Object[]{_labelListX, _maxYValue,_labelListZ};
        AxisParentBehavior _axisParentBehav = _visBehave._axis.GetComponent<AxisParentBehavior>();
        _axisParentBehav.IsXZNumeric = false;
        _axisParentBehav.initializeLines(_objList);

    }
}