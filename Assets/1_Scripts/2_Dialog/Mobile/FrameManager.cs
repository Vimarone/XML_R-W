using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameManager : MonoBehaviour
{
    float _deltaTime = 0;
    GUIStyle _style;
    Rect _rect;
    float _mSec;
    float _fps;
    float _worstFps = 100;
    string _text;

    void Awake()
    {
        Application.targetFrameRate = 60;
        InitFpsData();
        StartCoroutine("WorstReset");
    }

    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
    }

    void InitFpsData()
    {
        int sW = Screen.width;
        int sH = Screen.height;
        _rect = new Rect(0, 20, sW, sH * 4 / 100);
        _style = new GUIStyle();
        _style.alignment = TextAnchor.UpperLeft;
        _style.fontSize = sH * 4 / 100;
        _style.normal.textColor = Color.cyan;
    }
    
    IEnumerator WorstReset()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            _worstFps = 100;
        }
    }

    void OnGUI()
    {
        _mSec = _deltaTime * 1000;
        _fps = 1 / _deltaTime;
        if (_fps < _worstFps)
            _worstFps = _fps;
        _text = string.Format("{0:F1}ms ({1:F1}) + worst : {2:F1}", _mSec, _fps, _worstFps);
        GUI.Label(_rect, _text, _style);
    }
}
