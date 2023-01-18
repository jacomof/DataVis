using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using System.IO;


public class Gazer : MonoBehaviour, IGazeFocusable
{
    private static readonly int _baseColor = Shader.PropertyToID("_BaseColor");
    public Color highlightColor = Color.red;
    public float animationTime = 0.1f;

    private Renderer _renderer;
    private Color _originalColor;
    private Color _targetColor;

    public float Timer = 0;
    private float _timerStep = 0.001f;

    private bool timerGoing = false;
    public VisualizationBehavior _visualizationBehavior;

    
    

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        //If this object received focus, fade the object's color to highlight color and start timer again
        if (hasFocus)
        {
            if(!timerGoing)
            {
                Debug.Log("Starting timer with time " + Timer);
                timerGoing = true;
            }
            _targetColor = highlightColor;
        }
        //If this object lost focus, fade the object's color to it's original color and pause timer
        else
        {
            if(timerGoing){
                timerGoing = false;
                Debug.Log("Stopping timer with time " + Timer);
            }
            _targetColor = _originalColor;
        }
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;
        _targetColor = _originalColor;
    }

    private void Update()
    {
        /*Debug Code: changes the detector's color when it is being looked at*/
        //This lerp will fade the color of the object
        if (_renderer.material.HasProperty(_baseColor)) // new rendering pipeline (lightweight, hd, universal...)
        {
            _renderer.material.SetColor(_baseColor, Color.Lerp(_renderer.material.GetColor(_baseColor), _targetColor, Time.deltaTime * (1 / animationTime)));
        }
        else // old standard rendering pipline
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / animationTime));
        }
        /*End Debug Code*/
        
        if (timerGoing) Timer += Time.deltaTime;
        
    }
    
    private void IncrementTimer()
    {

        Timer+=_timerStep;

    }

    //Public routine used to save the time measured by the detector in a CSV file
    public void SaveTime(){

        Debug.Log("Saving Time!!");
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci; //Use US locale to write decimal places of numbers using english notation
        StreamWriter _timeFile =  File.AppendText("Assets/Data/GazeTimeData/" + _visualizationBehavior.GetExperimentName() + "TimeFile.csv"); //Open file in append mode
        _timeFile.WriteLineAsync(_visualizationBehavior.GetParticipantID() + ", " +
        _visualizationBehavior.GetExperimentCount().ToString() + ", "
         + gameObject.name + ", " + Timer.ToString("F5")); //Write measurement to file using participant and test information in a single row 
        _timeFile.Close(); //Close file so other detectors can write on it
        Timer=0.0f; //Restart timer for next test

    }
}
