using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameOver : MonoBehaviour {

    public static GameOver instance;
    public Text nowScoreUI;
    public Text highScoreUI;
	// Use this for initialization
	void Start () {
        instance = this;
        this.gameObject.SetActive(false);
	}

    public void Show(float nowScore) {
        float historyScore = PlayerPrefs.GetFloat("historyHighScore", 0);
        if (nowScore > historyScore) {
            PlayerPrefs.SetFloat("historyHighScore", nowScore);
        }
        highScoreUI.text = "Highest: " + historyScore;
        nowScoreUI.text = "Current: " + nowScore;
        this.gameObject.SetActive(true);

    }
	// Update is called once per frame
	void Update () {
	
	}
}
