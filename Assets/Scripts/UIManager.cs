using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject taptoplaceAnim;
    [SerializeField] GameObject infoText;
    [SerializeField] GameObject warningPanel;
    
    private bool IsPlayed;
    public bool tapToPlaceAnimation
    {
        get
        {
            return this.IsPlayed;
        }
        set
        {
            IsPlayed = value;
            RemoveTapToPlaceUI();
        }

    }

    private void Awake()
    {
        warningPanel.SetActive(true);
        taptoplaceAnim.SetActive(false);
        infoText.SetActive(false);
    }

    public void CloseWaring()
    {
        warningPanel.SetActive(false);
        IsPlayed = true;
        //Tap to place UI È°¼ºÈ­
        taptoplaceAnim.SetActive(true);
        infoText.SetActive(true);
    }

    private void RemoveTapToPlaceUI()
    {
        if(!IsPlayed)
        {
            taptoplaceAnim.SetActive(false);
            infoText.SetActive(false);
        }
        
    }

}
