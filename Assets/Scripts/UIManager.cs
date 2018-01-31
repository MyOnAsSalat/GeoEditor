using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject FigureScrollView;
    public Button HideButton;
    public void ExitButton_OnClick()
    {
        Application.Quit();
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
	void Start ()
    {
      

    }
	
	
	void Update () {
		
	}
}
