<?php

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/26/2017
 * Time: 8:44 PM
 */

include_once $_SERVER["DOCUMENT_ROOT"] . "/Connection.php";

class WeatherServices
{
    public function __construct()
    {
        $this->connection = new Connection();
    }


    public function addData($data)
    {
        try {
            if (!isset($data["Temperature"])
                || !isset($data["Humidity"])
            ) {
                return 1;
            }
            $stm = $this->connection->prepare("INSERT INTO Data VALUES(?,?,?)");
            $stm->execute(array(date(DATE_RFC3339), $data["Temperature"], $data["Humidity"]));
            return 0;
        } catch (Exception $e) {
            $result = array();
            $result[] = 2;
            $result[] = $e;
            return $result;
        }
    }

    public function getAllData()
    {
        try {
            $stm = $this->connection->prepare("SELECT * FROM Data ORDER BY Time DESC ");
            $stm->execute();
            $result = array();
            while ($row = $stm->fetch()) {
                $result[] = $row;
            }
            return $result;
        } catch (Exception $e) {
            $result = array();
            $result[] = 2;
            $result[] = $e;
            return $result;
        }
    }

    public function getForecast()
    {
        $stm = $this->connection->prepare("SELECT * FROM Data ORDER BY Time DESC LIMIT 5");
        $stm->execute();
        $rows = array();
        while ($row = $stm->fetch()) {
            $rows[] = $row;
        }
        $n = count($rows);
        if ($n > 0) {
            $temperature = 0;
            $humidity = 0;

            for ($i = 0; $i < $n; $i++) {
                $temperature += $rows[$i]["Temperature"];
                $humidity += $rows[$i]["Humidity"];
            }

            $result = array();
            $result["Temperature"] = $temperature / $n;
            $result["Humidity"] = $humidity / $n;
        } else {
            $result["Temperature"] = 'na';
            $result["Humidity"] = 'na';
        }
        return $result;
    }
}