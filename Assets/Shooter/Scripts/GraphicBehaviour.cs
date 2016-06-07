using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;

public class GraphicBehaviour : MonoBehaviour
{

    public GameObject Bullet = null;
    public Camera GraphCamera = null;
    public FirstPersonController Player = null;

    public LineChart SpeedChart = null;
    public LineChart XSpeedChart = null;
    public LineChart YSpeedChart = null;
    public LineChart ZSpeedChart = null;

    private List<Vector3> speeds;
    private bool isRendered = false;
    private Vector3 startSpeed = Vector3.zero;
    
    public void Init()
    {
        this.isRendered = false;
        this.speeds = new List<Vector3>();
        var flightBehaviour = Bullet.GetComponent<FlightBehaviour>();
        if (flightBehaviour != null)
            this.startSpeed = flightBehaviour.VSpeed;
    }

    private void Start()
    {
        Init();
    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O) && this.GraphCamera != null)
        {
            this.GraphCamera.enabled = !this.GraphCamera.enabled;
            this.Player.enabled = !this.Player.enabled;
        }

        if (!this.Bullet.activeInHierarchy)
        {
            if (!this.isRendered)
                this.Render();
        }
        else
        {
            var flightBehaviour = Bullet.GetComponent<FlightBehaviour>();
            if (flightBehaviour != null)
                this.speeds.Add(flightBehaviour.VSpeed);
        }
    }

    private void Render()
    {
        var max = this.speeds.Count;
        Debug.LogWarning("Render: " + max + "pt");
        float[] speeds = new float[max];
        float[] speedsX = new float[max];
        float[] speedsY = new float[max];
        float[] speedsZ = new float[max];
        for (int i = 0; i < max; i++)
        {
            speeds[i] = this.speeds[i].magnitude;
            speedsX[i] = this.startSpeed.x - this.speeds[i].x;
            speedsY[i] = this.startSpeed.y - this.speeds[i].y;
            speedsZ[i] = this.startSpeed.z - this.speeds[i].z;
        }
        var SpeedMMax = speeds.Max();
        var SpeedXMMax = speedsX.Max();
        var SpeedZMMax = speedsZ.Max();

        this.SpeedChart.mMax = SpeedMMax;
        this.SpeedChart.UpdateData(speeds);

        this.XSpeedChart.mMax = SpeedXMMax;
        this.XSpeedChart.UpdateData(speedsX);
        
        this.YSpeedChart.UpdateData(speedsY);

        this.ZSpeedChart.mMax = SpeedZMMax;
        this.ZSpeedChart.UpdateData(speedsZ);

        this.isRendered = true;
    }
}
