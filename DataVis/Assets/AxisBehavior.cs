using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisBehavior : MonoBehaviour
{
    private float _Size;
    public enum AxisId
    {
        X,
        Y,
        Z
    } 

    public AxisId MyAxisId;
    private float _Unit = 2.0f;

    private float _ValueUnit = 1.0f;

    public string Label; 
    [SerializeField] private GameObject StripeText; 
    // Start is called before the first frame update
    void Start()
    {
        _Size = VisualizationBehavior.PLANE_SIZE;
        _ValueUnit = _ValueUnit*_Unit;
        if(MyAxisId == AxisId.X)
            _initializeXAxis();
        else if(MyAxisId == AxisId.Y)
            _initializeYAxis();
        else
            _initializeZAxis();

    }

    private void _initializeXAxis()
    {
        int _numStripes = (int) Mathf.Floor(_Size/_Unit);
        Vector3 _stripeDif = new Vector3(_Unit, 0, 0);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, _stripeDif*_Size); //Sets position of the end point of the line to the size of the visualization volume
        var _stripePos = new Vector3(0,0,0);
        var _valueUnitIter = 0.0f;

        for (int i = 0; i < _numStripes; ++i){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            _valueUnitIter += _ValueUnit;
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _valueUnitIter.ToString();
            _transform.localPosition = _stripePos;

        }

    }

    private void _initializeYAxis()
    {

        int _numStripes = (int) Mathf.Floor(_Size/_Unit);
        Vector3 _stripeDif = new Vector3(0, _Unit, 0);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, _stripeDif*_Size); //Sets position of the end point of the line to the size of the visualization volume
        var _stripePos = new Vector3(-0.2f,0,0);
        var _valueUnitIter = 0.0f;

        for (int i = 0; i < _numStripes; ++i){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            _valueUnitIter += _ValueUnit;
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _valueUnitIter.ToString();
            _transform.localPosition = _stripePos;

        }
    }

    

    private void _initializeZAxis()
    {

        int _numStripes = (int) Mathf.Floor(_Size/_Unit);
        Vector3 _stripeDif = new Vector3(0, 0, _Unit);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, _stripeDif*_Size); //Sets position of the end point of the line to the size of the visualization volume
        var _stripePos = new Vector3(0,0,0);
        var _valueUnitIter = 0.0f;

        for (int i = 0; i < _numStripes; ++i){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            _valueUnitIter += _ValueUnit;
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _valueUnitIter.ToString();
            _transform.localPosition = _stripePos;
            _transform.localEulerAngles = new Vector3(0, 90.0f, 0);

        }

    }

    


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
