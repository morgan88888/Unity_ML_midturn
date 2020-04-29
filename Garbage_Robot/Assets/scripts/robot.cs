using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class robot : Agent
{
    [Header("速度"), Range(1, 50)]
    public float speed = 10;

    private Rigidbody rigRobot;

    private Rigidbody rigcan;

    private Rigidbody rigpack;

    private Rigidbody rigpaper;

    private void Start()
    {
        rigRobot = GetComponent<Rigidbody>();
        rigcan = GameObject.Find("可樂罐").GetComponent<Rigidbody>();
        rigpack = GameObject.Find("牛奶包").GetComponent<Rigidbody>();
        rigpaper = GameObject.Find("紙").GetComponent<Rigidbody>();
    }


    public override void OnEpisodeBegin()
    {
        rigRobot.velocity = Vector3.zero;
        rigRobot.angularVelocity = Vector3.zero;
        rigcan.velocity = Vector3.zero;
        rigcan.angularVelocity = Vector3.zero;
        rigpack.velocity = Vector3.zero;
        rigpack.angularVelocity = Vector3.zero;
        rigpaper.velocity = Vector3.zero;
        rigpaper.angularVelocity = Vector3.zero;

        Vector3 posRobot = new Vector3(Random.Range(0.5f, -0.5f), 0.1f, Random.Range(-1f, -1.8f));
        transform.position = posRobot;

        Vector3 poscan = new Vector3(Random.Range(1.3f, -1.3f), 0.1f, Random.Range(0f, 0f));
        rigcan.position = poscan;

        Vector3 pospack = new Vector3(Random.Range(1.3f, -1.3f), 0.1f, Random.Range(0f, 0f));
        rigpack.position = pospack;

        Vector3 pospaper = new Vector3(Random.Range(1.3f, -1.3f), 0.1f, Random.Range(0f, 0f));
        rigpaper.position = pospaper;

        trash.complete = false;
        Ftrash.complete = false;
        FFtrash.complete = false;
    }

    /// <summary>
    /// 收集觀測資料
    /// </summary>
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigcan.position);
        sensor.AddObservation(rigRobot.velocity.x);
        sensor.AddObservation(rigRobot.velocity.z);
    }

    /// <summary>
    /// 動作 : 控制機器人與回饋
    /// </summary>
    /// <param name="vectorAction"></param>
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigRobot.AddForce(control * speed);

        if (trash.complete)
        {
            SetReward(1);
            EndEpisode();
        }

        if (Ftrash.complete)
        {
            SetReward(-1);
            EndEpisode();
        }

        if (FFtrash.complete)
        {
            SetReward(-1);
            EndEpisode();
        }

        if (transform.position.y < -1 || rigcan.position.y < -1)

        {
            SetReward(-1);
            EndEpisode();
        }


    }

    /// <summary>
    /// 探索 : 讓開發者測試環境
    /// </summary>
    /// <returns></returns>
    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
