using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Je laat hier de powerups naar beneden vallen, maar als je de power up niet hebt geraakt en uit het zicht verdwijnt, dan destroy je het object.
    void Update() {
        transform.Translate(new Vector2(0f,-1f) * Time.deltaTime * speed);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
