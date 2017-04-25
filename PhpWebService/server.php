<?php

class server
{
    public function __construct()
    {
    }


    public function getWeatherData()
    {
        return "123";
    }
}

$param = array('uri' => "server.php");
$server = new SoapServer(null, $param);
$server->setClass('server');
$server->handle();