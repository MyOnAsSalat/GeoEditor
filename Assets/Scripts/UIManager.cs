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
    public GameObject PrefabUIGraphicImagePanel;
    public Button HideButton;
    public Button RemoveButton;
    public Button AddFigureButton;
    public Button AddGraphicButton;
    public Button DeselectButton;
    public Button GridButton;
    public Button SpereMeshButton;
    public GameObject GeoCatalog;
    public MeshRenderer SphereMesh;


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

    private bool GeoCatalogActive = true;
    public void GridButton_OnClick()
    {
        if (GeoCatalog == null) {GeoCatalog = GameObject.Find("GeoCatalog_1"); }
        if (GeoCatalogActive)
        {
            GridButton.GetComponentInChildren<Text>().text = "Включить сетку";
            GeoCatalogActive = false;
            GeoCatalog.SetActive(false);
        }
        else
        {
            GridButton.GetComponentInChildren<Text>().text = "Отключить сетку";
            GeoCatalogActive = true;
            GeoCatalog.SetActive(true);
        }
    }

    public void SphereMeshButton_OnClick()
    {
        if (SphereMesh.enabled)
        {
            SpereMeshButton.GetComponentInChildren<Text>().text = "Включить сферу";
            SphereMesh.enabled = false;
        }
        else
        {
            SpereMeshButton.GetComponentInChildren<Text>().text = "Отключить сферу";
            SphereMesh.enabled = true;
        }
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
        receiver?.Destroy();
    }
    public void AddFigureButton_OnClick()
    {
        GameObject uiFigure = Instantiate(PrefabUIFigureImagePanel, Vector3.zero, Quaternion.identity);
     //   AddFigureButton.transform.SetSiblingIndex(transform.Find("Panel/figures_scroll_view/Viewport/Content").childCount - 1);
    }
    public void AddGraphicButton_OnClick()
    {
     //   Debug.Log("test");
        GameObject uiGraphic = Instantiate(PrefabUIGraphicImagePanel, Vector3.zero, Quaternion.identity);
        
    }
    void Start ()
    {
        AddFigureButton = transform.Find("Panel/main_menu_image_panel/add_figure_button").GetComponent<Button>();
        AddFigureButton.onClick.AddListener(AddFigureButton_OnClick);
        AddGraphicButton.onClick.AddListener(AddGraphicButton_OnClick);
    }

    void Awake()
    {
        SphereMesh = GameObject.Find("Sphere").GetComponent<MeshRenderer>();
    }
    void Update ()
    {
		
	}
}
