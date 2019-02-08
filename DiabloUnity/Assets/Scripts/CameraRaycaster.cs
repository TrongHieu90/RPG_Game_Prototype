﻿using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    private Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    float distanceToBackground = 100f;
    Camera viewCamera;

    RaycastHit m_hit;
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    public delegate void OnLayerChange(Layer newLayer);
    public event OnLayerChange layerChangeObservers;



    void Start() 
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                if(m_layerHit != layer)
                {
                    m_layerHit = layer;

                    layerChangeObservers?.Invoke(layer); //call all the delegates if not null

                }
                return;
            }

            
        }

        // Otherwise return background hit
        m_hit.distance = distanceToBackground;
        if (Layer.RaycastEndStop != m_layerHit)
        {
            m_layerHit = Layer.RaycastEndStop;

            layerChangeObservers?.Invoke(layerHit);
        }
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
