using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform segment;

    public List<Transform> body;
    private Vector2 _direction;

    private bool _gameOver;
    private bool newDirection;
    private int _score;
    public int initialSize;

    public bool gameOver{
        get {return _gameOver; }
        set { _gameOver = gameOver; }
    }
    public int score{
        get {return _score; }
        set { _score = score; }
    }

    //Start is called before the first frame update
    void Start()
    {
        _direction = Vector2.right;
        body = new List<Transform>();
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        if(newDirection) OnInput();
        
        if(_gameOver && Input.GetKeyDown(KeyCode.Space)){
            ResetState();
        }
    }

    private void FixedUpdate() {
        if(_gameOver) return;
        MoveBody();
        OnMove();
        newDirection = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Food"){
            Grow();
            _score++;
        }
        else if(other.tag == "Obstacle"){
            _gameOver = true;
        }
            
    }

    private void ResetState(){
        _gameOver = false;
        _score = 0;
        for(int i = 1; i < body.Count; i++){
            Destroy(body[i].gameObject);
        }

        body.Clear();
        body.Add(this.transform);

        transform.position = new Vector3(-10, 0.5f, 0);
        _direction = Vector2.right;

        for(int i = 1; i < initialSize; i++) Grow();

    }

    #region Movement

    void OnInput(){
        if(Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down){
            _direction = Vector2.up;
            newDirection = false;
        } 
        else if(Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up){
            _direction = Vector2.down; 
            newDirection = false;
        } 
        else if(Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right){
            _direction = Vector2.left;
            newDirection = false;
        } 
        else if(Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left){
            _direction = Vector2.right;
            newDirection = false;
        } 
    }

    void OnMove(){
        transform.position = new Vector3(
            transform.position.x + _direction.x,
            transform.position.y + _direction.y,
            0.0f
        );
    }
    #endregion

    #region Body

    void MoveBody(){
        for(int i = body.Count - 1; i > 0; i--)
            body[i].position = body[i-1].position;
    }

    void Grow(){
        Transform newSegment = Instantiate(segment);
        newSegment.position = body[body.Count - 1].position;
        body.Add(newSegment);
    }

    #endregion
}
