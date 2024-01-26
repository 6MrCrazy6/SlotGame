using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelSlots : MonoBehaviour
{
    [HideInInspector] public float timeInterval;

    private int randomValue;
    private float startSpeed;
    private float speed;

    public pixelSlotValue stoppedSlot;
    private PixelSlotMachine sm;

    private void Start()
    {
        sm = gameObject.GetComponentInParent<PixelSlotMachine>();
    }

    public IEnumerator Spin()
    {
        timeInterval = sm.timeInterval;
        randomValue = Random.Range(0, 90);
        speed = 30f + randomValue;
        while (speed >= 10f)//for (int i = 0; i < 30+randomValue; i++)
        {
            speed = speed / 1.01f;
            transform.Translate(Vector2.up * Time.deltaTime * -speed);
            if (transform.localPosition.y <= -2.5f)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, 2.17f);
            }

            yield return new WaitForSeconds(timeInterval);
        }
        StartCoroutine("EndSpin");
        yield return null;
    }

    private IEnumerator EndSpin()
    {
        while (speed >= 2f)
        {
            float targetY = GetTargetY();

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(transform.localPosition.x, targetY), speed * Time.deltaTime);

            if (Mathf.Approximately(transform.localPosition.y, targetY))
            {
                speed = 0;
            }

            speed = speed / 1.01f;
            yield return new WaitForSeconds(timeInterval);
        }

        speed = 0;
        CheckResults();
        yield return null;
    }

    private void CheckResults()
    {
        if (transform.localPosition.y == -2.39f)
        {
            stoppedSlot = pixelSlotValue.Bell;
        }
        else if (transform.localPosition.y == -1.06f)
        {
            stoppedSlot = pixelSlotValue.Seven;
        }
        else if (transform.localPosition.y == 0.6f)
        {
            stoppedSlot = pixelSlotValue.Cherry;
        }
        else if (transform.localPosition.y == 2.3f)
        {
            stoppedSlot = pixelSlotValue.Bar;
        }

        sm.WaitResults();
    }

    private float GetTargetY()
    {
        if (transform.localPosition.y < -2.37f)
        {
            return -2.37f;
        }
        else if (transform.localPosition.y < -1.06f)
        {
            return -1.06f;
        }
        else if (transform.localPosition.y < 0.6f)
        {
            return 0.6f;
        }
        else if (transform.localPosition.y < 2.3f)
        {
            return 2.3f;
        }

        return 0f;
    }
}
