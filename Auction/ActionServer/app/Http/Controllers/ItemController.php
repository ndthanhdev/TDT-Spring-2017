<?php
/**
 * Created by PhpStorm.
 * User: duyth
 * Date: 5/3/2017
 * Time: 4:20 PM
 */

namespace App\Http\Controllers;


use App\Entities\Item;

class ItemController extends Controller
{
    public function getItemsOfUser($id)
    {
        return response()->json(Item::where('id', $id)->get());
    }
}