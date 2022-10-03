using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefineEnum;

public class RSPBot : MonoBehaviour
{
    Sprite _rsp;
    float _timer = 0;
    float _change = 0.25f;
    int _rspIdx = 0;

    void Awake()
    {
        _rsp = GetComponent<Image>().sprite;
        _rsp = ResourcePoolManager._instance.GetRSPImage(_rspIdx);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _change)
        {
            _timer = 0;
            if (_rspIdx++ >= (int)RSP.max)
                _rspIdx = 0;
            _rsp = ResourcePoolManager._instance.GetRSPImage(_rspIdx);
        }
    }

    
}
