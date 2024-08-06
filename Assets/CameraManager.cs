using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Buton için gerekli

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float time = 1f; // Hareket süresi
    [SerializeField] private AnimationCurve curve; // Hareket eðrisi
    [SerializeField] private float upValue = 1f; // Y ekseninde çýkma miktarý
    [SerializeField] private Button sayButton; // Buton referansý

    private void Start()
    {
        if (sayButton != null)
        {
            sayButton.onClick.AddListener(OnSayButtonClick);
        }
        else
        {
            Debug.LogError("SayButton referansý atanmadý!");
        }
    }

    private void OnSayButtonClick()
    {
        Up(); // Kamera yukarý hareket ettiriliyor
    }

    public void Up()
    {
        StopAllCoroutines();
        var target = transform.position;
        target.y += upValue; // Kameranýn yukarý çýkacaðý miktar
        StartCoroutine(Move(transform, target, time, curve));
    }

    private IEnumerator Move(Transform current, Vector3 target, float time, AnimationCurve curve)
    {
        var passed = 0f;
        var init = current.position;

        while (passed < time)
        {
            passed += Time.deltaTime;
            var normalized = passed / time;
            normalized = curve.Evaluate(normalized);

            // Y eksenindeki hareketi iþleyin, X ve Z eksenlerini deðiþtirmeyin
            var newPosition = new Vector3(init.x, Mathf.Lerp(init.y, target.y, normalized), init.z);
            current.position = newPosition;

            yield return null;
        }
    }
}
