﻿using MrPP.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Helper {
    public class Gaze : GDGeek.Singleton<Gaze>
    {
        [SerializeField]
        private Camera _camera = null;

        [SerializeField]
        [Tooltip("The layers that can be tapped on for objects to center the camera on them")]
        private LayerMask _tapToCenterLayerMask = -1;

        [SerializeField]
        private GameObject _focus = null;
        [SerializeField]
        private GameObject _normal = null;
        [SerializeField]
        private Inputable target_ = null;
        void Awake() {
            _focus.SetActive(false);
            _normal.SetActive(true);
        }
        void Update() {


            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
           // _normal.transform.position = ray.GetPoint(-1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, _tapToCenterLayerMask))
            {

                _focus.transform.position = hit.point;
                _focus.transform.LookAt(hit.normal);
                // Inputable
                Inputable target = hit.collider.gameObject.GetComponent<Inputable>();

                if (target == null && target_ !=null) {
                    doChange(target_, null);
                    target_ = null;
                }else if (target_ != target) {

                    doChange(target_, target);
                    target_ = target; 
                }

              

            }
            else {
                if (target_ != null) {
                    doChange(target_, null);
                    target_ = null;
                }


            }
         
        }

        private void doChange(Inputable oldValue, Inputable newValue)
        {

            if (newValue == null)
            {
                _focus.SetActive(false);
                _normal.SetActive(true);
            }
            else {
                _focus.SetActive(true);
                _normal.SetActive(false);
            }
            //oldValue?.focusEnter();
           // oldValue?.focusExit();

          
        }
    }
}