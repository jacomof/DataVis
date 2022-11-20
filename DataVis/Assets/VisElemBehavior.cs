using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisElemBehavior : MonoBehaviour
{
    [SerializeField] private float _size;
    [SerializeField] private Color _color;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(_size, _size, _size);
        var _renderer = gameObject.GetComponent<Renderer>();
        _renderer.material.SetColor("_EmissionColor", _color);
        _renderer.material.SetColor("_BaseColor", _color);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
