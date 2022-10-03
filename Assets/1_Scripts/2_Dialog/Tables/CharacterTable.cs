using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTable : TableBase
{
    public override void Load(string strJson)
    {
        string realDatas = strJson.Split('[')[1];
        string[] fields = realDatas.Split('{');
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
                        case '<':
                            n++;
                            switch (data[n])
                            {
                                case '0':
                                    //string s = data.Substring(n, 1);
                                    value += "플레이어";
                                    n+=2;
                                    break;
                            }
                            break;
                        default:
                            value += data[n];
                            break;
                    }
                    n++;
                }
                Add(tableKey, key, value);
            }
        }
    }
}
