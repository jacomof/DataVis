using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float _Unit = 1.0f;

    private float _ValueUnit = 1.0f;

    public string Label; 
    [SerializeField] private GameObject StripeText; 
    // Start is called before the first frame update
    void Start()
    {
        _Size = VisualizationBehavior.PLANE_SIZE;
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
        _lineRenderer.SetPosition(1, _stripeDif*_Size);
        var _stripePos = _stripeDif;
        for (int i = 0; i < _numStripes; ++i){

            var _textStripe = GameObject.Instantiate(StripeText, gameObject.transform);
            _stripePos += _stripePos+_stripeDif;
            _textStripe.transform.position = _stripePos;

        }

        throw new NotImplementedException();
    }

    private void _initializeYAxis()
    {

        throw new NotImplementedException();
    }

    

    private void _initializeZAxis()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
