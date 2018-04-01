using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
    public float speed = 30;
	private int score_red;
	private int score_blue;
	public Text scoreBlueText;
	public Text scoreRedText;
	public Text winText;

	public string winner = "";

    void Start() {
        // Initial Velocity
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
		score_red = 0;
		score_blue = 0;
		SetBlueText ();
		SetRedText ();
		winText.text = "";
    }
    




    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                    float racketHeight) {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    void OnCollisionEnter2D(Collision2D col) {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider
        
        // Hit the left Racket?
        if (col.gameObject.name == "RacketLeft") {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;

        }

		if (col.gameObject.name == "WallLeft") {
			score_red = score_red + 1;
			SetRedText ();
		}




        // Hit the right Racket?
        if (col.gameObject.name == "RacketRight") {
            // Calculate hit Factor
            float y = hitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;
            
            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

		if (col.gameObject.name == "WallRight") {
			score_blue = score_blue + 1;
			SetBlueText ();
		}
    }

	void SetBlueText ()
	{
		scoreBlueText.text = "Score: " + score_blue.ToString ();
		if (score_blue >= 11)
		{
			
			//StartCoroutine(ShowMessage("Blue Wins!", 200000));
			winner = "Blue Wins!";
			PlayerPrefs.SetString ("winner", winner);
			SceneManager.LoadScene (1);

		}
	}

	void SetRedText ()
	{
		scoreRedText.text = "Score: " + score_red.ToString ();
		if (score_red >= 11)
		{
			
			//StartCoroutine(ShowMessage("Red Wins!", 200000));
			winner = "Blue Wins!";
			PlayerPrefs.SetString ("winner", winner);
			SceneManager.LoadScene (1);
		}
	}

	IEnumerator ShowMessage (string message, float delay) {
		winText.text = message;
		yield return new WaitForSeconds(delay);
		winText.text ="";
	}

}
