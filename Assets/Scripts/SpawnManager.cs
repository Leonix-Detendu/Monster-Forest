using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static SpawnManager instance;

    public GameObject[] Monsters;
    // public Target[] TargetScripts;

    [SerializeField] private float limitLeft;
    [SerializeField] private float limitRight;
    [SerializeField] private float limitUp;
    [SerializeField] private float limitDown;

    private bool shouldSpawn = true;
    [SerializeField] private float spawnDelayMin;
    [SerializeField] private float spawnDelayMax;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        /* TargetScripts = new Target[Monsters.Length];
        int i = 0;
        
        foreach ( GameObject monster in Monsters )
        {
            TargetScripts[i] = monster.GetComponent<Target>();
            i++;
        } */
    }

    private void Update()
    {
        if (shouldSpawn) StartCoroutine(SpawnRoutine()); 
    }

    IEnumerator SpawnRoutine()
    {
        shouldSpawn = false;

        Spawn();

        yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));

        shouldSpawn = true;
    }

    private void Spawn()
    {
        Vector2 spawnPos = new Vector2(Random.Range(limitLeft, limitRight), Random.Range(limitDown, limitUp));

        int indexOfMonster = Random.Range(0, Monsters.Length);

        BoxCollider2D colliderOfMonster = Monsters[indexOfMonster].GetComponent<BoxCollider2D>();

        Vector2 monsterSize = colliderOfMonster.size;

        while (Physics.CheckBox(spawnPos, monsterSize / 2 + new Vector2(0.5f, 0.5f)))
        {
            spawnPos = new Vector2(Random.Range(limitLeft, limitRight), Random.Range(limitDown, limitUp));
        }

        GameObject newMonster = Instantiate(Monsters[indexOfMonster], spawnPos, Monsters[indexOfMonster].transform.rotation);

        Target newMonsterTargetScript = newMonster.GetComponent<Target>();

        newMonsterTargetScript.hasSpawned = true;
    }
}
