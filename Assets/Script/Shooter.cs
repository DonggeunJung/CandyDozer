using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    const int SphereCandyFrequency = 3;
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;
    int sampleCandyCount = 0;
    int shotPower = MaxShotPower;

    public GameObject[] candyPrefabs;
    public GameObject[] candySquarePrefabs;
    //public GameObject candyHolder;
    public CandyHoler candyHolder;
    public float shotSpeed;
    public float shotTorque;
    public float baseWidth;
    AudioSource shotSound;

    // Use this for initialization
    void Start () {
        shotSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        // 압력 감지
        if (Input.GetButtonDown("Fire1"))    // "Fire1" 은 기본 입력 명칭
            Shot();
	}

    // 사탕의 프리팹에서 임의로 1개 선택
    GameObject SampleCandy()
    {
        GameObject prefab = null;

        // 특정 주기로 한 번씩 둥근 사탕을 선택한다
        if(sampleCandyCount % SphereCandyFrequency == 0)
        {
            int index = Random.Range(0, candyPrefabs.Length);
            prefab = candyPrefabs[index];
        } else
        {
            int index = Random.Range(0, candySquarePrefabs.Length);
            prefab = candySquarePrefabs[index];
        }

        sampleCandyCount++;
        return prefab;
    }

    Vector3 GetInstantiatePosition()
    {
        // 화면의 사이즈와 Input의 비율로 부터 사탕 생성의 포지션을 계산
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }

    public void Shot()
    {
        // 사탕을 생성할 수 있는 조건을 벗어나면 Shot 하지 않는다
        if (candyHolder.GetCandyAmount() <= 0)
            return;
        if (shotPower <= 0) return;

        // 프리팹에서 Candy 오브젝트를 생성
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity    // 회전이 없음
            );

        // 생성한 Candy 오브젝트의 부모를 CandyHolder 에 설정한다
        candy.transform.parent = candyHolder.transform;

        // Candy 오브젝트의 Rigidbody 를 취득하여 힘과 회전을 더한다
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotSpeed);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        // Candy의 던질 회수를 소비
        candyHolder.ConsumeCandy();
        ConsumePower();

        // 사운드를 재생
        shotSound.Play();
    }

    void OnGUI()
    {
        GUI.color = Color.black;

        // ShotPower의 남은 수를 +의 수로 표시
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";

        GUI.Label(new Rect(0, 15, 100, 30), label);
    }

    void ConsumePower()
    {
        // ShotPower 를 소비하면 동시에 회복 카운터를 스타트
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower()
    {
        // 일정 시간(초 단위)을 기다린 후에 shotPower 를 회복
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
