<?php

include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherServices.php";

class server
{
    var $service;

    public function __construct()
    {
        $this->service = new WeatherServices();
    }

    public function addData($data)
    {
        return json_encode($this->service->addData($data), JSON_PRETTY_PRINT);
    }

    public function getAllData()
    {
        return json_encode($this->service->getAllData(), JSON_PRETTY_PRINT);
    }

    public function getForecast()
    {
        return json_encode($this->service->getForecast(), JSON_PRETTY_PRINT);
    }
}


$param = array('uri' => "soap/WeatherSoapServer.php");
$server = new SoapServer(NULL, $param);
$server->setClass('server');
$server->handle();