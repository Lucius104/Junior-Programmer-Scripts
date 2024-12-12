using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public float sSpeed = 1f;
    public float movementRange = 10f;
    private Vector3 startLocation;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        AttackRoutine();
    }

    void AttackRoutine()
    {
        Vector3 attackPosition = player.transform.position - transform.position;
        float attackRange = Vector3.Distance(player.transform.position, transform.position);

        if (attackRange < movementRange)
        {
            transform.Translate(attackPosition * speed * Time.deltaTime);
        }
    }

}
