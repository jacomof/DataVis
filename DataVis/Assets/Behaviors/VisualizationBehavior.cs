using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VisualizationBehavior : MonoBehaviour
{
    [SerializeField] private float Size;
    [SerializeField] private bool _Enabled;
    public Vector3 AxisScale; 
    public static float PLANE_SIZE = 10.0f;

    [SerializeField] GameObject _Grid;

    [SerializeField] private bool EnableGrid;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(Size,Size,Size);
        gameObject.SetActive(_Enabled);
        Debug.Log("Hello there, I'm the visualization!");
        var _loadData = gameObject.GetComponent<LoadDataBehaviour>();
        var _populateBehavior = gameObject.GetComponent<PopulateElementsScatter>();
        var _lineFactory = gameObject.GetComponent<LineFactory>();

        _loadData.LoadData();
        AxisScale = _populateBehavior.DoPopulate();
        
    
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnableGrid)
            _Grid.SetActive(true);
        else
            _Grid.SetActive(false);
    }
}
