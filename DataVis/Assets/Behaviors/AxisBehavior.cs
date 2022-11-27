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
    
    [SerializeField] private GameObject StripeText; 
    // Start is called before the first frame update
    public List<string> XLabels;
    public List<string> ZLabels;
    public float MaxYValue;

    public int NumberOfLabels = 10;
    void Start()
    {

    }

    public void initializeAxis(System.Object _labels){

        switch(MyAxisId){
            case AxisId.X:
                XLabels = (List<string>) _labels; 
                _initializeXAxis();
                break;
            case AxisId.Y:
                MaxYValue = (float) _labels; 
                _initializeYAxis();
                break;
            case AxisId.Z:
                ZLabels = (List<string>) _labels; 
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
        var _stripePos = new Vector3(0f,0.1f,-0.1f);

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
        List<(string, float)> _numericLabels = _getLabelsFromNumber(MaxYValue);
        var _lineRenderer = gameObject.GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(1, new Vector3(0,_size,0)); //Sets position of the end point of the line to the size of the visualization volume
        
        foreach (var (_label, _pos) in _numericLabels){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            var _stripePos = new Vector3(-0.5f, _pos, 0);
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
        var _stripePos = new Vector3(-0.25f,0,0);
        var _rotation = new Vector3(0,90.0f,0);
        

        foreach (string _label in ZLabels){
            
            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripeDif;
            
            var _textComp = _textStripe.GetComponent<TextMeshPro>();
            var _transform = _textStripe.GetComponent<RectTransform>();
            _textComp.text = _label;
            _transform.localPosition = _stripePos;
            _transform.localEulerAngles = _rotation;
        }

    }

    List<(string, float)> _getLabelsFromNumber(float _number)
    {
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        
        //Format exact _unit string to char array, change first non-0 digit to 1
        //Limit precision to 5 decimals
        float _unit = _number/NumberOfLabels;
        char[] _unitChars = _unit.ToString("F3").ToCharArray();
        int i = 0;
        while(_unitChars[i] == '0')
        {
            ++i;

        }

        //Construct new digit array, using position of first non-0 digit as the magnitude of the units
        int _labelUnitCharsLength = _unitChars.Length;
        char[] _labelUnitChars = new char[_labelUnitCharsLength];
        _labelUnitChars[i] = '1';

        //Convert array of digits back to float
        float _labelUnit = float.Parse(new string(_labelUnitChars));
        float _labelRate = _number/(_labelUnit*(float)NumberOfLabels);
        int _labelNum = (int) Mathf.Ceil(_number/_labelUnit);
        //_labelUnit*=_labelRate;
        
        //Initialize list of labels using previously calculated units of labels
        float _sumLabelUnits = 0.0f;
        var _labelStrings = new List<(string, float)>();
        
        /*while(_sumLabelUnits <= _number){
            _labelStrings.Add((_sumLabelUnits.ToString("F5"), _sumLabelUnits));
            _sumLabelUnits += _labelUnit;
        }*/

        for(int j = 0; j <= _labelNum; ++j)
        {
            
            _labelStrings.Add((_sumLabelUnits.ToString("F3"), _sumLabelUnits));
            _sumLabelUnits += _labelUnit;

        }

        return _labelStrings;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
