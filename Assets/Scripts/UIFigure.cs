using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFigure : MonoBehaviour, IReceiver
{
    private UIManager Manager;
    private Button SelectFigureButton;
    private Button LoopButton;
    private Button LengthLabel;
    private Button AreaLabel;
    private Button AddPointButton;
    private bool Loop = false;
    private List<UIPoint> UIPointList = new List<UIPoint>();
    private List<LineSegment> LineSegmenList = new List<LineSegment>();
    public void Set(PointC p)
    {
        GameObject uiPoint = Instantiate(Manager.PrefabUIPoint, Vector3.zero, Quaternion.identity);
        uiPoint.transform.parent = transform.Find("scroll_view/Viewport/Content");
        uiPoint.GetComponent<IReceiver>().Set(p);
        AddPointButton.transform.SetSiblingIndex(transform.Find("scroll_view/Viewport/Content").childCount-1);
    }
    public void Deselect()
    {
        var colors = SelectFigureButton.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = Color.white;
        SelectFigureButton.colors = colors;
    }
    public void SelectFigureButton_OnClick()
    {
        Manager.Receiver = this;  
        var colors = SelectFigureButton.colors;
        colors.normalColor = Color.green;
        colors.highlightedColor = Color.green;
        SelectFigureButton.colors = colors;
    }
    public void LoopButton_OnClick()
    {
        if (Loop == false)
        {
            LoopButton.transform.GetChild(0).GetComponent<Text>().text = "Разъединить";
            Loop = true;
        }
        else
        {
            LoopButton.transform.GetChild(0).GetComponent<Text>().text = "Зациклить";
            Loop = false;
        }
        Figure_OnPointChange();
    }

    private void Figure_OnPointChange()
    {
        LineSegmentDrawing();
        AngularLengthEstimating();
        return;
        int count = transform.childCount - 1;
        if (count == 0)
        {
            AreaLabel.transform.GetChild(0).GetComponent<Text>().text = "0";
            LengthLabel.transform.GetChild(0).GetComponent<Text>().text = "0";
        }
        PointC[] points = new PointC[count];
        AddPointButton.transform.SetSiblingIndex(count + 1);
        for (int i = 0; i < count-1; i++)
        {
            
            points[i] = transform.Find("scroll_view/Viewport/Content").GetChild(count - i + 1).GetComponent<UIPoint>().Point;
         //   Debug.Log(points[i].Point);
        }
        float DegreesLength = 0;
        for (int i = 0; i < count-1; i++)
        {
            DegreesLength += MathS.ArcLength(points[i], points[i + 1]);
        }
        AreaLabel.transform.GetChild(0).GetComponent<Text>().text = "0";
        if (Loop)
        {
            DegreesLength += MathS.ArcLength(points[count - 1], points[0]);
            LengthLabel.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(DegreesLength);
        }
        else
        {
            LengthLabel.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(DegreesLength);
        }

    }

    private void LineSegmentDrawing()
    {
        LineSegmenList.ForEach(x => x.Destroy());
        LineSegmenList.Clear();
        if (UIPointList.Count < 2) return;
        var buffer = UIPointList[0];
        for (int i = 1; i < UIPointList.Count; i++)
        {
            LineSegmenList.Add(new LineSegment(buffer.Point,UIPointList[i].Point));
            buffer = UIPointList[i];
        }
        if(Loop && UIPointList.Count>2) { LineSegmenList.Add(new LineSegment(UIPointList[0].Point, UIPointList[UIPointList.Count - 1].Point)); }

    }
    private void AngularLengthEstimating()
    {
        float arcLength = 0;
        LengthLabel.transform.GetChild(0).GetComponent<Text>().text = arcLength.ToString();
        if (UIPointList.Count < 2) return;
        var buffer = UIPointList[0];       
        for (int i = 1; i < UIPointList.Count; i++)
        {
            arcLength += MathS.ArcLength(buffer.Point, UIPointList[i].Point);
            buffer = UIPointList[i];
        }
        if (Loop && UIPointList.Count > 2) { arcLength += MathS.ArcLength(UIPointList[0].Point, UIPointList[UIPointList.Count-1].Point); }
        LengthLabel.transform.GetChild(0).GetComponent<Text>().text = arcLength.ToString();

    }
    public void AddPointButton_OnClick()
    {
        Set(new PointC(new Vector3(Manager.Radius,0,0),InputType.Radians));
    }

    public void UIPoint_OnChange()
    {
        Figure_OnPointChange();
    }
    void Awake ()
    {
        SelectFigureButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        LoopButton = transform.GetChild(0).GetChild(3).GetComponent<Button>();
        LengthLabel = transform.GetChild(0).GetChild(4).GetComponent<Button>();
        AreaLabel = transform.GetChild(0).GetChild(5).GetComponent<Button>();
        SelectFigureButton.onClick.AddListener(SelectFigureButton_OnClick);
        LoopButton.onClick.AddListener(LoopButton_OnClick);
        Manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        AddPointButton = transform.Find("scroll_view/Viewport/Content/add_point_button").GetComponent<Button>();
        AddPointButton.onClick.AddListener(AddPointButton_OnClick);
        transform.parent = Manager.transform.Find("Panel/figures_scroll_view/Viewport/Content");
        transform.localScale = Vector3.one;
    }

    public void AddUIPoint(UIPoint point)
    {
        if (UIPointList.Contains(point)) return;
        UIPointList.Add(point);
    }

    public void RemoveUIPoint(UIPoint point)
    {
        UIPointList.Remove(point);
    }
    public void Destroy()
    {
       
        
    }
}
