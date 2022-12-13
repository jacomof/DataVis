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
        //This lerp will fade the color of the object
        if (_renderer.material.HasProperty(_baseColor)) // new rendering pipeline (lightweight, hd, universal...)
        {
            _renderer.material.SetColor(_baseColor, Color.Lerp(_renderer.material.GetColor(_baseColor), _targetColor, Time.deltaTime * (1 / animationTime)));
        }
        else // old standard rendering pipline
        {
            _renderer.material.color = Color.Lerp(_renderer.material.color, _targetColor, Time.deltaTime * (1 / animationTime));
        }
        if (Input.GetKeyDown(KeyCode.G)) Debug.Log("Timer is: " + Timer);

        //

        if(Input.GetKeyDown(KeyCode.T) && !timerGoing){
            //Debug.Log("Time is " + Timer);
            InvokeRepeating("IncrementTimer", _timerStep, _timerStep);
            timerGoing = true;
        }else if(Input.GetKeyDown(KeyCode.T)){

            Debug.Log("Time is " + Timer);
            //Debug.Log("Timer stopped!");
            CancelInvoke("IncrementTimer");
            timerGoing = false;

        }
        if (timerGoing) Timer += Time.deltaTime;
        
    }
    
    private void IncrementTimer()
    {

        Timer+=_timerStep;

    }

    public void SaveTime(){

        Debug.Log("Saving Time!!");
        System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        StreamWriter _timeFile =  File.AppendText("Assets/Data/GazeTimeData/" + _visualizationBehavior.GetExperimentName() + "TimeFile.csv");
        _timeFile.WriteLineAsync(_visualizationBehavior.GetParticipantID() + ", " + _visualizationBehavior.GetExperimentCount().ToString() + ", " + Timer.ToString("F5"));
        _timeFile.Close();

    }
}
