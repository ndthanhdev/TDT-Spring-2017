<?php

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 4/26/2017
 * Time: 7:58 AM
 */

define("PATH",$_SERVER["DOCUMENT_ROOT"]."db.db");

class myDb extends SQLite3
{

public function __construct()
{

}
}