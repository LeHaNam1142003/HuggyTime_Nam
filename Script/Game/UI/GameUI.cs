using System.Collections;
using System.Collections.Generic;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI SloganText;
    private string slogan = " KILL THEM ALL";
    private string leveltext = "LEVEL";
    void Start()
    {
        SloganText.text = (slogan);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLevelText(int index)
    {
        LevelText.text = leveltext + index;
    }
    public void RePlayBtn()
    {
        GameManager.GameManager.Instance.ReLoadLevel(LevelData.Currentlevel);
    }
}
