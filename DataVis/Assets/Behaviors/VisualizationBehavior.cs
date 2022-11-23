using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VisualizationBehavior : MonoBehaviour
{
    [SerializeField] private float Size;
    [SerializeField] private bool _Enabled;
    public Vector3 AxisScale; 
    public static float PLANE_SIZE = 10.0f;

    public enum VisualizationTypeEnum{
        Scatter,
        Bar
    }

    public VisualizationTypeEnum VisualizationType;
    
    
    [SerializeField] GameObject _Grid;

    [SerializeField] private bool EnableGrid;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(Size,Size,Size);
        gameObject.SetActive(_Enabled);
        Debug.Log("Hello there, I'm the visualization!");
        var _loadData = gameObject.GetComponent<LoadDataBehaviour>();
        _loadData.LoadData();
        switch (VisualizationType)
        {
            case VisualizationTypeEnum.Scatter:
                var _populateBehaviorSca = gameObject.GetComponent<PopulateElementsScatter>();
                _populateBehaviorSca.DoPopulate();
                break;
            case VisualizationTypeEnum.Bar:
                var _populateBehaviorBar = gameObject.GetComponent<PopulateElementsBar>();
                _populateBehaviorBar.DoPopulate();
                break;

            default:
                throw new System.Exception("No Visualization Type selected!");

        }
        var _lineFactory = gameObject.GetComponent<LineFactory>();

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
