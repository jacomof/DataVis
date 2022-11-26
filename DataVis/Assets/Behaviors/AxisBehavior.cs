using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisBehavior : MonoBehaviour
{
    private float _size = VisualizationBehavior.PLANE_SIZE;
    public enum AxisId
    {
        X,
        Y,
        Z
    } 

    public AxisId MyAxisId;

    public string Label; 
    [SerializeField] private GameObject StripeText; 
    // Start is called before the first frame update
    public List<string> XLabels;
    public List<string> ZLabels;
    public List<string> YLabels;
    void Start()
    {

    }

    public void initializeAxis(List<string> _labelList){

        switch(MyAxisId){
            case AxisId.X:
                XLabels = _labelList; 
                _initializeXAxis();
                break;
            case AxisId.Y:
                YLabels = _labelList; 
                _initializeYAxis();
                break;
            case AxisId.Z:
                ZLabels = _labelList; 
                _initializeZAxis();
                break;
            default:
                throw new ArgumentException("The axis has to have a valid AxisId!");
        }

    }

    private void _initializeXAxis()
    {

        int _numStripes = (int) XLabels.Count;
        var _unit = _size/_numStripes;
        Vector3 _stripeDif = new Vector3(_unit, 0, 0);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, new Vector3(_size,0,0)); //Sets position of the end point of the line to the size of the visualization volume
        var _stripePos = new Vector3(0,0,0);
        

        foreach (string _label in XLabels){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _label;
            _transform.localPosition = _stripePos;

        }

    }

    private void _initializeYAxis()
    {

        int _numStripes = (int) YLabels.Count;
        var _unit = _size/_numStripes;
        Vector3 _stripeDif = new Vector3(0, _unit, 0);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, new Vector3(0,_size,0)); //Sets position of the end point of the line to the size of the visualization volume
        var _stripePos = new Vector3(0,0,0);
        

        foreach (string _label in YLabels){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _label;
            _transform.localPosition = _stripePos;

        }
    }

    

    private void _initializeZAxis()
    {

        int _numStripes = (int) ZLabels.Count;
        var _unit = _size/_numStripes;
        Vector3 _stripeDif = new Vector3(0, 0, _unit);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, new Vector3(0,0,_size)); //Sets position of the end point of the line to the size of the visualization volume
        var _stripePos = new Vector3(0,0,0);
        

        foreach (string _label in ZLabels){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _label;
            _transform.localPosition = _stripePos;

        }

    }

    


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
