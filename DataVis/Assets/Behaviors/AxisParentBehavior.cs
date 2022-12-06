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
            
            var _initializer = _axis.GetComponent<AxisBehavior>();
            _initializer.NumberOfLabels = _labelNum[i];
            _initializer.IsXZNumeric = IsXZNumeric;
            _initializer.initializeAxis(_labelLists[i]);
            i++;

        }
    }

    public void initializeLinesOnlyY(float max, int num_labels)
    {

        var _initializer = MyChildren[1].GetComponent<AxisBehavior>();
        _initializer.NumberOfLabels = num_labels;
        MyChildren[0].SetActive(false);
        MyChildren[2].SetActive(false);
        _initializer.initializeAxis(max);

    }
}
