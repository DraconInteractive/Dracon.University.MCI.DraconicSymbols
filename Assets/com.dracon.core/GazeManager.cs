using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dracon.Core
{
    public class GazeManager : MonoBehaviour
    {
        float progress;
        GazeButton current;
        bool clicked;

        // Update is called once per frame
        void Update()
        {
            if (GazeButton.All.Count == 0)
            {
                current = null;
                progress = 0;
                return;
            }
            Transform cam = Camera.main.transform;

            float min = 0;
            GazeButton closest = null;
            foreach (var button in GazeButton.All)
            {
                Vector3 dir = ((button.lookPoint == null) ? button.transform.position : button.lookPoint.position) - cam.position;
                dir.Normalize();
                float dot = Vector3.Dot(cam.forward, dir);
                if (dot > min && dot > button.minDot)
                {
                    closest = button;
                    min = dot;
                }
            }

            if (closest != null && closest == current)
            {
                if (!clicked)
                {
                    progress += Time.deltaTime / closest.lookDuration;
                    if (progress > 1)
                    {
                        progress = 0;
                        closest.onClick?.Invoke();
                        closest.onGazeUpdate?.Invoke(0);
                        clicked = true;
                    }
                    else
                    {
                        closest.onGazeUpdate?.Invoke(progress);
                    }
                }
            }
            else if (closest != null)
            {
                if (current != null)
                {
                    current.onGazeUpdate?.Invoke(0);
                }
                current = closest;
                progress = 0;
                clicked = false;
            }
            else
            {
                progress = 0;
                current = null;
            }
        }
    }
}

