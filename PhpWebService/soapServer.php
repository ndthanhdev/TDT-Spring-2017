<?php

class server
{
    public function __construct()
    {
    }


    public function addWeatherData($id_array)
    {
        return "123";
    }
}

$param = array('uri' => "server.php");
$server = new SoapServer(NULL, $param);
$server->setClass('server');
$server->handle();