using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Collider2D _attackPoint;

    private float _delay = 0.5f;

    public void Attacking()
    {
        _attackPoint.gameObject.SetActive(true);

        StartCoroutine(DisableAttack());
    }

    private IEnumerator DisableAttack()
    {
        yield return new WaitForSeconds(_delay);

        _attackPoint.gameObject.SetActive(false);
    }
}