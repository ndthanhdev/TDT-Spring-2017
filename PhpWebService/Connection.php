<?php

define("CONNECTION","sqlite:".$_SERVER["DOCUMENT_ROOT"]."/db.db");

class Connection extends PDO{
    public function __construct()
    {
        parent::__construct(CONNECTION);
        $this->exec("set names utf8");
        $this->setAttribute(PDO::ATTR_ERRMODE,
        PDO::ERRMODE_EXCEPTION);
    }
}
?>