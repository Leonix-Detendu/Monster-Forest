using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [HideInInspector] public static SpawnManager instance;

    public GameObject[] Monsters;

    [SerializeField] private float limitLeft;
    [SerializeField] private float limitRight;
    [SerializeField] private float limitUp;
    [SerializeField] private float limitDown;

    [HideInInspector] public float currentBonusSpeed;
    [HideInInspector] public float currentDespawnReduction;

    public int scoreInterval;
    public float spawnDelayDecrease;
    public float despawnDelayDecrease;
    public float speedIncrease;

    private bool shouldSpawn = true;
    public float spawnDelayMin;
    public float spawnDelayMax;

    public float minOfSpawnDelay;


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

        currentBonusSpeed = 0f;
        currentDespawnReduction = 0f;
    }

    private void Update()
    {
        if (shouldSpawn && !GameManager.instance.gameOver) StartCoroutine(SpawnRoutine()); 
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

        StartCoroutine(GrowthRoutine(newMonster));
    }

    IEnumerator GrowthRoutine(GameObject monster)
    {
        if (monster == null) yield break;

        Vector3 defaultScale = monster.transform.localScale;
        Target monsterTargetScript = monster.GetComponent<Target>();

        Debug.Log(monsterTargetScript.moveSpeed);

        monster.transform.localScale = Vector3.one * 0.01f;

        while (monster != null && monster.transform.localScale.sqrMagnitude < (monsterTargetScript.maxScaleFactor * defaultScale).sqrMagnitude)
        {
            monster.transform.localScale = monster.transform.localScale * monsterTargetScript.scaleFactor;
            yield return new WaitForEndOfFrame();
        }

        while (monster != null && monster.transform.localScale.sqrMagnitude > defaultScale.sqrMagnitude)
        {
            monster.transform.localScale = monster.transform.localScale / monsterTargetScript.scaleFactor;
            yield return new WaitForEndOfFrame();
        }

        if (monster != null)
        {
            monster.transform.localScale = defaultScale;
        }
    } 
}
