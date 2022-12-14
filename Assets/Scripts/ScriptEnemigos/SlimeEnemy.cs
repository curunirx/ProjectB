using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movHor = 0f;
    public float speed = 3f;

    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGive = 50;

    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //evitar caer en un precipicio
        isGroundFloor =(Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z )
        , new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer));// revisa la distancia en x en 1 hacia delante si se mueve en esa direccion o 1 hacia atras en caso contrario

        if (isGroundFloor)//mientras encuentre piso no hara nada
            movHor = movHor * -1;

        // Choque con pared
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer)) //si el raycast da verdadero con el choque,cambiaremos el sentido del mov
            movHor = movHor *-1;
        //Choque con otro enemigo
        hit = (Physics2D.Raycast(new Vector3(transform.position.x + movHor*frontCheck, transform.position.y, transform.position.z ),
            new Vector3(movHor, 0, 0), frontDist));// revisa la distancia en x en 1 hacia delante si se mueve en esa direccion o 1 hacia atras en caso contrario

        if (hit != null)
        {
            if (hit.transform != null)
            {
                if(hit.transform.CompareTag("Enemy"))//revisa si choca con el enemigo
                    movHor = movHor *-1;
            }
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor*speed, rb.velocity.y);
    }

   /* void OnCollisionEnter2D(Collision2D collision)
    {
        // da??ar al player
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Da??o al personaje"); //manda un mensaje por consola
            player.obj.getDamage(500);
        }

    }*/

    

    private void getKilled()
    {
         gameObject.SetActive(false); // funcion que desactiva el objeto
    } 
}