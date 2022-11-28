using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Data.Analysis;
using System.Linq;
using Microsoft.ML;
using static VisualizationBehavior;

public class PopulateElementsPie : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3[] _valueVectors;
    private float _scaleSize;
    public Vector3 AxisScale{get; private set;}
    [SerializeField] private float Margin = 0.0f;
    [SerializeField] GameObject VisualizationElement;
    [SerializeField] LineRenderer VisualizationLine;

    void Start(){
        MeshFilter _myMesh = GetComponent<MeshFilter>();
        _myMesh.mesh = _constructMesh();
    }
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
        var _data = loadData.LoadPie();
        return AxisScale;
    }

    private Mesh _constructMesh()
    {

        Mesh _piePiece = new Mesh();

        Vector3[] _vertices = new Vector3[4];
        
        _vertices[0] = new Vector3(0,0,0);
        _vertices[1] = new Vector3(0,1f,0);
        _vertices[2] = new Vector3(1f,0,0);
        _vertices[3] = new Vector3(1f,1f,0);

        int[] _triangles = new int[]{0,1,2,1,3,2};
        _piePiece.vertices = _vertices;
        _piePiece.triangles = _triangles;

        return _piePiece;

    }
}