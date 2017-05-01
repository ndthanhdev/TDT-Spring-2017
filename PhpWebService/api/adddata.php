<?php

include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherSoapClient.php";
include_once $_SERVER["DOCUMENT_ROOT"] . "/ResponseGenerate.php";


setHeader();

$data = json_decode(file_get_contents('php://input'), true);

if (isset($data["Temperature"]) && isset($data["Humidity"])) {
    generateResponse($mySoapClient->addData($data));
}