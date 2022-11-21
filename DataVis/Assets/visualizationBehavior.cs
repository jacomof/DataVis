using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class visualizationBehavior : MonoBehaviour
{
    [SerializeField] private float Size;
    [SerializeField] private bool _Enabled;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(Size,Size,Size);
        gameObject.SetActive(_Enabled);
        Debug.Log("Hello there, I'm the visualization!");
        var loadData = gameObject.GetComponent<loadDataBehaviour>();
        var populateBehavior = gameObject.GetComponent<PopulateElementsBehavior>();

        loadData.loadData();
        populateBehavior.DoPopulate();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
