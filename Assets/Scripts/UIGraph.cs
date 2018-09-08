using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIGraph : MonoBehaviour, IReceiver
{

    private Button BuildChartButton;
    private Button SelectGraphicButton;
    private InputField FormulaField;
    private UIManager Manager;
    private GameObject Line;
    void Awake()
    {
        SelectGraphicButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        FormulaField = transform.GetChild(0).GetChild(1).GetComponent<InputField>();
        BuildChartButton = transform.GetChild(0).GetChild(2).GetComponent<Button>();     
        BuildChartButton.onClick.AddListener(BuildChartButton_OnClick);
        SelectGraphicButton.onClick.AddListener(SelectGraphicButton_OnClick);
        Manager = GameObject.Find("Canvas").GetComponent<UIManager>();       
        transform.SetParent(Manager.transform.Find("Panel/figures_scroll_view/Viewport/Content"));
        transform.localScale = Vector3.one;
    }

    public void BuildChartButton_OnClick()
    {
        try
        {
            string formula = FormulaField.text;
            var exp = new Function(formula);
            List<PointC> list = new List<PointC>();
            for (float i = 0; i < 360; i += 0.1f)
            {
                list.Add(new PointC(i * Mathf.Deg2Rad, exp.GetValue(i) * Mathf.Deg2Rad));
            }
            DrawGraph(list);
        }
        catch (Exception)
        {
            
        }

    }

    public void DrawGraph(List<PointC> points)
    {
        if(Line != null) Destroy(Line);
        Line = new GameObject();
        Line.transform.SetParent(GameObject.Find("RenderGraph_Calatog").transform);
        var lr = Line.AddComponent<LineRenderer>();
        lr.startWidth = 0.019f;
        lr.endWidth = 0.019f;
        lr.material.color = Color.green;
        lr.positionCount = points.Count;
        lr.SetPositions(points.Select(x=>x.Point).ToArray());
    }
    public void SelectGraphicButton_OnClick()
    {
        Manager.Receiver = this;
        var colors = SelectGraphicButton.colors;
        colors.normalColor = Color.green;
        colors.highlightedColor = Color.green;
        SelectGraphicButton.colors = colors;
    }
    public void Set(PointC a)
    {
        //nothing
    }

    public void Deselect()
    {
        var colors = SelectGraphicButton.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = Color.white;
        SelectGraphicButton.colors = colors;
    }

    public void Destroy()
    {
        Manager.Receiver = null;
        Destroy(Line);
        Destroy(gameObject);
    }
}
