using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowBarResource : MonoBehaviour {

    public Image crowbar;
    public PlayerController player;
	
	void Update ()
    {
        crowbar.fillAmount = player.metalBarRessource;
	}
}
