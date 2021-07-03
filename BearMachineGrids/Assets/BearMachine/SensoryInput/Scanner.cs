using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bear
{
    [ExecuteInEditMode]
    public class Scanner : MonoBehaviour
    {
        public bool isScanning;
        public float degree;
        public float radius;
        public int count;

        public LayerMask targetLayer;
        public LayerMask occlusion;
        public Collider[] colliders = new Collider[50];

        public List<Collider> visible = new List<Collider>();

        [Header("+++Scan Frequency+++")]
        public int scanFrequency;
        private float scanInterval;
        private float scanTimer;

        private void Start()
        {
            scanInterval = 1.0f / scanFrequency;
        }

        public void Update()
        {
            if (!isScanning) {
                return;
            }

            scanTimer -= Time.deltaTime;
            if (scanTimer <0) {
                scanTimer += scanInterval;
                Scan();
            }
        }

        private void Scan() {
            count = Physics.OverlapSphereNonAlloc(transform.position,radius,colliders,targetLayer,QueryTriggerInteraction.Collide);

            visible.Clear();
            for (int i =0;i < count; i++) {
                Collider col = colliders[i];
                if (IsVisible(col)) {
                    visible.Add(col);
                }
                
            }
        }

        private bool IsVisible(Collider collider) {
            if (Physics.Linecast(transform.position, collider.transform.position, occlusion)) {
                return false;
            }
            return true;
        }
    }

    
}