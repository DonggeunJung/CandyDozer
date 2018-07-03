using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour {
    ParticleSystem particle;

	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        // 파티클의 재생이 끝나면 GameObject 를 삭제
        if (particle.isPlaying == false) Destroy(gameObject);
	}
}
