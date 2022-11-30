using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class BarElemBehavior : MonoBehaviour
{
    private Color _color;
    private Vector3 _size;
    [SerializeField] UnityEvent myEvent;
    // Start is called before the first frame update

    public void SetColor(Color _col)
    {
        var _renderer = gameObject.GetComponent<Renderer>();
        _renderer.material.SetColor("_BaseColor", _color);

    }

    public Color GetColor()
    {
        return _color;
    }

    public void SetSize(Vector3 _inSize)
    {

        _size = _inSize;
        gameObject.transform.localScale = _size;

    }

    public Vector3 GetSize()
    {

        return _size;

    }

    public void OnSelected(SelectEnterEventArgs _eventInfo)
    {
        Debug.Log("I've been selected.");
    }
}