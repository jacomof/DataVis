using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterFlyingScript : MonoBehaviour
{
    [SerializeField] private float _gravity = -9.8f;
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
        DescendButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        if(AscendButton.IsPressed()){
            _yVelocity=_flyingSpeed;
            //Debug.Log("Button is pressed!");
        }
        if(DescendButton.IsPressed())
            _yVelocity= -_flyingSpeed;
        
        if(_controller.isGrounded && _yVelocity < 0) 
            _yVelocity = 0;
        var _vectorVelocity = (new Vector3(0,1,0))*_yVelocity;
        if(_controller.enabled) _controller.Move(_vectorVelocity * Time.deltaTime);
        _yVelocity = 0;
        
    }

}
