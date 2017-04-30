<?php

include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherSoapClient.php";
include_once $_SERVER["DOCUMENT_ROOT"] . "/ResponseGenerate.php";

header('Access-Control-Allow-Origin: *');

generateResponse($mySoapClient->getForecast());