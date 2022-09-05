using System.Collections;
using System.Collections.Generic;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLost : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextReplay;
    [SerializeField] private Button BtnReplay;
    [SerializeField] private TextMeshProUGUI LevelText;
    private string textreplay= "Tap To Replay";
    private string leveltext = "LEVEL ";
    void Start()
    {
        LevelText.text = leveltext + LevelData.Currentlevel;
        TextReplay.text = textreplay;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void RePlay()
    {
        GameManager.GameManager.Instance.ReLoadLevel(LevelData.Currentlevel);
        GameManager.GameManager.Instance.GameDefaultState();
        Destroy(gameObject);
    }
}
