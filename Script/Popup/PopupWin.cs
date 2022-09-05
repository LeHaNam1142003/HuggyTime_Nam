using System.Collections;
using System.Collections.Generic;
using Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextNextLevel;
    [SerializeField] private TextMeshProUGUI TextLevel;
    [SerializeField] private Button BtnNextLevel;
    private string textnextlevel ="Tap To Continue";
    private string textlevel = " LEVEL ";
    void Start()
    {
        TextNextLevel.text = textnextlevel;
        TextLevel.text = textlevel + LevelData.Currentlevel;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextLevel()
    {
        LevelData.Currentlevel++;
        GameManager.GameManager.Instance.LoadLevel(LevelData.Currentlevel);
        GameManager.GameManager.Instance.GameDefaultState();
        Destroy(gameObject);
    }
}
