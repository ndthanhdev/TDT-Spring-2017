<?php

class WeatherSoapClient extends SoapClient
{
    public function __construct()
    {
        $param = array('location' => "http://localhost/WeatherSoapServer.php",
            'uri' => "http://localhost/WeatherSoapServer.php",
            'trace' => 1);
        parent::__construct(null, $param);
    }

    public function addData($data)
    {
        return $this->__soapCall('addData', array($data));
    }

    public function getAllData()
    {
        return $this->__soapCall("getAllData",array());
    }

    public function getForecast()
    {
        return $this->__soapCall('getForecast', array());
    }
}

$mySoapClient = new WeatherSoapClient;