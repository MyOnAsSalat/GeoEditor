using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IReceiver
{
    public GameObject FiguresContainerScrollView;
    public GameObject PrefabPoint;
    public GameObject PrefabUIPoint;
    public GameObject PrefabUIFigureImagePanel;
    public Button HideButton;
    public Button RemoveButton;
    public Button AddFigureButton;
    public Button DeselectButton;
    public float Radius = 5;
    private IReceiver receiver;
    public IReceiver Receiver
    {
        get
        {
            return receiver;
        }
        set
        {
            receiver?.Deselect();
            receiver = value;
        } 
    }
    public void ExitButton_OnClick()
    {
        Application.Quit();
    }

    public void Destroy()
    {

    }
    public void HideButton_OnClick()
    {
        var UItext = HideButton.GetComponentInChildren<Text>();
        if (UItext.text == "Свернуть >>")
        {
            UItext.text = "Развернуть <<";
            FiguresContainerScrollView.SetActive(false);
        }
        else
        {
            UItext.text = "Свернуть >>";
            FiguresContainerScrollView.SetActive(true);
        }       
    }

    public void DeselectButton_OnClick()
    {
        receiver?.Deselect();
        receiver = null;
    }
  
    public void Set(PointC p)
    {
        Receiver?.Set(p);
    }
    public void Deselect()
    {
        //nothing
    }

  
    public void RemoveButton_OnClick()
    {
        receiver.Destroy();
    }
    public void AddFigureButton_OnClick()
    {
        GameObject uiFigure = Instantiate(this.PrefabUIFigureImagePanel, Vector3.zero, Quaternion.identity);
//        uiFigure.transform.parent = transform.Find("Panel/figures_scroll_view/Viewport/Content");
        AddFigureButton.transform.SetSiblingIndex(transform.Find("Panel/figures_scroll_view/Viewport/Content").childCount - 1);
    }
    void Start ()
    {
        AddFigureButton = transform.Find("Panel/figures_scroll_view/Viewport/Content/add_figure_button").GetComponent<Button>();
        AddFigureButton.onClick.AddListener(AddFigureButton_OnClick);      
    }
    void Update ()
    {
		
	}
}
