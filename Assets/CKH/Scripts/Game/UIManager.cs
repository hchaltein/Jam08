using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : GameBehaviour
{
    public GameObject PlayerObj;
    CharacterController2D PlayerObjScr;

    public GameObject JumpTextObj;
    Text JumpText;

    public GameObject SpeedTextObj;
    Text SpeedText;

    public GameObject LivesTextObj;
    Text LivesText;

    public GameObject LevelTextObj;
    Text LevelText;

    


    void Awake()
    {
        PlayerObjScr            = PlayerObj.GetComponent<CharacterController2D>();
        JumpText                = JumpTextObj.GetComponent<Text>();
        SpeedText = SpeedTextObj.GetComponent<Text>();
        LivesText = LivesTextObj.GetComponent<Text>();
        LevelText = LevelTextObj.GetComponent<Text>();

        // Updates UI to Default Value
        UpdateUI();
    }

	// Update is called once per frame
	void Update ()
    {
        
        UpdateUI();
	}

    //Updates the UI
    void UpdateUI()
    {
        JumpText.text = "Jumps: " + PlayerObjScr.RemainingJumps.ToString() + "/" + PlayerObjScr.TotalJumps.ToString();

        SpeedText.text = "Speed: " + Time.timeScale.ToString();
        LivesText.text = "Lives: " + PlayerObjScr.Lives.ToString();
        LevelText.text = "Level: " + Application.loadedLevelName;

        // Uptade rest of texts here.
    }

}
