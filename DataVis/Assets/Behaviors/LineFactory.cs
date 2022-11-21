using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFactory : MonoBehaviour
{
    // Start is called before the first frame update
    private static float PLANE_SIZE = VisualizationBehavior.PLANE_SIZE;
    [SerializeField] private float unit;
    [SerializeField] private LineRenderer Lineprefab;
    private int _numRows=0;
    private int _numColumns=0;

    private Vector3 AxisScale;

    void Start()
    {
        ConstructGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConstructGrid(){

        _numRows = (int) Mathf.RoundToInt(PLANE_SIZE/unit);
        _numColumns = (int) Mathf.RoundToInt(PLANE_SIZE/unit);
        
        initializeXZPlaneGrid();
        initializeXYPlaneGrid();
        initializeYZPlaneGrid();

    }
    private void initializeXZPlaneGrid(){
        Vector3 _ori = gameObject.transform.position;
        Vector3 _xdif = new Vector3(unit, 0, 0);
        Vector3 _zdif = new Vector3(0,0,unit);
        Vector3 _posIter = new Vector3();
        for(int i = 0; i <= _numRows; ++i){
            _posIter.x = 0.0f;
            for(int j = 0; j <= _numColumns; ++j){
                
                LineRenderer localLine = Instantiate(Lineprefab, gameObject.transform);
                localLine.transform.position = _posIter;
                localLine.SetPosition(0, new Vector3());
                Vector3 _upperPosition = new Vector3(0, PLANE_SIZE, 0);
                localLine.SetPosition(1, _upperPosition);
                _posIter += _xdif;

            }
            _posIter +=_zdif;
        }

    }

    private void initializeXYPlaneGrid(){
        Vector3 _ori = gameObject.transform.position;
        Vector3 _xdif = new Vector3(unit, 0, 0);
        Vector3 _ydif = new Vector3(0,unit,0);
        Vector3 _posIter = new Vector3();
        for(int i = 0; i <= _numRows; ++i){
            _posIter.x = 0.0f;
            for(int j = 0; j <= _numColumns; ++j){
                
                LineRenderer localLine = Instantiate(Lineprefab, gameObject.transform);
                localLine.transform.position = _posIter;
                localLine.SetPosition(0, new Vector3());
                Vector3 _upperPosition = new Vector3(0, 0, PLANE_SIZE);
                localLine.SetPosition(1, _upperPosition);
                _posIter += _xdif;

            }
            _posIter +=_ydif;
        }
    }

    private void initializeYZPlaneGrid(){
        Vector3 _ori = gameObject.transform.position;
        Vector3 _ydif = new Vector3(0, unit, 0);
        Vector3 _zdif = new Vector3(0,0,unit);
        Vector3 _posIter = new Vector3();
        for(int i = 0; i <= _numRows; ++i){
            _posIter.z = 0.0f;
            for(int j = 0; j <= _numColumns; ++j){
                
                LineRenderer localLine = Instantiate(Lineprefab, gameObject.transform);
                localLine.transform.position = _posIter;
                localLine.SetPosition(0, new Vector3());
                Vector3 _upperPosition = new Vector3(PLANE_SIZE, 0, 0);
                localLine.SetPosition(1, _upperPosition);
                _posIter += _zdif;

            }
            _posIter +=_ydif;
        }
    }
}
