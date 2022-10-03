using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : MonoBehaviour
{
    static ResourcePoolManager _uniqueInstance;
    [SerializeField] Sprite[] _chars;
    [SerializeField] Sprite[] _maps;
    [SerializeField] Sprite[] _rsps;

    public static ResourcePoolManager _instance
    {
        get { return _uniqueInstance; }
    }

    void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetCharImage(int charIndex)
    {
        return _chars[charIndex - 1];
    }
    public Sprite GetMapImage(int mapIndex)
    {
        return _maps[mapIndex - 1];
    }
    public Sprite GetRSPImage(int idx)
    {
        return _rsps[idx];
    }
}
