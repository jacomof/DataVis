using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;



public class BarElemGroupedBehavior : MonoBehaviour
{
    [SerializeField] GameObject Label;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string _text)
    {

        var _textComp = Label.GetComponent<TextMeshPro>();
        _textComp.text = _text;

    }

    public void OnSelected(SelectEnterEventArgs _eventInfo)
    {
        Debug.Log("I've been selected.");
    }
}
