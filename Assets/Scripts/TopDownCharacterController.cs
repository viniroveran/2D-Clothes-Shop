using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;
        private float scaleX;
        private float scaleY;
        private float scaleZ;

        private void Start()
        {
            animator = GetComponent<Animator>();
            scaleX = transform.localScale.x;
            scaleY = transform.localScale.y;
            scaleZ = transform.localScale.z;
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            animator.SetBool("isRun", false);

            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                transform.localScale = new Vector3(dir.x * scaleX, scaleY, scaleZ);
                animator.SetBool("isRun", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                transform.localScale = new Vector3(dir.x * scaleX, scaleY, scaleZ);
                animator.SetBool("isRun", true);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetBool("isRun", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetBool("isRun", true);
            }

            dir.Normalize();

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }
}
