using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject cardPrefab;
	public Sprite[] cardPictures = new Sprite[5];

	private GameObject[] cards = new GameObject[15];

	// Start is called before the first frame update
	void Start() {
		InitField();
		StartCoroutine(TurnCards());
	}

	// Update is called once per frame
	void Update() {

		if (Input.GetButtonDown("Fire1")) {
			GameObject gameObject;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit)) {
				gameObject = hit.collider.gameObject;
				gameObject.GetComponent<CardScript>().Turn();
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
		int	rand2 = 0;
		for (int i = 0; i < 15; i++) {
			do {
				rand1 = Random.Range(0, 15);
				rand2 = Random.Range(0, 15);
			} while (rand1 == rand2);
			SwapCards(rand1, rand2);
		}		
	}

	IEnumerator TurnCards() {
		yield return new WaitForSeconds(3f);

		for (int i = 0; i < cards.Length; i++) {
			cards[i].GetComponentInChildren<CardScript>().Turn();
		}
	}

	void SwapCards(int card1, int card2) {
		Vector3 buffer = cards[card1].transform.position;
		cards[card1].transform.position = cards[card2].transform.position;
		cards[card2].transform.position = buffer;
	}
}
