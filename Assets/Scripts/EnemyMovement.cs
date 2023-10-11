<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

<<<<<<< HEAD
    public GameConstants gameConstants;
    private float originalX;
=======
    


    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

<<<<<<< HEAD
    public Vector3 startPosition = new Vector3(-21.15f, -7.62f, 0.0f);
    private bool alive;
=======
    

    

    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
<<<<<<< HEAD
        alive = true;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.goombaMaxOffset / gameConstants.goombaPatrolTime, 0);
=======
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

<<<<<<< HEAD
    void Update()
    {
        if (alive)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.goombaMaxOffset)
            {// move goomba
                Movegoomba();
            }
            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                Movegoomba();
            }
        }
    }

    public void GetCrushed(GameObject goomba)
    {
        if (GameObject.ReferenceEquals(this.gameObject, goomba))
        {
            this.gameObject.tag = "DeadEnemies";
            alive = false;
            GetComponent<Animator>().Play("Smash-goomba");
            enemyBody.bodyType = RigidbodyType2D.Static;
        }
    }
    public void DestroyGoomba()
    {
        Destroy(gameObject);
    }
=======

    void OnTriggerEnter2D(Collider2D other)
    {
     Debug.Log(other.gameObject.name);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }

>>>>>>> 61e7f24223104f05955f697e592929f862e9089a
}