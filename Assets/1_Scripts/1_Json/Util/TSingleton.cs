using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    static volatile T _uniqueInstance = null;
    static volatile GameObject _uniqueObject = null;

    protected TSingleton()
    {

    }

    // 얘를 상속받는 클래스는 고유 클래스가 되는 것
    // 기반 클래스 전용
    // _instance 최초 실행 = 메모리 상에서 생성되는 것
    public static T _instance
    {
        get
        {
            if (_uniqueInstance == null)
            {
                // lock : 메모리 상에서 락을 걸어놓은 것
                lock (typeof(T))
                {
                    if (_uniqueInstance == null && _uniqueObject == null)
                    {
                        _uniqueObject = new GameObject(typeof(T).Name, typeof(T));
                        _uniqueInstance = _uniqueObject.GetComponent<T>();

                        _uniqueInstance.Init();
                    }
                }
            }
            return _uniqueInstance;
        }
    }

    protected virtual void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
}
