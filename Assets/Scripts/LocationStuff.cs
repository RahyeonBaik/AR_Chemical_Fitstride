using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class LocationStuff : MonoBehaviour
{
    [SerializeField] private char unit = 'K';

    public TMP_Text debugText;
    public bool gps_ok = false;
    private float PI = Mathf.PI;

    private GPSLocation startLocation = new GPSLocation();
    private GPSLocation currentLocation = new GPSLocation();

    private bool measureDistance = false;

// 37.484446, 126.929709 StartLocation

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location not enabled on Device");
            debugText.text = "Location not enabled on Device";
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            debugText.text += "\nTimed out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            debugText.text += "\nUnable to determine device location";
            yield break;
        }
        else
        {
            Debug.Log("Location : " + Input.location.lastData.latitude +
                      ", " + Input.location.lastData.longitude);
            debugText.text =
                "\nLocation : \n Lat : " + Input.location.lastData.latitude
                                         + "\nLon : " + Input.location.lastData.longitude
                                         + "\nAlt : " + Input.location.lastData.altitude
                                         + "\nH_ACC : " + Input.location.lastData.horizontalAccuracy
                                         + "\nTime : " + Input.location.lastData.timestamp;
            gps_ok = true;
        }
    }

    private void Update()
    {
        if (gps_ok)
        {
            debugText.text = "GPS : ...";

            debugText.text
                = "\nLocation : \nLat : " + Input.location.lastData.latitude
                                          + "\nLon : " + Input.location.lastData.longitude
                                          + "\nH_Acc : " + Input.location.lastData.horizontalAccuracy;

            currentLocation.lat = Input.location.lastData.latitude;
            currentLocation.lon = Input.location.lastData.longitude;

            debugText.text += "\nHome : " + startLocation.GetLocationData();

            if (measureDistance)
            {
                double distanceBetween = distance(currentLocation.lat, startLocation.lon,
                    startLocation.lat, startLocation.lon, 'K');
                debugText.text += "\nDistance : " + distanceBetween;
            }
        }
    }

    public void StopGPS()
    {
        Input.location.Stop();
    }

    public void HomeCurrentGPS()
    {
        startLocation = new GPSLocation(currentLocation.lat, currentLocation.lon);
        measureDistance = true;
    }

    private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
                          Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));

            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;


            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }

            return dist;
        }
    }

    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    private double rad2deg(double rad)
    {
        return (rad / 180 * Math.PI);
    }
}

public class GPSLocation
{
    public float lon;
    public float lat;

    public GPSLocation()
    {
        lon = 0;
        lat = 0;
    }

    public GPSLocation(float lon, float lat)
    {
        this.lon = lon;
        this.lat = lat;
    }

    public string GetLocationData()
    {
        return string.Format("lat : {0}\nlon : {1}", lon, lat);
    }
}