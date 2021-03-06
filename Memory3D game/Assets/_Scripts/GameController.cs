﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject cardPrefab;
	public Sprite[] cardPictures = new Sprite[5];

	// Game UI
	public GameUI gameUI;

	private GameObject[] cards = new GameObject[15];

	//public static int record;
	private int score = 0;
	private int lives = 5;
	private int cardSetsLeft = 5;

	private int combo = 0;
	private int currentCardValue = -1;

	private List<CardScript> turnedUpCards = new List<CardScript>();

	private bool isActive = true;

	// Start is called before the first frame update
	void Start() {
		InitField();
		StartCoroutine(TurnCards());
	}

	// Update is called once per frame
	void Update() {

		//if (Input.GetButtonDown("Fire1")) {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (touch.phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				//RaycastHit hit;

				if (Physics.Raycast(ray, out RaycastHit hit)) {
					GameObject gameObject = hit.collider.gameObject;
					CardScript hittenCard = gameObject.GetComponent<CardScript>();
					if (gameObject.CompareTag("Card") && isActive && !hittenCard.IsStateUp) {
						TapCard(hittenCard);
					}
				}
			}
		}

	}

	void InitField() {
		int n = 0;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 3; j++) {
				cards[n] = Instantiate(cardPrefab, new Vector3(j, 0, i), Quaternion.identity);
				CardScript card = cards[n].GetComponentInChildren<CardScript>();
				card.picture.sprite = cardPictures[i];
				card.value = i;
				n++;
			}
		}

		int rand1 = 0;
		int rand2 = 0;
		for (int i = 0; i < 15; i++) {
			do {
				rand1 = Random.Range(0, 15);
				rand2 = Random.Range(0, 15);
			} while (rand1 == rand2);
			SwapCards(rand1, rand2);
		}
	}

	void TapCard(CardScript tappedCard) {
		tappedCard.Turn();
		turnedUpCards.Add(tappedCard);
		if (combo == 0) {
			UpdatePointsAndLives(true);
			currentCardValue = tappedCard.value;
		} else {			
			if (tappedCard.value == currentCardValue) { // если новая карта совпадает с предыдущей
				if (combo == 2) {
					StartCoroutine(DelayedTurnCards(true));
					cardSetsLeft--;
				}
				UpdatePointsAndLives(true);
			} else { // если не совпадает
				StartCoroutine(DelayedTurnCards(false));
				UpdatePointsAndLives(false);
			}
		}
		if (IsGameOver()) {
			isActive = false;
			gameUI.GameOver(cardSetsLeft == 0, score);
		}
	}

	void UpdatePointsAndLives(bool isSuccess) {
		if (isSuccess) {
			if (combo == 1) {
				score += lives;
				combo++;
			} else if (combo == 2) {
				score += lives * 3;
				combo = 0;
			} else {
				combo++;
			}
			gameUI.UpdateScore(score);
			//scoreText.text = score.ToString();
		} else {
			combo = 0;
			lives--;
			gameUI.UpdateHP(lives);
			//hitPoints[lives].sprite = heartEmpty;
		}
	}

	private bool IsGameOver() {
		return lives == 0 || cardSetsLeft == 0;
	}

	IEnumerator TurnCards() {
		yield return new WaitForSeconds(3f);

		for (int i = 0; i < cards.Length; i++) {
			cards[i].GetComponentInChildren<CardScript>().Turn();
		}
		isActive = true;
	}

	IEnumerator DelayedTurnCards(bool deleteMode) {
		isActive = false;
		yield return new WaitForSeconds(1f);

		if (deleteMode) {
			foreach (CardScript curCard in turnedUpCards) {
				Destroy(curCard.gameObject);
			}
		} else {
			foreach (CardScript curCard in turnedUpCards) {
				curCard.Turn();
			}
		}
		turnedUpCards.Clear();
		isActive = true;
	}

	void SwapCards(int card1, int card2) {
		Vector3 buffer = cards[card1].transform.position;
		cards[card1].transform.position = cards[card2].transform.position;
		cards[card2].transform.position = buffer;
	}
}
