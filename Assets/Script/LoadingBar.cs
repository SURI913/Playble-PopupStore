using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public GameObject character;

    private void Start(){
        StartTestLoading();
    }
    public void LoadScene(string sceneName){
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously (string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName); // 씬 비동기 호출

        loadingScreen.SetActive(true);
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f); // 0~1의 값을 갖도록 옵티마이징

            slider.value = progress; // 슬라이더의 값은 progress 값으로 변환
            progressText.text = progress * 100f + "%"; 

            Vector3 characterPos = character.transform.localPosition; // 벡터 객체를 하나 생성
            // 캐릭터의 위치는 슬라이더의 x축과 캐릭터의 x축을 합하여 설정
            characterPos.x = slider.fillRect.localPosition.x + slider.fillRect.rect.width;
            // 갱신 
            character.transform.localPosition = characterPos;

            yield return null;
        }
    }

    ///////////// 임시 테스트 코드 /////////////////
    public void StartTestLoading(){
        StartCoroutine(TestLoadProgress());
    }

    IEnumerator TestLoadProgress(){
        loadingScreen.SetActive(true);

        float duration = 30f; // 10초 동안 로딩
        float startTime = Time.time;

        while(Time.time - startTime < duration){
            float progress = (Time.time - startTime) / duration;
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";

            Vector3 characterPos = character.transform.localPosition;
            characterPos.x = slider.fillRect.localPosition.x + slider.fillRect.rect.width;
            character.transform.localPosition = characterPos;

            yield return null;
        }

        // 로딩 완료 후 처리
        slider.value = 1f;
        progressText.text = "100%";
        loadingScreen.SetActive(false);
    }
}
