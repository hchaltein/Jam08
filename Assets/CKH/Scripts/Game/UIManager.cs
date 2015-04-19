using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : GameBehaviour
{
    public GameObject PlayerObj;
    CharacterController2D PlayerObjScr;
    public GameObject JumpTextObj;
    Text JumpText;


    void Awake()
    {
        PlayerObjScr            = PlayerObj.GetComponent<CharacterController2D>();
        JumpText                = JumpTextObj.GetComponent<Text>();

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
    }

}
