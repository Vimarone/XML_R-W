using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TableBase
{
    // 시트 정보를 가지고 있는 역할
    Dictionary<string, Dictionary<string, string>> _sheetData = new Dictionary<string, Dictionary<string, string>>();

    public int _recordCount
    {
        get { return _sheetData.Count; }
    }

    protected void Add(string mainKey, string subKey, string val)
    {
        // 중복된 mainKey가 있는가
        if (_sheetData.ContainsKey(mainKey))
        {
            Dictionary<string, string> indexData = new Dictionary<string, string>();
            Dictionary<string, string> record = _sheetData[mainKey];
            foreach (string column in record.Keys)
                indexData.Add(column, record[column]);

            // 중복된 subKey가 있다면 해당 subKey 삭제 후 다시 추가
            if (indexData.ContainsKey(subKey))
            {
                indexData.Remove(subKey);
                indexData.Add(subKey, val);
            }
            else
                indexData.Add(subKey, val);

            _sheetData.Remove(mainKey);
            _sheetData.Add(mainKey, indexData);
        }
        else
        {
            Dictionary<string, string> tempData = new Dictionary<string, string>();
            tempData.Add(subKey, val);
            _sheetData.Add(mainKey, tempData);
        }
    }
    
    // 무조건 만들어야하는 함수
    public abstract void Load(string strJson);

    // 범용성이 넓은 함수일 경우 다양하게 생각해야함
    public string ToStr(string mainKey, string subKey)
    {
        return _sheetData[mainKey][subKey];
    }
    public int ToInt32(string mainKey, string subKey)
    {
        int res = 0;
        if (int.TryParse(_sheetData[mainKey][subKey], out res))
            return res;
        return res;
    }
    public float ToFloat(string mainKey, string subKey)
    {
        float res = 0f;
        if (float.TryParse(_sheetData[mainKey][subKey], out res))
            return res;
        return res;
    }
    public string ToStr(int mainKey, string subKey)
    {
        return _sheetData[mainKey.ToString()][subKey];
    }
    public int ToInt32(int mainKey, string subKey)
    {
        int res = 0;
        if (int.TryParse(_sheetData[mainKey.ToString()][subKey], out res))
            return res;
        return res;
    }
    public float ToFloat(int mainKey, string subKey)
    {
        float res = 0f;
        if (float.TryParse(_sheetData[mainKey.ToString()][subKey], out res))
            return res;
        return res;
    }

}
