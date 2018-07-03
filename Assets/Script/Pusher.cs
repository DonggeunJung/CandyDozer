using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {
    Vector3 startPosition;

    public float amplitude;    // 이동량 파라미터
    public float speed;        // 이동 속도 파라미터

	// Use this for initialization
	void Start () {
        startPosition = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        // 변위를 계산
        float z = amplitude * Mathf.Sin(Time.time * speed);

        // Z를 변위시킨 포지션으로 재설정
        transform.localPosition = startPosition + new Vector3(0, 0, z);
    }
}
