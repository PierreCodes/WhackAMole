using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SummaryManager : MonoBehaviour {

    public Text hitTxt;
    public Text missTxt;
    public Text scoreTxt;



	// Use this for initialization
	void Start () {
        //assign the text to the coordinating player Pref
        hitTxt.text = "Total Hits: " + PlayerPrefs.GetInt(ScoreTypes.PlayerHits.ToString());
        missTxt.text = "Total Misses: " + PlayerPrefs.GetInt(ScoreTypes.PlayerMisses.ToString());
        scoreTxt.text = "Total Score: " + PlayerPrefs.GetInt(ScoreTypes.PlayerScore.ToString());

	}
	

    public void RestartPressed(){

        //load back to Game scene when tapped
        SceneManager.LoadSceneAsync(GameScenes.GameScene.ToString());

    }

}
