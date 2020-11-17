using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public Sprite heartEmpty;
	public Image[] hitPoints;
	public Text scoreText;

	public GameObject gameOverScreen;
	public Text gameResult;
	public Text recordAchieved;
	public Text recordScore;
	public Text gameScore;

	public void UpdateHP(int newHP) {
		hitPoints[newHP].sprite = heartEmpty;
	}

	public void UpdateScore(int newScore) {
		scoreText.text = newScore.ToString();
	}

	public void GameOver(bool isWin, int score) {
		gameOverScreen.SetActive(true);
		if (isWin) {
			gameResult.text = "ВЫ ПОБЕДИЛИ!";
		} else {
			gameResult.text = "ВЫ ПРОИГРАЛИ!";
		}
		if (score > PlayerPrefs.GetInt("record")) {
			PlayerPrefs.SetInt("record", score);
			recordAchieved.gameObject.SetActive(true);
			recordAchieved.text = "НОВЫЙ РЕКОРД!";
		} else {
			recordAchieved.gameObject.SetActive(false);
		}

		recordScore.text = "Лучший результат: " + PlayerPrefs.GetInt("record");
		gameScore.text = "Ваш результат: " + score;
	}
}
