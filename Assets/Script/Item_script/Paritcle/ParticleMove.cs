using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    private Vector3 playerVector;

    public ParticleSystem customParticleSystem;
    public Transform player;
    public float delay = 1f;        //코인이 떨어지고 몇초 대기하는지
    public float moveTime = 3f;     //코인이 이동하는 시간
    public float acceleration = 0.1f;   //코인의 가속도

    private ParticleSystem.Particle[] particlesArray;   //떨어진 파티클들

    //---코인 폭팔
    public GameObject particlePrefab;
    private GameObject particleInstance;

    private void Start()
    {
        customParticleSystem = GetComponent<ParticleSystem>();
        player = GameObject.Find("Player").GetComponent<Player>().playerRigidbody.transform;

        StartCoroutine(MoveParticlesTowardsPlayer());
    }

    IEnumerator MoveParticlesTowardsPlayer()
    {
        yield return new WaitForSeconds(delay);         //몇초간 대기

        particlesArray = new ParticleSystem.Particle[customParticleSystem.main.maxParticles];       //생성된 파티클들을 배열로 집어넣음
        int numParticlesAlive = customParticleSystem.GetParticles(particlesArray);                  //나온 파티클들의 개수를 나타냄

        float elapsedTime = 0f;      //3초가 되면 밑에 while문을 탈출함
        float moveSpeed = 5f;

        while (elapsedTime < moveTime)
        {
            moveSpeed += acceleration;              //코인의 이동속도 증가
            playerVector = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                Vector3 directionToPlayer = (playerVector - particlesArray[i].position).normalized;
                float distanceToPlayer = Vector3.Distance(playerVector, particlesArray[i].position);

                if (distanceToPlayer > 0.5f)
                {
                    particlesArray[i].position += directionToPlayer * moveSpeed * Time.deltaTime; // 이동 속도 조절
                }
                else
                {
                    if (particlesArray[i].remainingLifetime > 0f) // 이미 0으로 설정되지 않은 파티클인지 확인
                    {
                        particlesArray[i].remainingLifetime = 0f; // 파티클을 사라지게 함

                        //추가 파티클 생성
                        particleInstance = Instantiate(particlePrefab, playerVector, Quaternion.identity);
                        ParticleSystem.EmissionModule emissionModule = particleInstance.GetComponent<ParticleSystem>().emission;
                        // 5초 후 파티클 인스턴스 소멸
                        Destroy(particleInstance, 1f);
                    }
                }
            }

            customParticleSystem.SetParticles(particlesArray, numParticlesAlive);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

   /* void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello");
        if (other.CompareTag("Player")*//* && particleInstance != null*//*)
        {
            Debug.Log("hello");
            Destroy(particleInstance);
        }
    }*/
}