<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/26/2017
 * Time: 9:41 PM
 */
include_once __DIR__ . "/WeatherServices.php";

$data =array();
$data["Time"]=date(DATE_RFC3339);
$data["Temperature"]=0;
$data["Humidity"]=0;

echo json_encode((new WeatherServices())->addData($data),JSON_PRETTY_PRINT);
echo json_encode((new WeatherServices())->getAllData(),JSON_PRETTY_PRINT);
echo json_encode((new WeatherServices())->getForecast(),JSON_PRETTY_PRINT);