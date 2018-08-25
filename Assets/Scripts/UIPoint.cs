using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
public class UIPoint : MonoBehaviour, IReceiver
{
    private PointC MathPoint;
    public PointC Point => MathPoint; 
    private UIManager Manager;
    private UIFigure Figure; 
    private GameObject RenderPoint;
    private Button SelectPointButton;
    private Button SetValuesButton;
    private InputField ThetaField;
    private InputField PhiField;

    void Awake()
    {
        SelectPointButton = transform.GetChild(0).GetComponent<Button>();
        SetValuesButton = transform.GetChild(3).GetComponent<Button>();
        ThetaField = transform.GetChild(4).GetComponent<InputField>();
        PhiField = transform.GetChild(5).GetComponent<InputField>();
        SetValuesButton.onClick.AddListener(SetValuesButton_OnClick);
        SelectPointButton.onClick.AddListener(SelectPointButton_OnClick);
        MathPoint = new PointC(new Vector3(5, 0, 0), InputType.Degrees);
        Manager = GameObject.Find("Canvas").GetComponent<UIManager>(); 
        RenderPoint = Instantiate(Manager.PrefabPoint, MathPoint.Point, Quaternion.identity);
        RenderPoint.transform.parent = transform;     
    }
    public void Set(PointC p)
    {
        if (RenderPoint != null)  Destroy(RenderPoint);        
        MathPoint = p;
        RenderPoint = Instantiate(Manager.PrefabPoint, MathPoint.Point, Quaternion.identity);
        RenderPoint.name = "render_point";
        ThetaField.text = Convert.ToString(p.ThetaDeg).Replace(",",".");
        PhiField.text = Convert.ToString(p.PhiDeg).Replace(",", ".");
        Figure = transform.parent.parent.parent.parent.GetComponent<UIFigure>();
        transform.localScale = Vector3.one;
        Figure.AddUIPoint(this);
        Figure.UIPoint_OnChange();
    }
    public void Deselect()
    {
        if (SelectPointButton == null) return;
        var colors = SelectPointButton.colors;
        colors.normalColor = Color.white;
        colors.highlightedColor = Color.white;
        SelectPointButton.colors = colors;
    }

    public void SelectPointButton_OnClick()
    {
        Manager.Receiver = this;
        var colors = SelectPointButton.colors;
        colors.normalColor = Color.green;
        colors.highlightedColor = Color.green;
        SelectPointButton.colors = colors;
    }
    public void SetValuesButton_OnClick()
    {
        float theta;
        float phi;
        if (float.TryParse(ThetaField.text.Replace(".",","), out theta) && float.TryParse(PhiField.text.Replace(".", ","), out phi))
        {
            MathPoint = new PointC(new Vector3(Manager.Radius, theta, phi), InputType.Degrees);
            Set(MathPoint);
        }     
        Figure.UIPoint_OnChange();
    }
    public void Destroy()
    {       
        if (RenderPoint != null)  Destroy(RenderPoint);   
        Destroy(gameObject);
        Figure.RemoveUIPoint(this);
        Figure.UIPoint_OnChange();
    }
}
