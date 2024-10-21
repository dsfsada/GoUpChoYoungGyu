using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;
using static UnityEngine.ParticleSystem;

//에너미 오브젝트의 스크립트 부여되어있음
public class Pa : MonoBehaviour
{
    // 파티클 프리팹에 대한 참조. Inspector에서 할당할 수 있습니다.
    public GameObject particlePrefab;
    private GameObject particleInstance;

    private float coinCount;


    void Die()
    {
        // 코인 개수 계산 (최대 5)
        coinCount = GameObject.Find("Statuse").GetComponent<Statuse>().floor;
        if (coinCount > 5) { coinCount = 5; }

        // 파티클 프리팹 인스턴스화
        particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // 파티클 시스템의 Emission 모듈 가져오기
        ParticleSystem.EmissionModule emissionModule = particleInstance.GetComponent<ParticleSystem>().emission;

        // 모든 기존 Burst를 제거
        emissionModule.burstCount = 0;

        // 새 Burst 추가, coinCount개의 파티클 발생
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0.0f, (short)coinCount); // short 타입 사용
        emissionModule.SetBursts(new ParticleSystem.Burst[] { burst });

        // 파티클 수에 맞게 배열 초기화
       /* coinParticles = new ParticleSystem.Particle[particleInstance.GetComponent<ParticleSystem>().main.maxParticles];*/

        // 5초 후 파티클 인스턴스 소멸
        Destroy(particleInstance, 4f);
    }

/*    private void Update()
    {

        if (particleInstance != null || setCoin)
        {
            setCoin = true; 
            ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();

            // 현재 파티클 시스템에서 파티클 가져오기
            int particleCount = ps.GetParticles(coinParticles);

            // 파티클의 위치를 이동
            for (int i = 0; i < particleCount; i++)
            {
                Vector3 direction = (player.transform.position - coinParticles[i].position).normalized;
                coinParticles[i].position += direction  * speed * Time.deltaTime; // 좌측으로 이동
            }

            // 수정된 파티클 배열을 다시 설정
            ps.SetParticles(coinParticles, particleCount);
        }
    }
*/
    /*void OnTriggerEnter(Collider other)
    {
        // �÷��̾�� �浹 �� ��ƼŬ �Ҹ�
        if (other.CompareTag("Player"))
        {
            Destroy(particleInstance);
        }
    }*/

    /*    void Die()
        {
            float coinCount = GameObject.Find("Statuse").GetComponent<Statuse>().floor;
            if (coinCount > 5) { coinCount = 5; }
            //������ ������ ����

            // ��ƼŬ ������ �ν��Ͻ�ȭ
            GameObject particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);

            // ��ƼŬ �ý����� Emission ��� ��������
            ParticleSystem.EmissionModule emissionModule = coinParticle.emission;

            // ��� ���� Burst�� ����
            emissionModule.burstCount = 0;

            // �� Burst �߰�, coinCount���� ��ƼŬ �߻�
            ParticleSystem.Burst burst = new ParticleSystem.Burst(0.0f, coinCount);
            emissionModule.SetBursts(new ParticleSystem.Burst[] { burst });

            // ���� �ð� �� ��ƼŬ �ν��Ͻ� �Ҹ�
            Destroy(particleInstance, 4f);

            StartCoroutine(MoveTowardsPlayer(particleInstance));    //�÷��̾ �i�ư��� ��ƼŬ �ڷ�ƾ Ȱ��ȭ
        }

        private static ParticleSystem.EmissionModule GetEmission(ParticleSystem coinParticle1)
        {
            return coinParticle1.emission;
        }*/

    /*IEnumerator MoveTowardsPlayer(GameObject particleInstance)
    {
        //yield return new WaitForSeconds(0.2f);

        while (particleInstance != null)
        {
            Debug.Log("hello");
            // �÷��̾ ������ �� �÷��̾ ���� �̵�
            if (player != null)
            {
                Debug.Log("��");
                particleInstance.transform.position = Vector3.MoveTowards(
                    particleInstance.transform.position,
                    player.transform.position,
                    particleMoveSpeed * Time.deltaTime
                );
            }
            yield return null;
        }
    }

   */
}
