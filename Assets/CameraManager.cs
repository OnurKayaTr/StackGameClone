using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform reference; // Takip edilecek nesne
    [SerializeField] private float followSpeed = 2f; // Takip hýzý
    [SerializeField] private float yOffset = 10f; // Kameranýn referansa göre sabit y offseti

    private void LateUpdate()
    {
        // Kameranýn hedef pozisyonunu hesapla
        Vector3 targetPosition = new Vector3(transform.position.x, reference.position.y + yOffset, transform.position.z);

        // Kamerayý hedef pozisyona doðru hareket ettir
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
