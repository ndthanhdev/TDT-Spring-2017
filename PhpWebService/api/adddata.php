<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/28/2017
 * Time: 7:05 PM
 */
include_once $_SERVER["DOCUMENT_ROOT"] . "/WeatherSoapClient.php";
include_once $_SERVER["DOCUMENT_ROOT"] . "/ResponseGenerate.php";


header('Access-Control-Allow-Origin: *');

header('Access-Control-Allow-Methods: GET, PUT, POST, DELETE, OPTIONS');

header("Access-Control-Allow-Headers: X-Requested-With, Content-Type, Accept, Origin, Authorization");

$data = json_decode(file_get_contents('php://input'), true);
if (isset($data["Temperature"]) && isset($data["Humidity"])) {
    generateResponse($mySoapClient->addData($data));
}