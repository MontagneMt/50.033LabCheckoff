using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public GameConstants gameConstants;
    private float originalX;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    public Vector3 startPosition = new Vector3(-21.15f, -7.62f, 0.0f);
    private bool alive;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
        alive = true;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.goombaMaxOffset / gameConstants.goombaPatrolTime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

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
}