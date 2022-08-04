using UnityEngine;

namespace UnityMovementAI
{
    public class SeekUnit : MonoBehaviour
    {
        public Vector3 targetPos;

        SteeringBasics steeringBasics;
        public float seekAcceleration;
        public Vector2 randomTimeSeek;
        Vector3 accel;

        float timeSeek;
        void Start()
        {
            timeSeek = Random.Range(randomTimeSeek.x, randomTimeSeek.y) + Time.time;
            steeringBasics = GetComponent<SteeringBasics>();
            accel = new Vector3(0, 0, 0);
            targetPos = new Vector3(0, 30f, 0);
        }

        void FixedUpdate()
        {
            if (Time.time > timeSeek)
            {
                accel = steeringBasics.Seek(targetPos, seekAcceleration);
            }
            else
            {
                accel = steeringBasics.Arrive(transform.position * 4);
            }
            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}