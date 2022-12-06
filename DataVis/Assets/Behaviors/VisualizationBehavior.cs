using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExperimentAttributes{

    public static int ExperimentCount = 1;
    public static int MaxCount = 24;
}
public class VisualizationBehavior : MonoBehaviour
{
    public float Size;
    [SerializeField] private bool _Enabled;
    public Vector3 AxisScale; 
    public static float PLANE_SIZE = 10.0f;

    public string nextScene;

    public InputAction NextSceneAction;

    public enum VisualizationTypeEnum{
        Scatter,
        Bar,
        GroupedBar,
        GroupedBar2DVs,
        Line
    }

    public enum ExperimentTypeEnum{

        GroupedvsGridBars,
        TwoDimensionalvsVRBars

    }

    public VisualizationTypeEnum VisualizationType;

    public ExperimentTypeEnum ExperimentType;
    
    public GameObject _axis;
    
    [SerializeField] GameObject _Grid;

    [SerializeField] private bool EnableGrid;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(Size,Size,Size);
        gameObject.SetActive(_Enabled);
        TextAsset _experimentFile;
        if(ExperimentType == ExperimentTypeEnum.TwoDimensionalvsVRBars)
            _experimentFile = Resources.Load<TextAsset>("Data/2DvsVR/2Dv3DBarComparison" + ExperimentAttributes.ExperimentCount.ToString());
        else
            _experimentFile = Resources.Load<TextAsset>("Data/GridvsGrouped/GroupedvsGridBarComparison" + ExperimentAttributes.ExperimentCount.ToString());
        var _loadBehavior = GetComponent<LoadDataBehaviour>();
        _loadBehavior.dataAsset = _experimentFile; 
        Debug.Log("Hello there, I'm the visualization!");
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
            case VisualizationTypeEnum.GroupedBar:
                var _populateBehaviorBarGrouped = gameObject.GetComponent<PopulateElementsBarGrouped>();
                _populateBehaviorBarGrouped.DoPopulate();
                break;
             case VisualizationTypeEnum.GroupedBar2DVs:
                var _populateBehaviorBarGrouped2DVs = gameObject.GetComponent<PopulateElementsBar2DVs>();
                _populateBehaviorBarGrouped2DVs.DoPopulate();
                break;
            case VisualizationTypeEnum.Line:
                var _populateBehaviorLine = gameObject.GetComponent<PopulateElementsLine>();
                _populateBehaviorLine.DoPopulate();
                break;
            
            default:
                throw new System.Exception("No Visualization Type selected!");

        }
        var _lineFactory = gameObject.GetComponent<LineFactory>();
        NextSceneAction.Disable();
        NextSceneAction.performed += OnNextScenePressed;
        NextSceneAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(EnableGrid)
            _Grid.SetActive(true);
        else
            _Grid.SetActive(false);
    }

    void OnNextScenePressed(InputAction.CallbackContext ctx){

        Debug.Log("Currently loaded scene: " + SceneManager.sceneCount);
        NextSceneAction.performed -= OnNextScenePressed;
        //string _sceneName = SceneManager.GetActiveScene().name;
        //SceneManager.UnloadScene("Scenes/Experiments/BarComparison3");
        //SceneManager.UnloadSceneAsync(_sceneName);
        if(ExperimentAttributes.ExperimentCount < ExperimentAttributes.MaxCount)
            ExperimentAttributes.ExperimentCount++;
        else{

            ExperimentAttributes.ExperimentCount = 1;

        }
        if(ExperimentType == ExperimentTypeEnum.TwoDimensionalvsVRBars)

            SceneManager.LoadScene("Scenes/Experiments/BarComparison" + ExperimentAttributes.ExperimentCount, LoadSceneMode.Single);
        
        else{

            SceneManager.LoadScene("Scenes/Experiments/GroupedvsGridBarComparison" + ExperimentAttributes.ExperimentCount, LoadSceneMode.Single);

        }
        
    }
}
