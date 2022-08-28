using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Stick
{
    public class StickTouch : MonoBehaviour
    {
        public bool IsCutting { get; set; }=false;
        private Rigidbody2D rb;
        private Camera cam;
        public GameObject Trail;
        private Vector2 previousposition;
        private float minveloc = 0.001f;
        CircleCollider2D circlecollider;
        void Start ()
        {
            circlecollider = GetComponent<CircleCollider2D>();
            cam = Camera.main;
            rb = GetComponent<Rigidbody2D>();
            circlecollider.enabled = false;
        }

        // Update is called once per frame
        void Update () {
            if (Input.GetMouseButtonDown(0))
            {
                StartCutting();
            } else if (Input.GetMouseButtonUp(0))
            {
                StopCutting();
            }

            if (IsCutting)
            {
                UpdateCut();
            }
        }
        void UpdateCut ()
        {
            Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var veloc = (newPosition - previousposition).magnitude * Time.deltaTime;
            if (veloc>minveloc)
            {
                Trail.gameObject.SetActive(true);
            }
            rb.position = newPosition;
        }
        void StartCutting ()
        {
            circlecollider.enabled = true;
            IsCutting = true;
            previousposition = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        void StopCutting ()
        {
            circlecollider.enabled = false;
            Trail.gameObject.SetActive(false);
            IsCutting = false;
        }
       
    }
}