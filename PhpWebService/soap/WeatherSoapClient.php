<?php

class WeatherSoapClient extends SoapClient
{
    public function __construct()
    {
        $param = array('location' => "http://localhost/soap/WeatherSoapServer.php",
            'uri' => "http://localhost/soap/WeatherSoapServer.php",
            'trace' => 1);
        parent::__construct(null, $param);
    }

    public function addData($data)
    {
        return $this->__soapCall('addData', $data);
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