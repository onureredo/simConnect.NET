import React, { useState, useEffect } from 'react';
import axios from 'axios';

const SDKTest = () => {
  const [connectionStatus, setConnectionStatus] = useState('Disconnected');
  const [flightData, setFlightData] = useState({});

  useEffect(() => {
    const fetchFlightData = async () => {
      try {
        setConnectionStatus('Connecting...');

        const response = await axios.get('http://localhost:5023/flightdata');
        setFlightData(response.data);
        setConnectionStatus('Connected');
      } catch (error) {
        console.error('Failed to connect to MSFS:', error);
        setConnectionStatus('Disconnected');
      }
    };

    fetchFlightData();

    return () => {};
  }, []);

  return (
    <div>
      <h1>MSFS Connection Test</h1>
      <p>Status: {connectionStatus}</p>
      <h2>Flight Data:</h2>
      {connectionStatus === 'Connected' && flightData ? (
        <ul>
          <li>Altitude: {flightData.altitude} feet</li>
          <li>Speed: {flightData.speed} knots</li>
          <li>Heading: {flightData.heading} degrees</li>
        </ul>
      ) : (
        <p>No flight data available.</p>
      )}
    </div>
  );
};

export default SDKTest;
