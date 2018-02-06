using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IReceiver
{
    public GameObject FigureScrollView;
    public GameObject PrefabPoint;
    public GameObject PrefabUIPoint;
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
            FigureScrollView.SetActive(false);
        }
        else
        {
            UItext.text = "Свернуть >>";
            FigureScrollView.SetActive(true);
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
       
    }
    void Start ()
    {
        AddFigureButton = transform.Find("Panel/figures_scroll_view/Viewport/Content/add_figure_button").GetComponent<Button>();
        AddFigureButton.onClick.AddListener(AddFigureButton_OnClick);      
    }
    void Update () {
		
	}
}
