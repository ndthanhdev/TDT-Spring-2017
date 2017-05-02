<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/2/2017
 * Time: 12:06 PM
 */

namespace App\Entities;


use Illuminate\Database\Eloquent\Model;

class Item extends Model
{

    public $timestamps = false;

    public function bids(){
        return $this->hasMany('App\Entities\Bid');
    }
}