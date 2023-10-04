using UnityEngine;
using System.Collections;

public class GoombaEffects : MonoBehaviour
{
    public Sprite flattenedGoombaSprite; // 把你的扁Goomba Sprite拖到这里
    public AudioClip deathSound; // 把Goomba的死亡音效拖到这里
    private AudioSource audioSource;
    public Sprite originalGoombaSprite;
    

    private bool isFlattened = false;

    void Start()
    {
        originalGoombaSprite = GetComponent<SpriteRenderer>().sprite;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void FlattenGoomba()
    {
        if (!isFlattened)
        {
            isFlattened = true;
            GetComponent<SpriteRenderer>().sprite = flattenedGoombaSprite; // 播放死亡动画
            if (deathSound)
            {
                audioSource.PlayOneShot(deathSound); // 播放击杀音效
            }

            // 设置一个延迟，确保动画和音效播放完成后再设置Goomba为不活跃
            StartCoroutine(DeactivateGoombaAfterDelay(0.7f));
        }
    }

    IEnumerator DeactivateGoombaAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // 设置Goomba为不活跃
    }

    public void ResetGoomba()
    {
        GetComponent<SpriteRenderer>().sprite = originalGoombaSprite;
        isFlattened = false;
    }




}
