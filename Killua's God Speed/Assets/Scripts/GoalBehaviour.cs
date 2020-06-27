using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    SphereCollider enemyCollider;

    public Transform cornerOne;
    public Transform cornerTwo;

    // Start is called before the first frame update
    void Start()
    {
        enemyCollider = GetComponent<SphereCollider>();

        if(cornerOne != null && cornerTwo != null) GoalSpawn(cornerOne, cornerTwo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ReachedGoal();
        }
    }

    public virtual void ReachedGoal()
    {
        //Put in effects or behaviours that happen if something touches the goal
        GameManager.instance.PlayerLoses();
    }


    public void GoalSpawn(Transform corner1, Transform corner2)
    {
        float x = Random.Range(corner1.position.x, corner2.position.x);
        float y = 2;
        float z = Random.Range(corner1.position.z, corner2.position.z);
        Vector3 position = new Vector3(x, y, z);

        transform.position = position;
    }


    public void Run()
    {
        float speed = 100f;

        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
}
