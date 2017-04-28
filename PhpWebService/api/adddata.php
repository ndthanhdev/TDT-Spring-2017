<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/28/2017
 * Time: 7:05 PM
 */
include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherSoapClient.php";
include_once $_SERVER["DOCUMENT_ROOT"] . "/ResponseGenerate.php";

if (isset($_POST["Temperature"]) && isset($_POST["Humidity"])) {
    generateResponse($mySoapClient->addData(array("Temperature" => $_POST["Temperature"],
        "Humidity" => $_POST["Humidity"])));
}