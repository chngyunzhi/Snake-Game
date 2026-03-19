using System;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segment;
    public Transform segmentPrefab;

    private void Start()
    {
        _segment = new List<Transform>();
        _segment.Add(this.transform);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            _direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.D))
            _direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.A))
            _direction = Vector2.left;
    }

    private void FixedUpdate()
    {
        for (int i = _segment.Count - 1;i > 0 ; i--)
        {
            _segment[i].position = _segment[i-1].position;    
        }

        this.transform.position = new Vector3
            (
            Mathf.Round(this.transform.position.x ) + _direction.x,
            Mathf.Round(this.transform.position.y ) + _direction.y,
            0.0f
            );
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segment[_segment.Count - 1].position;    
        _segment.Add (segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segment.Count; i++)
        {
            Destroy(_segment[i].gameObject);
        }

        _segment.Clear();
        _segment.Add(this.transform);

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
            Grow();
        else if (collision.tag == "obstacles")
            ResetState();
    }

}
