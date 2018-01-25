using UnityEngine;

class GeoLines : MonoBehaviour
{
    static int GeoCount = 0;
    int MeredianLineCount = 0;
    int ParralelLineCount = 0;
    float LineWidth = 0.005f;
    float R = 5;
    float X, Y, Z;
    private const float rotate = 0.57f + Mathf.PI/2;
    public GeoLines(int meredianCount = 24, int parralelsCount = 17, float r = 5, float x = 0, float y = 0, float z = 0, float linewWith = 0.010f)
    {
        GeoCount++;
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
            GameObject line;
            MeredianLineCount++;
            line = new GameObject(GeoCount + "_MeredianLine_" + MeredianLineCount);
            line.AddComponent<LineRenderer>();
            line.GetComponent<LineRenderer>().startWidth = (i == 0) ? LineWidth * 2 : LineWidth;
            line.GetComponent<LineRenderer>().endWidth = (i == 0) ? LineWidth * 2 : LineWidth;
            line.GetComponent<LineRenderer>().material.color = (i == 0)? Color.green: Color.gray;
            line.GetComponent<LineRenderer>().positionCount = 160;
            for (int fi = 0; fi < 160; fi++)
            {
                float x = R * Mathf.Cos(fi / 25f) * Mathf.Cos(i * Mathf.PI / count + 1 + rotate) + X;
                float z = R * Mathf.Cos(fi / 25f) * Mathf.Sin(i * Mathf.PI / count + 1 + rotate) + Y;
                float y = R * Mathf.Sin(fi / 25f) + Z;
                line.GetComponent<LineRenderer>().SetPosition(fi, new Vector3(x, y, z));
            }
        }
    }
    void SetParralel(int count)
    {
        if (count == 2) return;
        for (int i = 0; i < count; i++)
        {
            if (i == 0 || i == count - 1) continue;
            ParralelLineCount++;
            GameObject line;
            line = new GameObject(GeoCount + "_ParralelLine_" + ParralelLineCount);
            line.AddComponent<LineRenderer>();
            line.GetComponent<LineRenderer>().startWidth = LineWidth;
            line.GetComponent<LineRenderer>().endWidth = LineWidth;
            line.GetComponent<LineRenderer>().material.color = Color.gray;
            line.GetComponent<LineRenderer>().positionCount = 160;
            for (int fi = 0; fi < 160; fi++)
            {
                float parralelLength = R * 2 / (count - 1) * (i) - R;
                float parralelRadius = Mathf.Sqrt(Mathf.Pow(R, 2f) - Mathf.Pow(parralelLength, 2f));
                float x = parralelRadius * Mathf.Cos(fi / 25f) * Mathf.Cos(Mathf.PI) + X;
                float z = parralelRadius * Mathf.Sin(fi / 25f) + Z;
                float y = parralelLength;
                line.GetComponent<LineRenderer>().SetPosition(fi, new Vector3(x, y, z));
            }
        }
    }
    public void Release()
    {
        DestroyLines();
    }
    void DestroyLines()
    {
        for (int i = 0; i <= MeredianLineCount; i++) Destroy(GameObject.Find(GeoCount + "_MeredianLine_" + i));
        for (int i = 0; i <= ParralelLineCount; i++) Destroy(GameObject.Find(GeoCount + "_ParralelLine_" + i));
    }
}
