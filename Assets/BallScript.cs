using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform destroyeffect;
    public Transform powerUp;
    public int powerUpChance;
    public int startSpeed;
    AudioSource audio;
    public GameManager gm;

    // Hier roep je de rigidbody en de audio aan.
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
       
    }

    // Update is called once per frame
    void Update()
    {
        // Door dit script met de GameManager te koppelen laat je de bal stoppen als je geen levens meer hebt.
        if (gm.gameOver){
            return;
        }       
                
        // Als het spel niet speelt dan blijft de bal op de paddle.
        if (!inPlay){
            transform.position = paddle.position;
        }


        // Als je spatie drukt en het spel speelt niet af, dan wordt de bal omhoog geschoten en speelt het spel. De bal krijgt dan een standaard snelheid.
        // Als het spel speelt en de bal een snelheid heeft, dan word die snelheid als normaal gezien.
        if (Input.GetButtonDown("Jump") && !inPlay) {
            inPlay = true;
            speed = startSpeed;
            rb.velocity = Vector2.up * speed;
        }else if (inPlay){
            rb.velocity =  rb.velocity.normalized * speed;
        }
    }

    // Als de bal de bottom box collider raakt dan geeft het aan dat het speel niet speelt, dus het wordt op false gezet.
    // De snelheid staat dan weer op.
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Bottom")){
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives (-1);
        }
    }

    // Zorgt ervoor dat als de bal een object raakt met de tag "Brick", dat het object kapot word gemaakt.
    // Ook maak je de effecten waar op de plek van de bricks en later destroy je het in de hierachy.
    void OnCollisionEnter2D(Collision2D other){
        if (other.transform.CompareTag("Brick")) {

            // Hier laat je de kans rangen tussen 1 en 50 en dan als de kans minder is dan 25 dan spawned het een power up.
            int randChance = Random.Range(1, 101);
            if(randChance  < powerUpChance){
                Instantiate(powerUp, other.transform.position, other.transform.rotation);
            }

            // De Destroy effect word gespawned op de positie waar het word geraakt
            Transform newDestroyEffect = Instantiate(destroyeffect, other.transform.position, other.transform.rotation);
            Destroy(newDestroyEffect.gameObject, 2.5f);

            // Hier zeg je dus dat je naar het object moet gaan wat je zojuist hebt geraakt en pak daarvan het script. Ga in het Brickscript en pak dan de waarde van de punten.
            // Destroy vervolgens het game object en je laat een geluidje afspelen.
            gm.UpdateScore(other.gameObject.GetComponent<BrickScript>().points);
            gm.UpdateNumbersOfBricks();
            Destroy(other.gameObject);
            audio.Play();
        }
        
    }
}
