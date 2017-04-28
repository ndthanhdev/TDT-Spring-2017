<?php

class server
{
    public function __construct()
    {
    }


    public function getStudentName($id_array)
    {
        return "123";
    }
}

$param = array('uri' => "server.php");
$server = new SoapServer(NULL, $param);
$server->setClass('server');
$server->handle();