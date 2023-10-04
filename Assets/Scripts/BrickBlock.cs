using System.Collections;
using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    public float bounceHeight = 0.5f; // 方块跳跃的高度
    public float bounceSpeed = 3.0f;  // 跳跃的速度
    public GameObject coinPrefab;  // 金币的Prefab
    public Vector3 coinOffset = new Vector3(0, 1.5f, 0);  // 根据你的方块大小来调整
    public float coinBounceHeight = 1.0f;  // 金币弹起的高度
    public float coinBounceSpeed = 3.0f;  // 金币弹起的速度
    private bool isUsed = false;  // 判断盒子是否已经被顶过
    
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
        if (collision.gameObject.CompareTag("Player")) // 确保玩家有“Player”的标签
        {
            // 如果玩家从下方碰撞方块
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
            isUsed = false;
            
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
    Destroy(coin);  // 如果你希望金币重新落入盒子并消失的话
    }
}