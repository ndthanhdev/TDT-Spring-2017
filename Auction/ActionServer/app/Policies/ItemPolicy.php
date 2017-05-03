<?php

namespace App\Policies;

use App\Entities\Item;
use App\Entities\User;

/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/3/2017
 * Time: 1:45 PM
 */
class ItemPolicy
{
    public function update(User $user, Item $item)
    {
//        echo $user->id;
//        echo $item->user_id;
        return true;
    }
}