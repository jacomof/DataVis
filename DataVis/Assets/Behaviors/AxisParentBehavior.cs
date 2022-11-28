using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AxisParentBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public int NumberOfXLabels = 10;
    public int NumberOfYLabels = 10;
    public int NumberOfZLabels = 10;
    public GameObject[] MyChildren;
    public bool IsXZNumeric = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initializeLines (System.Object[] _labelLists)
    {
        
        int[] _labelNum = new int[]{NumberOfXLabels,NumberOfYLabels,NumberOfZLabels};
        int i = 0;

        //Order of initialization: X, Y, Z
        foreach(var _axis in MyChildren){
            
            var initializer = _axis.GetComponent<AxisBehavior>();
            initializer.NumberOfLabels = _labelNum[i];
            initializer.IsXZNumeric = IsXZNumeric;
            initializer.initializeAxis(_labelLists[i]);
            i++;

        }
    }
}
