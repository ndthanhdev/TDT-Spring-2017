<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/28/2017
 * Time: 11:32 PM
 */

function generateResponse($data)
{
    header('Content-type: application/json');
    echo $data;
}

function setHeader(){
    header('Access-Control-Allow-Origin: *');

    header('Access-Control-Allow-Methods: GET, PUT, POST, DELETE, OPTIONS');

    header("Access-Control-Allow-Headers: X-Requested-With, Content-Type, Accept, Origin, Authorization");
}