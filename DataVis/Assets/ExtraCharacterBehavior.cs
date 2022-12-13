using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExtraCharacterBehavior : MonoBehaviour
{

    [SerializeField] Vector3 InitialPosition; 
    [SerializeField] Vector3 InitialAngle;
    [SerializeField] InputAction ResetAction;

    private bool _active = true;
    private bool _restartPosition = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetAction.performed += RestartPosition;
        ResetAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var _controller = gameObject.GetComponent<CharacterController>();
        if(!_active){
            _controller.enabled = false;
        }else if(_active && !_controller.enabled){
            _controller.enabled = true;
        }
        if(_restartPosition){
            Debug.Log("Position resetted");
            Debug.Log("Character Enabled:" + _controller.enabled);
            _controller.enabled = false;
            gameObject.transform.localPosition = InitialPosition;
            gameObject.transform.localEulerAngles = InitialAngle;
            _restartPosition = false;
            _controller.enabled = true;
        }
        
        
    }

    public void RestartPosition(InputAction.CallbackContext ctx)
    {

        _restartPosition = true;

    }

    public void RestartPosition()
    {

        _restartPosition = true;

    }

    public void SetActive(bool _state)
    {

        _active = _state;

    }
}
