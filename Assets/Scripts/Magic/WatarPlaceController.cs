using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatarPlaceController : MonoBehaviour
{
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.gameObject.transform.position;
        pos.y += pos.y + 0.5f;
        this.gameObject.transform.position = pos;
        this.gameObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var Enemy = other.gameObject.GetComponent<IDamage>();
        if(Enemy != null)
        {
            List<Type> types = new List<Type> { Type.Water };
            Enemy.ApplyDamage(0f, types);
            
        }

        var player = other.gameObject.GetComponent<IPlayerDamage>();
        if(player != null)
        {
            List<Attacks> attacks = new List<Attacks> { Attacks.Water };
            player.ApplyDamage(0f, attacks);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

}
