using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour {
    public CandyHoler candyHolder;
    public int reward;
    public GameObject effectPrefab;
    public Vector3 effectRotation;

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            // 지정한 수만큼함 Candy의 투척 기회 수를 늘린다
            candyHolder.AddCandy(reward);

            // 오브젝트 삭제
            Destroy(other.gameObject);

            if(effectPrefab != null)
            {
                // Candy의 포지션에 효과를 생성
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRotation)
                    );
            }
        }
    }
}
