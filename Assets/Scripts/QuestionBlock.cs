using System.Collections;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public float bounceHeight = 0.5f; 
    public float bounceSpeed = 3.0f;  
    public GameObject coinPrefab;  
    public Vector3 coinOffset = new Vector3(0, 1.5f, 0); 
    public float coinBounceHeight = 1.0f; 
    public float coinBounceSpeed = 3.0f;  
    private bool isUsed = false;  
    public Sprite usedBoxSprite;
    private SpriteRenderer spriteRenderer;

    private Vector3 originalPosition;
    private bool isBouncing = false;
    public AudioClip coinSound;
    private AudioSource audioSource;
    public PlayerMovement player;  // 在Unity编辑器中，你将需要拖拽玩家对象到这个属性上。

    void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isUsed) return; // 如果盒子已经被顶过，则直接返回

        if (collision.gameObject.CompareTag("Player")) 
        {
            if (collision.contacts[0].normal.y > 0.5f && !isBouncing)
            {
                StartCoroutine(Bounce());
            }
        }
    }

    IEnumerator Bounce()
    {
        isBouncing = true;
        
        float startTime = Time.time;
        while (Time.time - startTime < bounceSpeed)
        {
            transform.position = new Vector3(originalPosition.x, originalPosition.y + Mathf.Sin((Time.time - startTime) * Mathf.PI / bounceSpeed) * bounceHeight, originalPosition.z);
            yield return null;
        }
        
        transform.position = originalPosition;
        isBouncing = false;
        
        if (!isUsed)
        {
            GenerateCoin();
            isUsed = true;
            GetComponent<Animator>().enabled = false;
            spriteRenderer.sprite = usedBoxSprite;
        }
    }

    void GenerateCoin()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position + coinOffset, Quaternion.identity);
        StartCoroutine(CoinBounce(coin));
        audioSource.PlayOneShot(coinSound);

        // Increase player's score in the same way as stomping on a Goomba
        if (player != null)
        {
            player.jumpOverGoomba.score++;
            player.scoreText.text = "Score: " + player.jumpOverGoomba.score.ToString();
        }
        else
        {
            Debug.LogError("PlayerMovement reference not set in BrickBlock.");
        }
    }


    IEnumerator CoinBounce(GameObject coin)
    {   
        float startTime = Time.time;
        Vector3 originalCoinPosition = coin.transform.position;
        while (Time.time - startTime < coinBounceSpeed)
        {
            coin.transform.position = new Vector3(originalCoinPosition.x, originalCoinPosition.y + Mathf.Sin((Time.time - startTime) * Mathf.PI / coinBounceSpeed) * coinBounceHeight, originalCoinPosition.z);
            yield return null;
        }
        Destroy(coin); 
    }
}
