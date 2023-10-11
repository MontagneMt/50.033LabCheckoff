using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpOverGoomba : MonoBehaviour
{
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    public GameObject goomba; // ��Ҫ����goomba����
    private bool onGroundState;

    [System.NonSerialized]
    public int score = 0;

    
    public Vector3 boxSize;
    public float maxDistance;
    public LayerMask layerMask;
    public LayerMask enemyMask; // ���ڼ����Goomba����ײ

    void Start()
    {
        // ��ԭ����Start�����ǿյģ������ұ���������
    }

    void Update()
    {
        // ��ԭ����Update�����ǿյģ������ұ���������
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("space") && onGroundCheck())
        {
            onGroundState = false;
        }

     
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) onGroundState = true;
    }

    private bool onGroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, maxDistance, layerMask))
        {
            Debug.Log("on ground");
            return true;
        }
        else
        {
            Debug.Log("not on ground");
            return false;
        }
    }
}
