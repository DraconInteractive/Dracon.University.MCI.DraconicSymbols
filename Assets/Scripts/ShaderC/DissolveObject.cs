using UnityEngine;

public class DissolveObject : MonoBehaviour
{
    public bool defaultState;
    public Vector2 minMax;
    public Renderer[] renderers;
    float progress;
    float progressTarget;
    public float step = 1;

    private void Start()
    {
        if (renderers == null || renderers.Length == 0)
        {
            this.enabled = false;
        }
        SetProgress(defaultState ? minMax.y : minMax.x, true);
    }

    private void Update()
    {
        if (progress != progressTarget)
        {
            SetHeight();
            progress = Mathf.MoveTowards(progress, progressTarget, step * Time.deltaTime);
        }
    }

    public void SetProgress (bool max)
    {
        SetProgress(max ? minMax.y : minMax.x);
    }

    public void SetProgress (float _progress)
    {
        SetProgress(_progress, false);
    }

    public void SetProgress (float _progress, bool direct = false)
    {
        progressTarget = _progress;

        if (direct)
        {
            progress = _progress;
            SetHeight();
        }
    }

    private void SetHeight ()
    {
        var height = transform.position.y + progress;
        foreach (var r in renderers)
        {
            r.material.SetFloat("_CutoffHeight", height);
        }
    }

    private void SetHeight(float height)
    {
        foreach (var r in renderers)
        {
            r.material.SetFloat("_CutoffHeight", height);
        }
    }
}