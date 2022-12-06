using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;
using static VisualizationBehavior;
using TMPro;

public class PopulateElementsBar2DVs : MonoBehaviour
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
    //Margin between the groups of bars
    [SerializeField] private float Margin = 7.5f;
    
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

        //------Construct groupings of points that will be connected by lines in the visualization----------
        
        var _groupingColumnName = _data.Columns[0].Name;
        var _secondCategoryName = _data.Columns[1].Name;

        var _catValuesList1 = _catValues1.Columns.GetStringColumn("Values").ToList<string>();
        var _catValuesList2 = _catValues2.Columns.GetStringColumn("Values").ToList<string>();
        var _groupings = (_data.GroupBy<string>(_groupingColumnName)).Groupings;
        

        //Fill the matrix using the existing values in the dataframe
        //Categories 1 (_cat1) are elements of the X axis, categories 2 (_cat2) are elements of the Z axis
        
        //Set max value (usually 1) that represents the maximum height of any bar inside the visualization
        //All bars Y values are mapped to the range [0, Max Value]
        float _maxYValue = MaxYValue;

        //Set Axis parameter for position calculation and enable quick access to it from outside the class
        AxisScale = new Vector3(_numCatValues1, _maxYValue, _numCatValues2);
        
        //Place elements inside the visualization using obtained data
        placeElems(_groupings);

        //Use labels to initialize the axis
        initializeAxisLabels();

        return AxisScale;

    }

    private void placeElems(IEnumerable<IGrouping<string, DataFrameRow>> _groupings){

        int _numGroups = _groupings.Count();
        float _plane_size = VisualizationBehavior.PLANE_SIZE;
        float _groupSize = _plane_size/((float)_numGroups);
        Vector3 _currentGroupCenter = new Vector3(_groupSize, 0, 0);
        float _groupPos = Margin;
        int _colorIndex = 0;

        foreach(var _group in _groupings){

            float _groupUnit = _groupSize;
            //Vector3 _groupCenter = new Vector3(_groupSize/2.0f, 0, 0);
            float _barPos = (_groupUnit/2.0f) + _groupPos;

            foreach(var _row in _group){
                
                float _yVal = (((float) _row[2])/MaxYValue)*_plane_size;
                Vector3 _barSize = new Vector3(2, _yVal, 2);
                GameObject _bar = Instantiate(VisualizationElement, gameObject.transform);
                var _localPosition = new Vector3(_barPos, _yVal/2.0f, 0);
                _bar.transform.localPosition = _localPosition;
                _bar.transform.localScale = _barSize;
                string _label = _row[1].ToString();
                //_bar.GetComponent<BarElemGroupedBehavior>().setText(_label);
                try
                    {
                        Color _barColor = ColorArray[_colorIndex];
                         _bar.GetComponent<Renderer>().material.SetColor("_BaseColor", _barColor);
                        _colorIndex++;
                    }
                    catch (System.IndexOutOfRangeException e)
                    {
                        
                        throw new System.IndexOutOfRangeException("To many bars inside the visualization. There are not enough colors to represent them all!", e);
                       
                    }
                _barPos += _groupUnit;

            }
            _groupPos+=_groupSize;
            
        }

    }

    private void initializeAxisLabels(){

        VisualizationBehavior _visBehave = gameObject.GetComponent<VisualizationBehavior>();
        AxisParentBehavior _axisParentBehav = _visBehave._axis.GetComponent<AxisParentBehavior>();
        _axisParentBehav.IsXZNumeric = false;
        _axisParentBehav.initializeLinesOnlyY(1.0f, 2);
    }
}
