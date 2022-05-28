using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{

    public float speed;
    // private NavMeshAgent enemyMesh;

    public float detectionRange = 30f;

    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float SightAngle = 45;

    //NOTA: deixo comentat el FOV i blocked, mayb no necessari
    //Probablement es pugui fer mes eficient. Corrutinas? FixedUpdates?


    void Update()
    {

        DetectPlayers();

    }

    void DetectPlayers()
    {
        List<Transform> detectedPlayers = new List<Transform>();
        if (IsInRange(ref detectedPlayers)) //  //El ref es pasar per referencia i aixi guardem el valor en la llista
        {
            //Una vez sabemos que lo tenemos en el area cerca, vamos a mirar si esta en el campo de vision
            // if (IsInFOV(detectedPlayers))
            // {
            //     IsNotBlocked(detectedPlayers);

            // }
        }

        // chase(detectedPlayers);
        // Debug.Log(detectedPlayers.Count);
    }

    bool IsInRange(ref List<Transform> detectedplayers)
    {
        var overlaps = Physics.OverlapSphere(transform.position, detectionRange, WhatIsPlayer);
        //transform.position para coger el centro del moñeco, detection range la definimos arriba y 
        //la mascara en el inspector
        //devuelve un array de los detectados, por eso el return

        foreach (var item in overlaps)
        {
            detectedplayers.Add(item.transform);
        }
        //Nos pasa una lista i en esa lista añadimos los que hemos detectado
        // Debug.Log(detectedplayers.Count);
        return (overlaps.Length > 0);

    }

    private bool IsInFOV(List<Transform> detectedPlayers)
    {
        for (int i = detectedPlayers.Count - 1; i >= 0; i--)
        {
            if (!IsInFOV(detectedPlayers[i]))
                detectedPlayers.Remove(detectedPlayers[i]);
            //Al eliminar un element de la llista, els demes es mouen, de manerea que perque no tinguem problemes
            //amb moure la llista, fer la conta al reves
            //La funcion de abajo es para controlar que esta en el marco de la vision
        }
        
        return detectedPlayers.Count > 0;
    }
    private bool IsInFOV(Transform player)
    {
        Vector2 dir = player.position - transform.position;
        var angle = Vector2.Angle(transform.right, dir);
        //En esta funcion le damos dons vectores (2 lineas) y nos dara el angulo que hay entre ellas
        //Definimos las dos linas que tenemos y el angulo de vision a partir de esto
        //El transform.rigth es el horizontal
        //Lo de dir esta explicado en la clase, es trigonometria rara

        return angle < SightAngle / 2;
        //Dividido en dos porque no miramos 45 grados arriba i 45 abajo, sino la mitad abajo i la otra arriba
        //Ambos suman 45
    }

    private bool IsNotBlocked(List<Transform> detectedPlayers)
    {
        for (int i = detectedPlayers.Count - 1; i >= 0; i--)
        {
            if (IsBlockedView(detectedPlayers[i]))
                detectedPlayers.Remove(detectedPlayers[i]);

        }
        return detectedPlayers.Count > 0;
    }

    private bool IsBlockedView(Transform player)
    {
        Vector2 dir = player.position - transform.position;
        var ray = new Ray (transform.position, dir);
        
        RaycastHit hit;
        Physics.Raycast(ray, out hit, detectionRange, WhatIsVisible);

        // Debug.Log(hit.transform.gameObject);
        return hit.transform != player;

        //Este es coger bastante de los del fov i range hechos i modificar poco

    }

      private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        //Este metodo y tal es para ver la circumferencia de deteccion del zombie
        //Este metodo se llama en cada update i solo lo vemos en la escena, no juego.
        //Tambien podemos poner OnDrawGizmosSelected y solo lo veras cuando selecciones el bicho

        var dir = Quaternion.AngleAxis(SightAngle / 2, transform.forward) * transform.right;
        var end = transform.position + dir.normalized * detectionRange;
        // Gizmos.color = Color.red;
        // Gizmos.DrawLine(transform.position, end);

        // dir = Quaternion.AngleAxis(-SightAngle / 2, transform.forward) * transform.right;
        // end = transform.position + dir.normalized * detectionRange;
        // Gizmos.DrawLine(transform.position, end);
        
        Gizmos.color = Color.white;
    }


    void chase(List<Transform> detectedPlayers){ 

        if(detectedPlayers.Count>0){
            // Debug.Log("Chasing buaaaa");
            int itemIndex = Random.Range(0, detectedPlayers.Count);
            // enemyMesh.SetDestination(Players[itemIndex].position);
            transform.position = Vector2.MoveTowards(transform.position, detectedPlayers[itemIndex].position, speed * Time.deltaTime);
        }

    }

    // void OnTriggerEnter(Collider other){

    //     Debug.Log("Collision!!!");

    //     if(other.gameObject.tag == "Player"){
    //         Players.Add(other.gameObject.transform);
    //         chasing = true;
    //         Debug.Log(chasing);
    //     }
    // }

    // void OnTriggerExit(Collider other){
    //     chasing = false;
    //     Debug.Log(chasing);
    // }




}
