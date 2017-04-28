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