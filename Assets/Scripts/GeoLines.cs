using System;
using System.Collections.Generic;
using UnityEngine;
using Camera = UnityEngine.Camera;

public class GeoLines 
{
    static int GeoCount = 0;
    int MeredianLineCount = 0;
    int ParralelLineCount = 0;
    float LineWidth = 0.005f;
    float R = 5;
    float X, Y, Z;
    private GameObject[] Meredian;
    private GameObject[] Parralel;
    private GameObject GeoCatalog;
    public GameObject plane;
    private const float rotate = 0.57f + Mathf.PI/2;

    public GeoLines(int meredianCount = 24, int parralelsCount = 17, float r = 5, float x = 0, float y = 0, float z = 0, float linewWith = 0.010f)
    {
        Meredian = new GameObject[meredianCount];
        Parralel = new GameObject[parralelsCount-2];
        GeoCount++;
        GeoCatalog = new GameObject();       
        GeoCatalog.name = "GeoCatalog_" + GeoCount;
        R = r;
        X = x;
        Y = y;
        Z = z;
        LineWidth = linewWith;
        SetMeredian(meredianCount);
        SetParralel(parralelsCount);
    }
   

    void SetMeredian(int count)
    {
        for (int i = 0; i < count; i++)
        {           
            Meredian[MeredianLineCount] = new GameObject("MeredianLine_" + (MeredianLineCount+1));
            Meredian[MeredianLineCount].transform.parent = GeoCatalog.transform;
            var renderer = Meredian[MeredianLineCount].AddComponent<LineRenderer>();
            MeredianLineCount++;
            renderer.startWidth = (i == 0) ? LineWidth * 2 : LineWidth;
            renderer.endWidth = (i == 0) ? LineWidth * 2 : LineWidth;
            renderer.material.color = (i == 0)? Color.blue: Color.gray;
            renderer.positionCount = 160;
            for (int fi = 0; fi < 160; fi++)
            {
                float x = R * Mathf.Cos(fi / 25f) * Mathf.Cos(i * Mathf.PI / count + 1 + rotate) + X;
                float z = R * Mathf.Cos(fi / 25f) * Mathf.Sin(i * Mathf.PI / count + 1 + rotate) + Y;
                float y = R * Mathf.Sin(fi / 25f) + Z;
                renderer.SetPosition(fi, new Vector3(x, y, z));
            }
        }
    }
    void SetParralel(int count)
    {
        if (count == 2 || count == 1) return;
        for (int i = 0; i < count; i++)
        {
            if (i == 0 || i == count - 1) continue;                 
            Parralel[ParralelLineCount] = new GameObject("ParralelLine_" + (ParralelLineCount+1));
            Parralel[ParralelLineCount].transform.parent = GeoCatalog.transform;
            var renderer = Parralel[ParralelLineCount].AddComponent<LineRenderer>();
            ParralelLineCount++;
            renderer.startWidth = LineWidth;
            renderer.endWidth = LineWidth;
            renderer.material.color = Color.gray;
            renderer.positionCount = 160;
            for (int fi = 0; fi < 160; fi++)
            {
                float parralelLength = R * 2 / (count - 1) * (i) - R;
                float parralelRadius = Mathf.Sqrt(Mathf.Pow(R, 2f) - Mathf.Pow(parralelLength, 2f));
                float x = parralelRadius * Mathf.Cos(fi / 25f) * Mathf.Cos(Mathf.PI) + X;
                float z = parralelRadius * Mathf.Sin(fi / 25f) + Z;
                float y = parralelLength;
                renderer.GetComponent<LineRenderer>().SetPosition(fi, new Vector3(x, y, z));
            }
        }
    }
    public void Release()
    {
        DestroyLines();
    }
    void DestroyLines()
    {
        foreach (var o in Parralel)
        {
            GameObject.Destroy(o);
        }
        foreach (var o in Meredian)
        {
            GameObject.Destroy(o);  
        }
        GameObject.Destroy(GeoCatalog);
    }
}


