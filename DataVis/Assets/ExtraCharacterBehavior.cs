using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExtraCharacterBehavior: MonoBehaviour
{

    [SerializeField] Vector3 InitialPosition; 
    [SerializeField] Vector3 InitialAngle;
    [SerializeField] InputAction ResetAction;

    private bool _active = true;
    private bool _restartPosition = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetAction.performed += RestartPosition; //The restart position function is added to the performed callback of the Input Action
        ResetAction.Enable(); 
    }

    // Update is called once per frame
    void Update()
    {
        var _controller = gameObject.GetComponent<CharacterController>();
        if(!_active){ //If character is not active, character controller is disabled to disallow movement from the user
            _controller.enabled = false;
        }else if(_active && !_controller.enabled){
            _controller.enabled = true;
        }
        if(_restartPosition){ //If the restart position botton was pressed, disable the character temporarily and restart the position to the inititial position
            Debug.Log("Position resetted");
            Debug.Log("Character Enabled:" + _controller.enabled);
            _controller.enabled = false;
            gameObject.transform.localPosition = InitialPosition;
            gameObject.transform.localEulerAngles = InitialAngle;
            _restartPosition = false;
            _controller.enabled = true;
        }
        
        
    }
    //Public function that enables the _restartPosition flag
    public void RestartPosition(InputAction.CallbackContext ctx) 
    {

        _restartPosition = true;

    }

    public void RestartPosition()
    {

        _restartPosition = true;

    }
    //Public function to disable user character's movement externally (used by the VisualizationBehavior when changing tests)
    /*Post: Character is disabled and user can't move*/
    public void SetActive(bool _state) 
    {

        _active = _state;

    }
}
