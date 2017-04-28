<?php

include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherSoapClient.php";
include_once $_SERVER["DOCUMENT_ROOT"] . "/ResponseGenerate.php";

generateResponse($mySoapClient->getForecast());