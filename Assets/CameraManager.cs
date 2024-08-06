using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Buton i�in gerekli

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float time = 1f; // Hareket s�resi
    [SerializeField] private AnimationCurve curve; // Hareket e�risi
    [SerializeField] private float upValue = 1f; // Y ekseninde ��kma miktar�
    [SerializeField] private Button sayButton; // Buton referans�

    private void Start()
    {
        if (sayButton != null)
        {
            sayButton.onClick.AddListener(OnSayButtonClick);
        }
        else
        {
            Debug.LogError("SayButton referans� atanmad�!");
        }
    }

    private void OnSayButtonClick()
    {
        Up(); // Kamera yukar� hareket ettiriliyor
    }

    public void Up()
    {
        StopAllCoroutines();
        var target = transform.position;
        target.y += upValue; // Kameran�n yukar� ��kaca�� miktar
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

            // Y eksenindeki hareketi i�leyin, X ve Z eksenlerini de�i�tirmeyin
            var newPosition = new Vector3(init.x, Mathf.Lerp(init.y, target.y, normalized), init.z);
            current.position = newPosition;

            yield return null;
        }
    }
}
