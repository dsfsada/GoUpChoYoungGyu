using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    private Vector3 playerVector;

    public ParticleSystem customParticleSystem;
    public Transform player;
    public float delay = 1f;        //������ �������� ���� ����ϴ���
    public float moveTime = 3f;     //������ �̵��ϴ� �ð�
    public float acceleration = 0.1f;   //������ ���ӵ�

    private ParticleSystem.Particle[] particlesArray;   //������ ��ƼŬ��

    //---���� ����
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
        yield return new WaitForSeconds(delay);         //���ʰ� ���

        particlesArray = new ParticleSystem.Particle[customParticleSystem.main.maxParticles];       //������ ��ƼŬ���� �迭�� �������
        int numParticlesAlive = customParticleSystem.GetParticles(particlesArray);                  //���� ��ƼŬ���� ������ ��Ÿ��

        float elapsedTime = 0f;      //3�ʰ� �Ǹ� �ؿ� while���� Ż����
        float moveSpeed = 5f;

        while (elapsedTime < moveTime)
        {
            moveSpeed += acceleration;              //������ �̵��ӵ� ����
            playerVector = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                Vector3 directionToPlayer = (playerVector - particlesArray[i].position).normalized;
                float distanceToPlayer = Vector3.Distance(playerVector, particlesArray[i].position);

                if (distanceToPlayer > 0.5f)
                {
                    particlesArray[i].position += directionToPlayer * moveSpeed * Time.deltaTime; // �̵� �ӵ� ����
                }
                else
                {
                    if (particlesArray[i].remainingLifetime > 0f) // �̹� 0���� �������� ���� ��ƼŬ���� Ȯ��
                    {
                        particlesArray[i].remainingLifetime = 0f; // ��ƼŬ�� ������� ��

                        //�߰� ��ƼŬ ����
                        particleInstance = Instantiate(particlePrefab, playerVector, Quaternion.identity);
                        ParticleSystem.EmissionModule emissionModule = particleInstance.GetComponent<ParticleSystem>().emission;
                        // 5�� �� ��ƼŬ �ν��Ͻ� �Ҹ�
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