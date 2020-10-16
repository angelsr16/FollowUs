﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Main gameController;
    public Vector2Int currentLocation;

    public Animator animator;

    public float speed;

    public Rigidbody2D rb;
    private Vector2 moveVelocity;
    private Vector2 mousePos;

    public Camera mainCamera;

    public GameObject gun;
    private int flipGunValue;

    public int lifePoints;
    public Image[] lifesImages = new Image[3];


    void Start()
    {
        lifePoints = 3;
        rb = GetComponent<Rigidbody2D>();
        flipGunValue = 1;
    }

    
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x > 0) flipPlayer(-1);
        if (moveInput.x < 0) flipPlayer(1);
        if (mousePos.x > transform.position.x) flipPlayer(-1);
        if (mousePos.x < transform.position.x) flipPlayer(1);

        if (moveInput.x != 0 || moveInput.y != 0) animator.SetFloat("speed", 1);
        else animator.SetFloat("speed", 0);

    }

    private void flipPlayer(int flip)
    {
        transform.localScale = new Vector3(flip, 1, 1);
        flipGunValue = flip;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        gun.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle * flipGunValue));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Room"))
        {
            mainCamera.GetComponent<CameraFollow>().moveCamera(collision.transform.position);

            Room currentRoom = collision.gameObject.GetComponent<Room>();
            currentLocation = currentRoom.mapLocation;

            if(Vector2Int.Equals(currentLocation, gameController.currentActiveRoom))
            {
                gameController.setActiveAllUIArrows(true);
                gameController.updateUIArrows();
            }
            else
            {
                gameController.setActiveAllUIArrows(false);
            }
        }else if (collision.tag.Equals("Enemy"))
        {
            removeLifePoints(1);
        }
    }

    public void removeLifePoints(int points)
    {
        this.lifePoints -= points;
        updateHealthUI();
        if(lifePoints == 0)
        {
            //Kill player
        }
    }

    private void updateHealthUI()
    {
        for (int i = 0; i < lifesImages.Length; i++)
        {
            if (i >= lifePoints) lifesImages[i].enabled = false;
            else lifesImages[i].enabled = true;
        }
    }
}
