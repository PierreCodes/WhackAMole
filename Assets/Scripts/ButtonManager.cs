using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour {

    public ScoreManager scoreManager;
    public TimeManager timeManager;

    //When you add a Tooltip, and mouse over this var in the Editor, it will show the assigned text
    [Tooltip("connect all the game buttons to this")]
    public GameObject[] buttons;//By giving the '[]' assingment to the GameObject var we have created an array! Arrays are one type of Data structure


    //let's start this out with .5 seconds at first, then it will get randomly set to a new time.
    float randTime = .5f;

    //by setting this public we can modify it in unity and change it at runtime too so we can see what feels right.
    public float hideTime = 1.0f;

	// Use this for initialization
	void Start () {

        //Your first "For Loop"! Here we iterate thru our 'buttons' array and set each one to inactive at the start of the game.
        for (int i = 0; i < buttons.Length; i ++)
        {
            buttons[i].SetActive(false);
        }

        //we also set a CoRoutine, which is a delayed event to start showing our random buttons which starts the game
        StartCoroutine(ShowButton());
	}
	
    //This is what we will hook up to the buttons
	public void PressedBtn(){
        //allow the player to score as long as the game is still going
        if (timeManager.gameOver == false)
        {
            //Grab a reference to the object that we pressed so we can do some stuff with it
            GameObject myBtn = EventSystem.current.currentSelectedGameObject;

            //change the color to red & set text so that we know we hit it
            myBtn.GetComponentInChildren<Text>().text = "HIT!";
            myBtn.GetComponentInChildren<Image>().color = Color.red;

            //add a point
            scoreManager.UpdateScore(10, true);

            //start a delayed timer aka a CoRoutine to hide the button again until the Next time it is chosen
            StartCoroutine(HitButton(myBtn));
        }

	}

    //this is on repeat, until the game is over
    IEnumerator ShowButton(){

        yield return new WaitForSeconds(randTime);

        //allow the coroutine to happen as long as the game is still going
        if (timeManager.gameOver == false)
        {
			//randomly pick a button in the index to show.  By using .Length it will count how many buttons are in our array, so it can change with out our help aka it's now dynamic
			int randBtn = Random.Range(0, buttons.Length);

			//we grab the button at the numbered postion "randBtn" in the "buttons[]" array and see if it is active
			if (buttons[randBtn].activeInHierarchy == false)//not active so let's show it
			{
				buttons[randBtn].SetActive(true);

			}

			//let's make the time random, for next time this coroutine is called
			randTime = Random.Range(0, 1f);

			//we need the coroutine to keep repeating to keep the game going, so we call it in itself.  
			StartCoroutine(ShowButton());

			//hide the button too after a delay, like in whack a mole
			StartCoroutine(HideButton(buttons[randBtn]));
        }
		
	}

    /// <summary>
    /// Hides the button after a delay from being Shown
    /// Called from "ShowButton" coroutine
    /// </summary>
    /// <returns>The button.</returns>
    /// <param name="myBtn">My button.</param>
    IEnumerator HideButton(GameObject myBtn)
	{
		yield return new WaitForSeconds(hideTime);

        //let's make sure it wasn't hit first because we don't want two coroutines on the same button
        if (myBtn.GetComponentInChildren<Image>().color == Color.green)//green, then it hasn't been hit, so we can hide it
        {
			//set to inactive so that it can get called again!
			myBtn.gameObject.SetActive(false);

			//player didn't hit this in time, so we reduce their score
			scoreManager.UpdateScore(5, false);

		}
       


	}

    /// <summary>
    /// Called when you hit a button, to reset it's state back to hidden and green
    /// Notice how we passed the game object thru the method! Now we have full access to the original button, but in a different method
    /// </summary>
    /// <returns>The button.</returns>
    /// <param name="myBtn">My button.</param>
	IEnumerator HitButton(GameObject myBtn)
	{
		yield return new WaitForSeconds(.5f);
		//change the color back to gree red & reset text
		myBtn.GetComponentInChildren<Text>().text = "";
		myBtn.GetComponentInChildren<Image>().color = Color.green;

		//set to inactive so that it can get called again!
		myBtn.gameObject.SetActive(false);


	}
}
