<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/28/2017
 * Time: 6:01 AM
 */

include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherSoapClient.php";
include_once $_SERVER["DOCUMENT_ROOT"] . "/ResponseGenerate.php";

generateResponse($mySoapClient->getAllData());