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
    //Distance between Axis and bars 
    [SerializeField] private float Margin = 7.5f;
    [SerializeField] private GameObject GazeObject;
    
   public void DoPopulate()
    {

        var loadData = gameObject.GetComponent<LoadDataBehaviour>();
        DataFrame _data = loadData.LoadExperimentData();
        var _cols = _data.Columns;
        
        //Set max value (usually 1) that represents the maximum height of any bar inside the visualization
        //All bars Y values are mapped to the range [0, Max Value]
        float _maxYValue = MaxYValue;
        
        //Place elements inside the visualization using obtained data
        placeElems(_data);

        //Use labels to initialize the axis
        initializeAxis();

        

    }

    private void placeElems(DataFrame _data){

        int _numGroups = 2;
        float _plane_size = VisualizationBehavior.PLANE_SIZE;
        float _groupSize = _plane_size/((float)_numGroups);
        Vector3 _currentGroupCenter = new Vector3(_groupSize, 0, 0);
        float _groupPos = Margin;
        int _colorIndex = 0;
        float _groupUnit = _groupSize;
        

        for(int i = 0; i < 2; ++i){
            Debug.Log("Añado Gaze Detector!");
            //First the bar itself is instantiated and configured properly
            float _barPos = _groupPos;
            float _yVal = (((float) _data[0,i])/MaxYValue)*_plane_size;
            Vector3 _barSize = new Vector3(2, _yVal, 2);
            GameObject _bar = Instantiate(VisualizationElement, gameObject.transform);
            var _localPosition = new Vector3(_barPos, _yVal/2.0f, 0);
            _bar.transform.localPosition = _localPosition;
            _bar.transform.localScale = _barSize;
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
                
            //Then Gaze detector is added
            var _yGazeValue = _plane_size-_yVal;
            var _yGazeCenter = _yVal + _yGazeValue/2.0f;
            GameObject gazeObject = Instantiate(GazeObject, gameObject.transform);
            gazeObject.transform.localScale = new Vector3(2, _yGazeValue, 2);
            gazeObject.transform.localPosition = new Vector3(_barPos, _yGazeCenter, 0);
            gazeObject.GetComponent<Gazer>()._visualizationBehavior = gameObject.GetComponent<VisualizationBehavior>();
            _groupPos+=_groupSize;
        }
            
            
    }

    

    private void initializeAxis(){

        VisualizationBehavior _visBehave = gameObject.GetComponent<VisualizationBehavior>();
        AxisParentBehavior _axisParentBehav = _visBehave._axis.GetComponent<AxisParentBehavior>();
        _axisParentBehav.IsXZNumeric = false;
        _axisParentBehav.initializeLinesOnlyY(1.0f, 2);
    }
}
