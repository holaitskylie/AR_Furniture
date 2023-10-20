using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapMenu : MonoBehaviour
{
    public GameObject tabButton1;
    public GameObject tabButton2;
    public GameObject tabButton3;

    public GameObject tabContainer1;
    public GameObject tabContainer2;
    public GameObject tabContainer3;

    public void HideAllTabs()
    {
        tabContainer1.SetActive(false);
        tabContainer2.SetActive(false);
        tabContainer3.SetActive(false);

        //Change Pressed Tap Button's Color
        tabButton1.GetComponent<Button>().image.color = new Color32(154, 136, 108, 255);
        tabButton2.GetComponent<Button>().image.color = new Color32(154, 136, 108, 255);
        tabButton3.GetComponent<Button>().image.color = new Color32(154, 136, 108, 255);
    }

    public void ShowTap1()
    {
        HideAllTabs();
        tabContainer1.SetActive(true);
        //Change tabButton1's Color into White Color
        tabButton1.GetComponent<Button>().image.color=new Color32(255,255, 255, 255);   
    }

    public void ShowTap2()
    {
        HideAllTabs();
        tabContainer2.SetActive(true);
        //Change tabButton1's Color into White Color
        tabButton2.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
    }

    public void ShowTap3()
    {
        HideAllTabs();
        tabContainer3.SetActive(true);
        //Change tabButton1's Color into White Color
        tabButton3.GetComponent<Button>().image.color = new Color32(255, 255, 255, 255);
    }


}
