using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManagerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxExperimentCount = 20;
    void Start()
    {
        ExperimentAttributes.ExperimentCount = 1;
        ExperimentAttributes.MaxCount = MaxExperimentCount;
    }

}
