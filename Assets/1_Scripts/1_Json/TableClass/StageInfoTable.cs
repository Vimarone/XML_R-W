using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoTable : TableBase
{
    public override void Load(string strJson)
    {
        // TableBase의 _sheetData 채워넣기
        //Debug.Log(strJson);
        string[] realDatas = strJson.Split('[');
        string[] fields = realDatas[1].Split('{');
        for (int i = 1; i < fields.Length; i++)
        {
            string[] field = fields[i].Split(',');
            string tableKey = field[0].Split(':')[1].Split('"')[1];
            int len = i == fields.Length - 1 ? field.Length : field.Length - 1;
            for (int j = 0; j < len; j++)
            {
                string key = field[j].Split(':')[0].Split('"')[1];
                string data = field[j].Split(':')[1].Split('"')[1];
                string value = string.Empty;
                int n = 0;
                while (n < data.Length)
                {
                    switch (data[n])
                    {
                        case '\\':
                            n++;
                            switch (data[n])
                            {
                                case 'u':
                                    string s = data.Substring(n + 1, 4);
                                    value += (char)int.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier);
                                    n += 4;
                                    break;
                            }
                            break;
                        default:
                            value += data[n];
                            break;
                    }
                    n++;
                }
                //Debug.Log(tableKey);
                //Debug.Log(key);
                //Debug.Log(value);
                Add(tableKey, key, value);
            }
        }
    }
}
