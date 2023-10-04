using System.Collections;
using UnityEngine;

public class BrickBlock2 : MonoBehaviour
{
    public float bounceHeight = 0.5f; // 方块跳跃的高度
    public float bounceSpeed = 3.0f;  // 跳跃的速度
    private bool isUsed = false;  // 判断盒子是否已经被顶过
    
    private SpriteRenderer spriteRenderer;

    private Vector3 originalPosition;
    private bool isBouncing = false;
    private AudioSource audioSource;

    void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isUsed) // 确保玩家有“Player”的标签且方块未被顶过
        {
            // 如果玩家从下方碰撞方块
            if (collision.contacts[0].normal.y > 0.5f && !isBouncing)
            {
                StartCoroutine(Bounce());
                isUsed = true;
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
    }
}
