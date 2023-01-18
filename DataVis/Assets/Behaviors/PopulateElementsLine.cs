using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;
using static VisualizationBehavior;

public class PopulateElementsLine : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3[] _valueVectors;
    private float _scaleSize;
    public Vector3 AxisScale{get; private set;}
    [SerializeField] private float Margin = 0.0f;
    [SerializeField] GameObject VisualizationElement;
    [SerializeField] LineRenderer VisualizationLine;

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
        var _data = loadData.LoadCategoric();
        
        
        var _cols = _data.Columns;
        _data = _data.OrderBy(_cols[0].Name);
        var _catValues1 = (_cols[0].ValueCounts()).OrderBy("Values");
        var _catValues2 = (_cols[1].ValueCounts()).OrderBy("Values");

        var _numCatValues1 = _catValues1.Rows.ToList().Count;
        var _numCatValues2 = _catValues2.Rows.ToList().Count;

        //------Construct groupings of points that will be connected by lines in the visualization----------
        
        var _groupingColumnName = _data.Columns[1].Name;
        var _secondCategoryName = _data.Columns[0].Name;

        var _catValuesList1 = _catValues1.Columns.GetStringColumn("Values").ToList<string>();
        var _catValuesList2 = _catValues2.Columns.GetStringColumn("Values").ToList<string>();
        var _groupings = (_data.GroupBy<string>(_groupingColumnName)).Groupings;

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
        float _maxYValue = (float) _data.Columns[2].Max();
    
        //Set Axis parameter for position calculation and enable quick access to it from outside the class
        AxisScale = new Vector3(_numCatValues1, _maxYValue, _numCatValues2);
        //Place elements inside the visualization volume, and connect them with lines accordingly
        placeElems(_catValuesDict1, _catValuesDict2, _groupings);
        //Use labels to initialize the axis
        initializeAxisLabels(_catValues1, _maxYValue,_catValues2);
        return AxisScale;

    }

    private void placeElems(Dictionary<string, int> _catValues1, Dictionary<string, int> _groupingValues, IEnumerable<IGrouping<string, DataFrameRow>> _groupings)
    {

        var _plane_size = VisualizationBehavior.PLANE_SIZE;
        float _unitX = _plane_size/AxisScale.x;
        float _unitZ = _plane_size/AxisScale.z;
        float _unitMiddle = _unitX/2;

        int numElemX = (int) AxisScale.x;
        int numElemZ = (int) AxisScale.z;

        
        foreach(var _group in _groupings)
        {
            GameObject prevObj = null;

            foreach(var _row in _group){
                float _xInd = _catValues1[(string) _row[0]];
                float _zInd = _groupingValues[(string) _row[1]];
                var _posZ = _unitMiddle + (_zInd*_unitZ);
                var _posX = _unitMiddle + (_xInd*_unitX);

                float _posY = (((float)_row[2]) / AxisScale.y)*_plane_size;
                GameObject _point = GameObject.Instantiate(VisualizationElement, gameObject.transform);
                var _pos = new Vector3(_posX, _posY, _posZ);
                _point.transform.localPosition = _pos;

                if(prevObj != null){
                    LineRenderer _line = Instantiate<LineRenderer>(VisualizationLine, gameObject.transform);
                    var _prevPos = prevObj.transform.localPosition;
                    _line.SetPositions(new Vector3[]{_prevPos, _pos});    
                    
                }prevObj = _point;   

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
        _axisParentBehav.initializeLines(_objList);

    }
}