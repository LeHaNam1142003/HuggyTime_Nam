using System.Collections;
using System.Collections.Generic;
using Pattern;
using UnityEngine;

public class GamePopupManager : Singleton<GamePopupManager>
{
    [SerializeField] private PopupWin PopupWin;
    [SerializeField] private PopupLost PopupLost;
    [SerializeField] private Transform Popup;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameWin()
    {
        Instantiate(PopupWin, Popup.transform);
    }

    public void SetGameLost()
    {
        Instantiate(PopupLost, Popup.transform);
    }
}
