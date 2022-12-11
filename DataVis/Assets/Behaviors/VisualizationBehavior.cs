using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class VisualizationBehavior : MonoBehaviour
{
    public float Size;
    [SerializeField] private bool _Enabled;
    public Vector3 AxisScale; 
    public static float PLANE_SIZE = 10.0f;

    public string nextScene;

    public InputAction NextExperimentAction;

    public enum VisualizationTypeEnum{
        Scatter,
        Bar,
        GroupedBar,
        GroupedBar2DVs,
        GroupedBar2DVsBoxed,
        Line
    }

    public enum ExperimentTypeEnum{

        GroupedvsGridBars,
        TwoDimensionalvsVRBars

    }

    public VisualizationTypeEnum VisualizationType;

    public ExperimentTypeEnum ExperimentType;

    [SerializeField] private bool ExperimentMode = false;
    
    public GameObject _axis;
    
    [SerializeField] GameObject _Grid;

    [SerializeField] private bool EnableGrid;
    // Start is called before the first frame update

    [SerializeField] private GameObject Player;

    [SerializeField] private GameObject ExperimentManagerObject;
    private ExperimentManagerBehavior _experimentManager;
    
    void Start()
    {
        gameObject.transform.localScale = new Vector3(Size,Size,Size);
        gameObject.SetActive(_Enabled);
        
        Debug.Log("Hello there, I'm the visualization!");
        populateElements();
        var _lineFactory = gameObject.GetComponent<LineFactory>();
        NextExperimentAction.Disable();
        if(ExperimentMode)
        {
            NextExperimentAction.performed += OnNextExperimentPressed;
            NextExperimentAction.Enable();
            _experimentManager = ExperimentManagerObject.GetComponent<ExperimentManagerBehavior>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnableGrid)
            _Grid.SetActive(true);
        else
            _Grid.SetActive(false);
    }

    void OnNextExperimentPressed(InputAction.CallbackContext ctx){

        Debug.Log("Currently loaded scene: " + SceneManager.sceneCount);
        //NextExperimentAction.performed -= OnNextExperimentPressed;
        //string _sceneName = SceneManager.GetActiveScene().name;
        //SceneManager.UnloadScene("Scenes/Experiments/BarComparison3");
        //SceneManager.UnloadSceneAsync(_sceneName);
        if(_experimentManager.ExperimentCount < _experimentManager.MaxCount)
            _experimentManager.ExperimentCount++;
        else{

            _experimentManager.ExperimentCount = 1;

        }
        restartVisualization();
        
        
    }

    private void restartVisualization()
    {
        
        if(GetComponent<LoadDataBehaviour>().IsThereExperimentData()){
            ExtraCharacterBehavior _controller = null;
            foreach(Transform child in transform)
            {

                if(child.tag == "Erasable"){

                    Object.Destroy(child.gameObject);

                }else if(child.tag == "Gazer"){

                    child.gameObject.GetComponent<Gazer>().SaveTime();
                    Object.Destroy(child.gameObject);

                }
            }
            
             
            _controller = Player.GetComponent<ExtraCharacterBehavior>();
            _controller.SetActive(false);
            _controller.RestartPosition();
            populateElements();
            _controller.SetActive(true);
        }else{
            ExtraCharacterBehavior _controller = Player.GetComponent<ExtraCharacterBehavior>();
            foreach(Transform child in transform)
            {

                if(child.tag == "Erasable"){

                    Object.Destroy(child.gameObject);

                }else if(child.tag == "Gazer"){

                    child.gameObject.GetComponent<Gazer>().SaveTime();
                    Object.Destroy(child.gameObject);

                }
            }
            _controller.SetActive(false);
            Debug.Log("Experiment Finished");
            _experimentManager.ExperimentFinished=true;
        }
    }

    private void populateElements()
    {

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
            case VisualizationTypeEnum.GroupedBar2DVsBoxed:
                var _populateBehaviorBarGrouped2DVsBoxed = gameObject.GetComponent<PopulateElementsBarBoxedContainer>();
                _populateBehaviorBarGrouped2DVsBoxed.DoPopulate();
                break;
            case VisualizationTypeEnum.Line:
                var _populateBehaviorLine = gameObject.GetComponent<PopulateElementsLine>();
                _populateBehaviorLine.DoPopulate();
                break;
            
            default:
                throw new System.Exception("No Visualization Type selected!");

        }
        var _lineFactory = gameObject.GetComponent<LineFactory>();

    }

    public string GetParticipantID(){

        return _experimentManager.ParticipantID;

    }

    public int GetExperimentCount(){

        return _experimentManager.ExperimentCount;

    }

    public string GetExperimentName(){

        return _experimentManager.ExperimentName;

    }


}
