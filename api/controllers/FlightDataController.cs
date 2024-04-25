using Microsoft.AspNetCore.Mvc;
using Microsoft.FlightSimulator.SimConnect;

using System;

[ApiController]
[Route("[controller]")]
public class FlightDataController : ControllerBase
{
    private readonly SimConnect _simConnect;

    public FlightDataController(SimConnect simConnect)
    {
        _simConnect = simConnect;
        _simConnect.OnRecvSimobjectData += OnRecvSimobjectData;
        _simConnect.SubscribeToSystemEvent(EventGroups.Group0, Enum.GetName(typeof(EventID), EventID.SIM_START));
    }

    [HttpGet]
    public IActionResult GetFlightData()
    {
        // Request flight data from MSFS
        _simConnect.RequestDataOnSimObject(DataRequestID.FlightData, 
                                           DefinitionID.FlightData, 
                                           SimConnect.SIMCONNECT_OBJECT_ID_USER,
                                           SIMCONNECT_PERIOD.SIMCONNECT_PERIOD_ONCE);

        // Return a temporary response until actual data is received
        return Ok("Waiting for flight data...");
    }

    private void OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
    {
        if (data.dwRequestID == (uint)DataRequestID.FlightData)
        {
            // Extract flight data from received SimConnect data
            var altitude = (double)data.dwData[0];
            var speed = (double)data.dwData[1];
            var heading = (double)data.dwData[2];
            
            // Create FlightData object with received data
            var flightData = new FlightData
            {
                Altitude = altitude,
                Speed = speed,
                Heading = heading
            };

            // Return flight data
            Ok(flightData);
        }
    }
}

public class FlightData
{
    public double Altitude { get; set; }
    public double Speed { get; set; }
    public double Heading { get; set; }
}
