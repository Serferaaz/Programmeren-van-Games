using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    public BallScript ballscript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Door dit script met de GameManager te koppelen laat je de paddle stoppen als je geen levens meer hebt.
        if (gm.gameOver){
            return;
        }


        // Hier geef je aan dat de paddle horizontaal te werk gaat en dat de paddle niet verder kan gaan dan het scherm zelf.
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    // Hier staat dus dat als je de power up aanraakt, dat het de powerup kapotmaakt.
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Powerup"))
        {
            ballscript.speed += 0.5f;
            Destroy(other.gameObject);
        }
    }
}

