using UnityEngine;
using System.Collections;

public class GoombaEffects : MonoBehaviour
{
    public Sprite flattenedGoombaSprite; // ����ı�Goomba Sprite�ϵ�����
    public AudioClip deathSound; // ��Goomba��������Ч�ϵ�����
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
            GetComponent<SpriteRenderer>().sprite = flattenedGoombaSprite; // ������������
            if (deathSound)
            {
                audioSource.PlayOneShot(deathSound); // ���Ż�ɱ��Ч
            }

            // ����һ���ӳ٣�ȷ����������Ч������ɺ�������GoombaΪ����Ծ
            StartCoroutine(DeactivateGoombaAfterDelay(0.7f));
        }
    }

    IEnumerator DeactivateGoombaAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // ����GoombaΪ����Ծ
    }

    public void ResetGoomba()
    {
        GetComponent<SpriteRenderer>().sprite = originalGoombaSprite;
        isFlattened = false;
    }




}
