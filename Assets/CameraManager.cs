using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform reference; // Takip edilecek nesne
    [SerializeField] private float followSpeed = 2f; // Takip h�z�
    [SerializeField] private float yOffset = 10f; // Kameran�n referansa g�re sabit y offseti

    private void LateUpdate()
    {
        // Kameran�n hedef pozisyonunu hesapla
        Vector3 targetPosition = new Vector3(transform.position.x, reference.position.y + yOffset, transform.position.z);

        // Kameray� hedef pozisyona do�ru hareket ettir
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
