using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterFlyingScript : MonoBehaviour
{
    [SerializeField] private float _flyingSpeed = 2.0f;

    private float _yVelocity = 0.0f;

    [SerializeField] private InputAction AscendButton;
    [SerializeField] private InputAction DescendButton;
    private CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        AscendButton.Enable();
        DescendButton.Enable();//Enable the input actions used to detect when the user wants to fly up or down 
    }

    // Update is called once per frame
    void Update()
    {

        if(AscendButton.IsPressed()){
            _yVelocity=_flyingSpeed; //If AscendButton is being pressed, apply velocity positively
        }
        if(DescendButton.IsPressed())
            _yVelocity= -_flyingSpeed; //If DescendButton is being pressed, apply velocity but negatively
        
        if(_controller.isGrounded && _yVelocity < 0) 
            _yVelocity = 0; //If velocity is negative and the User is in contact with the floor, set velocity to 0
        var _vectorVelocity = (new Vector3(0,1,0))*_yVelocity; //Calculate velocity vector
        if(_controller.enabled) _controller.Move(_vectorVelocity * Time.deltaTime); //If Character Controller is enabled, move the character using the calculated velocity
        _yVelocity = 0;
        
    }

}
