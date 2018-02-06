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

    public void Figure_OnPointChange()
    {
        int count = transform.childCount - 1;
        if (count == 0)
        {
            AreaLabel.transform.GetChild(0).GetComponent<Text>().text = "0";
            LengthLabel.transform.GetChild(0).GetComponent<Text>().text = "0";
        }
        PointC[] points = new PointC[count];
        for (int i =0;i < count; i++)
        {
          points[i] = transform.GetChild(i).GetComponent<UIPoint>().Point;
        }
        float DegreesLength = 0;
        for (int i = 0; i < count; i++)
        {
          DegreesLength +=  MathS.ArcLength(points[i], points[i + 1]);
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
    }

    public void Destroy()
    {
       
        
    }
}
