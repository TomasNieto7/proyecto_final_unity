using UnityEngine;
using System.Collections;

public class GeneratorNpcs : MonoBehaviour
{
    [Header("Configuración NPCs")]
    public GameObject[] npcPrefabs;
    public int maxNPCs = 5; 

    [Header("Posición y Orientación")]
    public Transform spawnPoint;
    public Transform objetivoAMirar; 
    public Transform puntoSalida;    
    public float alturaExtra = 0.0f;

    [Header("Tiempos")]
    public float minTime = 2.0f;
    public float maxTime = 5.0f;

    private bool isSpawning = true;

    void Start()
    {
        if (spawnPoint == null) spawnPoint = transform;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            SpawnRandomNPC();
        }
    }

    void SpawnRandomNPC()
    {
        int cantidadActual = GameObject.FindGameObjectsWithTag("npc").Length;

        if (cantidadActual >= maxNPCs)
        {
            return; 
        }

        if (npcPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, npcPrefabs.Length);
        GameObject selectedPrefab = npcPrefabs[randomIndex];

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.y += spawnPoint.localScale.y + alturaExtra;

        GameObject nuevoNPC = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        npc scriptDelNPC = nuevoNPC.GetComponent<npc>();

        if (scriptDelNPC != null)
        {
            scriptDelNPC.objetivoDestino = objetivoAMirar; 
            scriptDelNPC.puntoSalida = puntoSalida; 
        }
    }
}