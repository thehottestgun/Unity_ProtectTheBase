using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region VARIABLES
    public float ROTATE_SPEED;
    public float SHOOT_SPEED;

    public float movementInput;

    private static PlayerData _pd;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private TMPro.TextMeshProUGUI _HealthUI;
    #endregion
    void Start()
    {
        _pd = PlayerData.instance;
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _HealthUI = GameObject.Find("UI").transform.GetChild(0).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
        _HealthUI.text = _pd.health.ToString();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        movementInput = Input.GetAxis("Horizontal");

        this.Move(movementInput);
    }

    void Move(float direction)
    {
        if (direction == 0) return;
        this._rb.rotation += direction * ROTATE_SPEED * -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _pd.TakeDamage(3);
            Destroy(collision.gameObject);
        }
    }
}
