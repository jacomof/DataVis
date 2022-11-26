using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AxisParentBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] MyChildren;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeLines (List<string>[] _labelLists)
    {
        int i = 0;
        foreach(var _axis in MyChildren){

            var initializer = _axis.GetComponent<AxisBehavior>();
            initializer.initializeAxis(_labelLists[i]);
            i++;

        }
    }
}
