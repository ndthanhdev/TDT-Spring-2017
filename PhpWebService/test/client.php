<?php
class client{
    public function __construct()
    {
        $param = array('location'=>"http://localhost/server.php",
            'uri'=>"http://localhost/server.php",
            'trace'=>1);
        $this->instance = new SoapClient(NULL,$param);

    }

    public function getName($id_array){
        return $this->instance->__soapCall('getStudentName',$id_array);
    }

}

$client = new client;