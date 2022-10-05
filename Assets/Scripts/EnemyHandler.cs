using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    NavMeshAgent agent;
    [Header("Enemy Properties")]
    public Transform player;
    public float health = 50f;
    public float hitDamage = 10f;

    private int id;

    // [Header("Other")]
    private Canvas gameInfo;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        // Debug.Log("Taking Damage, Health: "+health);
        if (health <= 0f)
        {
            die();
            this.gameInfo.GetComponent<GameInfo>().incrementKill();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }

    public void setGameInfo(Canvas gameInfoCanvas)
    {
        this.gameInfo = gameInfoCanvas;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public int getId()
    {
        return this.id;
    }
}
