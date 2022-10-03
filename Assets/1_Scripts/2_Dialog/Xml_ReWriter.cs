using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

public class Xml_ReWriter : MonoBehaviour
{
    public struct stUserInfo
    {
        public int _episodeIndex;
        public int _stageIndex;

        public stUserInfo(int e, int s)
        {
            _episodeIndex = e;
            _stageIndex = s;
        }
    }

    XmlDocument _xmlFile;

    public Xml_ReWriter() { _xmlFile = new XmlDocument(); }

    #region [내부 사용 함수]
    void SaveFile(string fullPath, stUserInfo indices)
    {
        // 파일의 형식 지정 [파일]
        _xmlFile.AppendChild(_xmlFile.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // root node 생성 [파일 - 시트]
        XmlNode rootNode = _xmlFile.CreateNode(XmlNodeType.Element, "UserInfo", string.Empty);
        _xmlFile.AppendChild(rootNode);

        // child node 생성 [파일 - 시트 - 필드(유저 네임, 게임 캐시, 현금)]
        XmlNode childNode = _xmlFile.CreateNode(XmlNodeType.Element, "Record", string.Empty);
        rootNode.AppendChild(childNode);

        // Data를 넣는 부분
        XmlElement xepiIndex = _xmlFile.CreateElement("epiIndex");
        xepiIndex.InnerText = indices._episodeIndex.ToString();
        childNode.AppendChild(xepiIndex);
        XmlElement xestIndex = _xmlFile.CreateElement("stIndex");
        xestIndex.InnerText = indices._episodeIndex.ToString();
        childNode.AppendChild(xestIndex);

        _xmlFile.Save(fullPath);
    }
    void LoadFile(string fullPath, out stUserInfo outInfo)
    {
        outInfo = new stUserInfo();
        _xmlFile.Load(fullPath);

        XmlNodeList nList = _xmlFile.SelectNodes("/UserInfo/Record");

        foreach (XmlNode element in nList)
        {
            outInfo._episodeIndex = int.Parse(element["epiIndex"].InnerText);
            outInfo._stageIndex = int.Parse(element["stIndex"].InnerText);
        }
    }
    #endregion [내부 사용 함수]

    #region [외부 사용 함수]
    public bool xmlWriteFile(string fullPath, stUserInfo sourceInfo)
    {
        try
        {
            SaveFile(fullPath, sourceInfo);
        }
        catch (FileNotFoundException except)
        {
            Debug.Log(except.Message);
            return false;
        }
        return true;
    }

    public bool xmlReadFile(string fullPath, out stUserInfo outInfo)
    {
        outInfo = new stUserInfo();
        try
        {
            LoadFile(fullPath, out outInfo);
        }
        catch (FileNotFoundException except)
        {
            Debug.Log(except.Message);
            return false;
        }
        return true;
    }
    #endregion
}