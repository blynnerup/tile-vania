using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class BounceOnlyOnce : MonoBehaviour
{
    private PolygonCollider2D _collider2D;
    private void Awake()
    {
        _collider2D = GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        bool isPlayer = col.gameObject.GetComponent<PlayerMovement>();
        if (!isPlayer) return;
        ContactPoint2D[] allPoints = new ContactPoint2D[col.contactCount];
        col.GetContacts(allPoints);

        foreach (var i in allPoints)
        {
            // Debug.Log(i.point);
            if (i.point.x > Mathf.Floor(transform.position.x))
            {
                Debug.Log(Mathf.Floor(transform.position.x));
                StartCoroutine(NoBounceRoutine());                
            }
        }
        
    }

    private IEnumerator NoBounceRoutine()
    {
        yield return new WaitForSeconds(.2f);
        _collider2D.enabled = false;
        yield return new WaitForSeconds(1f);
        _collider2D.enabled = true;
    }
}
